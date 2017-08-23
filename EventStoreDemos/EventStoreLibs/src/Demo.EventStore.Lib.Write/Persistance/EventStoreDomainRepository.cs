namespace Demo.EventStore.Lib.Write.Persistance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Domain;
    using global::EventStore.ClientAPI;
    using Newtonsoft.Json;
    using global::EventStore.ClientAPI.Exceptions;

    public class EventStoreDomainRepository : IDomainRepository
    {
        private const int MinStreamVersion = 0;
        private const int MaxStreamVersion = int.MaxValue;
        private const int ReadBatchSize = 500;
        private const int WriteBatchSize = 500;

        private readonly IEventStoreConnection _connection;

        public EventStoreDomainRepository(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async Task<TAggregateRoot> GetById<TAggregateRoot>(string aggregateRootId) where TAggregateRoot : IAggregateRoot, new()
        {
            var aggregateRoot = new TAggregateRoot {Id = aggregateRootId};

            var streamId = new StreamId(aggregateRoot.AggregateRootType, aggregateRootId);

            var events = await GetEvents(streamId);

            if (events.Count == 0)
                throw new ArgumentException($"AggregateRoot with streamid '{streamId}' does not exist.");

            List<IDomainEvent> domainEvents = events.Select(ConvertEventDataToEventOfType).ToList();

            aggregateRoot.LoadFromHistoricalEvents(domainEvents);
            aggregateRoot.Version = events.Count;
            
            return aggregateRoot;
        }

        public async Task Save(IAggregateRoot aggregateRoot)
        {
            var domainEvents = aggregateRoot.UncommittedEvents;

            var streamId = new StreamId(aggregateRoot.AggregateRootType, aggregateRoot.Id);
            var streamVersion = aggregateRoot.Version - domainEvents.Count;

            try
            {
                await InsertToEventStore(streamId, streamVersion, domainEvents);
            }
            catch (WrongExpectedVersionException)
            {
                Console.WriteLine("Wrong version");
            }

            aggregateRoot.CommitEvents();
        }

        private async Task<List<ResolvedEvent>> GetEvents(StreamId streamId)
        {
            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            var nextSliceStart = MinStreamVersion;
            do
            {
                var nextSliceSize = ReadBatchSize;
                var nextSliceEnd = nextSliceStart + nextSliceSize;
                if (nextSliceEnd > MaxStreamVersion)
                    nextSliceSize = MaxStreamVersion - nextSliceStart;

                currentSlice = await _connection.ReadStreamEventsForwardAsync(streamId.ToString(), nextSliceStart, nextSliceSize, false);

                if (currentSlice.Status != SliceReadStatus.Success)
                {
                    //_logger.Info($"Received {currentSlice.Status} for stream: {streamId}, retrying one time.");
                    await Task.Delay(100);

                    currentSlice = await _connection.ReadStreamEventsForwardAsync(streamId.ToString(), nextSliceStart, nextSliceSize, false);
                    //if (currentSlice.Status != SliceReadStatus.Success)
                    // _logger.Error($"Error, could not find stream {streamId}, Status: {currentSlice.Status}, {currentSlice.IsEndOfStream}, {currentSlice.LastEventNumber}");
                }

                nextSliceStart = currentSlice.NextEventNumber;

                streamEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream && currentSlice.NextEventNumber < MaxStreamVersion);

            return streamEvents;
        }
        
        private IDomainEvent ConvertEventDataToEventOfType(ResolvedEvent resolvedEvent)
        {
            var metadataString = Encoding.UTF8.GetString(resolvedEvent.Event.Metadata);
            var eventString = Encoding.UTF8.GetString(resolvedEvent.Event.Data);

            var metadata = JsonConvert.DeserializeObject<DomainMetadata>(metadataString);
            var eventType = Type.GetType(metadata.EventClrType);
            metadata.EventType = eventType;

            var domainEvent = (IDomainEvent)JsonConvert.DeserializeObject(eventString, eventType);
            domainEvent.Metadata = metadata;

            return domainEvent;
        }

        private async Task<StreamWriteResult> InsertToEventStore(StreamId streamId, int streamVersion, IEnumerable<IDomainEvent> events)
        {
            var newEvents = events.ToList();
            if (newEvents.Any() == false)
                return new StreamWriteResult(-1);

            var commitId = Guid.NewGuid();

            var expectedVersion = streamVersion == 0 ? ExpectedVersion.NoStream : streamVersion - 1;
            var eventsToSave = newEvents.Select(e => ToEventData(commitId, e)).ToList();

            if (eventsToSave.Count < WriteBatchSize)
            {
                var result = await _connection.AppendToStreamAsync(streamId.ToString(), expectedVersion, eventsToSave);

                return new StreamWriteResult(result.NextExpectedVersion);
            }
            else
            {
                using (var transaction = await _connection.StartTransactionAsync(streamId.ToString(), expectedVersion))
                {
                    var position = 0;
                    while (position < eventsToSave.Count)
                    {
                        var pageEvents = eventsToSave.Skip(position).Take(WriteBatchSize);
                        await transaction.WriteAsync(pageEvents);
                        position += WriteBatchSize;
                    }

                    var result = await transaction.CommitAsync();

                    return new StreamWriteResult(result.NextExpectedVersion);
                }
            }
        }

        private static EventData ToEventData(Guid commitId, IDomainEvent domainEvent)
        {
            domainEvent.Metadata.CommitId = commitId;
            domainEvent.Metadata.EventClrType = domainEvent.GetType().AssemblyQualifiedName;

            var dataJson = JsonConvert.SerializeObject(domainEvent);
            var metadataJson = JsonConvert.SerializeObject(domainEvent.Metadata);

            var data = Encoding.UTF8.GetBytes(dataJson);
            var metadata = Encoding.UTF8.GetBytes(metadataJson); 

            return new EventData(Guid.NewGuid(), domainEvent.GetType().FullName, true, data, metadata);
        }
    }
}