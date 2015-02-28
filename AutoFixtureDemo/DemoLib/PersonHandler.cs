namespace DemoLib
{
    using Exceptions;
    using Interfaces;
    using Model;

    public class PersonHandler : IPersonHandler
    {
        private readonly IRepository<Person> _personRepository;
        private readonly ILogging _logging;

        public PersonHandler(IRepository<Person> personRepository,
                             ILogging logging)
        {
            _personRepository = personRepository;
            _logging = logging;
        }


        public void AddNewCustomer(Customer customer)
        {
            throw new AgeTooLowException();
        }
    }
}
