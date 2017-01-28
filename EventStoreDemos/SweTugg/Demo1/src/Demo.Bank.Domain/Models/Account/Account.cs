namespace Demo.Bank.Domain.Models.Account
{
    using System;
    using System.Collections.Generic;
    using Commands.Account;
    using Events.Account;
    using Events.Transactions;
    using EventStore.Lib.Common.Domain;

    public class Account : IAggregateRoot
    {
        public string Id { get; set; }

        public int Version { get; set; }

        public Type AggregateRootType { get; } = typeof(Account);

        private readonly AccountState _state = new AccountState();

        public List<IDomainEvent> UncommittedEvents { get; } = new List<IDomainEvent>();

        private static readonly Dictionary<Type, Action<IDomainEvent, Account>> EventDelegates = new Dictionary<Type, Action<IDomainEvent, Account>>
            {
                {typeof(AccountCreatedEvent), (e, a) => a.On((AccountCreatedEvent) e)},
                {typeof(AccountContactDetailsSetEvent),(e, a) => a.On((AccountContactDetailsSetEvent) e)},
                {typeof(BankTransferTransactionAddedEvent),(e, a) => a.On((BankTransferTransactionAddedEvent) e)},
                {typeof(CardTransactionAddedEvent),(e, a) => a.On((CardTransactionAddedEvent) e)}
            };

        public Account() { }

        public Account(string id)
        {
            Id = id;

            Apply(new AccountCreatedEvent
            {
                AccountNumber = id
            });
        }

        public void AddContactDetails(CreateAccountCommand command)
        {
            Apply(new AccountContactDetailsSetEvent
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                RegistrationNumber = command.RegistrationNumber
            });
        }

        public void LoadFromHistoricalEvents(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                EventDelegates[domainEvent.Metadata.EventType](domainEvent, this);
            }
        }

        public void AddBankTransferTransaction(decimal amount)
        {
            Apply(new BankTransferTransactionAddedEvent
            {
                Amount = amount
            });
        }
        public void AddCardTransaction(decimal amount, string authCode)
        {
            Apply(new CardTransactionAddedEvent
            {
                Amount = amount,
                AuthCode = authCode
            });
        }

        internal void On(AccountCreatedEvent e)
        {
            _state.AccountNumber = e.AccountNumber;
        }

        internal void On(AccountContactDetailsSetEvent e)
        {
            _state.ContactDetails.FirstName = e.FirstName;
            _state.ContactDetails.LastName = e.FirstName;
            _state.ContactDetails.RegistrationNumber = e.RegistrationNumber;
        }

        internal void On(BankTransferTransactionAddedEvent e)
        {
            _state.Balance += e.Amount;
        }

        internal void On(CardTransactionAddedEvent e)
        {
            _state.Balance += e.Amount;
        }

        private void Apply(IDomainEvent domainEvent)
        {
            UncommittedEvents.Add(domainEvent);
            EventDelegates[domainEvent.GetType()](domainEvent, this);
            Version++;
        }

        public void CommitEvents()
        {
            UncommittedEvents.Clear();
        }
    }
}