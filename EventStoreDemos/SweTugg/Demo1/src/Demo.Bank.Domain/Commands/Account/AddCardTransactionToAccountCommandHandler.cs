namespace Demo.Bank.Domain.Commands.Account
{
    using System.Threading.Tasks;
    using EventStore.Lib.Common.Domain;
    using Models.Account;

    public class AddCardTransactionToAccountCommandHandler : CommandHandler<AddCardTransactionToAccountCommand>
    {
        private readonly IDomainRepository _domainRepository;

        public AddCardTransactionToAccountCommandHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }
        public override async Task Handle(AddCardTransactionToAccountCommand command)
        {
            var account = await _domainRepository.GetById<Account>(command.AccountNumber);

            account.AddCardTransaction(command.Amount, command.AuthCode);

            await _domainRepository.Save(account);
        }
    }
}