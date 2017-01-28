namespace Demo.Bank.Domain.Commands.Account
{
    using System.Threading.Tasks;
    using EventStore.Lib.Common.Domain;
    using Models.Account;

    public class CreateAccountCommandHandler : CommandHandler<CreateAccountCommand>
    {
        private readonly IDomainRepository _domainRepository;

        public CreateAccountCommandHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public override async Task Handle(CreateAccountCommand command)
        {
            var newAccount = new Account(command.AccountNumber);

            newAccount.AddContactDetails(command);

            await _domainRepository.Save(newAccount);
        }
    }
}