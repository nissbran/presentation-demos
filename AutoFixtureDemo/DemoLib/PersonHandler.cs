namespace DemoLib
{
    using Exceptions;
    using Interfaces;
    using Model;

    public class PersonHandler : IPersonHandler
    {
        private readonly IRepository<Person> _personRepository;
        private readonly ILogging _logging;

        public PersonHandler(ILogging logging, 
                             IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
            _logging = logging;
        }

        public void AddNewCustomer(Customer customer)
        {
            if (customer.Age < 18)
                throw new AgeTooLowException();

           _personRepository.Add(customer);
           _logging.Info("Added customer");
        }
    }
}
