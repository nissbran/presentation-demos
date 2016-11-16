
namespace DockerDemo.Consumer
{
    using System;
    using System.Data.SqlClient;
    using System.Net;
    using System.Text;
    using EventStore.ClientAPI;
    using EventStore.ClientAPI.SystemData;

    public class EventSubscriber : IDisposable
    {
        private readonly IEventStoreConnection _connection;

        private readonly EventStorePersistentSubscriptionBase _subscription;

        private readonly string _connectionString;

        public EventSubscriber()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "172.20.0.20";
            builder.UserID = "sa";
            builder.Password = "Nisse_Rules";
            builder.InitialCatalog = "master";
            _connectionString = builder.ConnectionString;

            CreateDatabase();
            // var node = new Uri("http://localhost:9200");
            // var settings = new Nest.ConnectionSettings(node).DefaultIndex("nisse-app");

            // _elasticClient = new ElasticClient(settings);

            var gossipSeeds = new IPEndPoint[] {
                new IPEndPoint(IPAddress.Parse("172.20.0.11"), 2113),
                new IPEndPoint(IPAddress.Parse("172.20.0.12"), 2123),
                new IPEndPoint(IPAddress.Parse("172.20.0.13"), 2133),
             };
            // var gossipSeeds = new IPEndPoint[] {
            //     new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2113),
            //     new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2123),
            //     new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2133),
            // };

            var connectionSettings = EventStore.ClientAPI.ConnectionSettings.Create();
            connectionSettings.SetReconnectionDelayTo(TimeSpan.FromSeconds(1))
                              .KeepReconnecting()
                              .KeepRetrying()
                              .SetMaxDiscoverAttempts(int.MaxValue)
                              .FailOnNoServerResponse()
                              .SetGossipSeedEndPoints(gossipSeeds)
                              .SetGossipTimeout(TimeSpan.FromMilliseconds(1000))
                              .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"))
                              .UseConsoleLogger();

            _connection = EventStoreConnection.Create(connectionSettings.Build());
            _connection.ConnectAsync();

            try
            {
                _connection.CreatePersistentSubscriptionAsync(
                    "$ce-Account",
                    "Consumer",
                    PersistentSubscriptionSettings.Create().PreferRoundRobin(), new UserCredentials("admin", "changeit"));
            }
            catch (System.Exception)
            {
                Console.WriteLine("Subscription already exist");
            }

            _subscription = _connection.ConnectToPersistentSubscription("$ce-Account", "Consumer", EventAppered, null, null, 1);
        }

        private void EventAppered(EventStorePersistentSubscriptionBase subscription, ResolvedEvent resolvedEvent)
        {
            Console.WriteLine($"Data appeared with number {resolvedEvent.Event.EventNumber}");

            try
            {

                InsertData(new MyData()
                {
                    Name = $"Event number {resolvedEvent.Event.EventNumber}"
                });
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            // var bulkDescriptor = new BulkDescriptor();

            // bulkDescriptor.Index<MyData>(op => op.Document(new MyData() { Id = resolvedEvent.Event.EventNumber, Name = "Data appeared"}));
            // var result = _elasticClient.Bulk(bulkDescriptor);

            //Console.WriteLine(result.IsValid);
        }

        private void CreateDatabase()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Create a sample database
                Console.Write("Creating database 'NissesDb' if it doesnt exist ... ");
                string sql = "IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'NissesDb') CREATE DATABASE [NissesDb];";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("USE NissesDb; ");
                sb.Append("if not exists (select [name] from sys.tables where [name] = 'MyData') CREATE TABLE MyData ( ");
                sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                sb.Append(" Name NVARCHAR(50) ");
                sb.Append("); ");
                sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InsertData(MyData data)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("USE NissesDb; ");
                sb.Append("INSERT INTO MyData (Name) ");
                sb.Append("VALUES (@name);");
                var sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", data.Name);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void Stop()
        {
            _subscription.Stop(new TimeSpan(0, 0, 0, 1));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EventSubscriber() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}