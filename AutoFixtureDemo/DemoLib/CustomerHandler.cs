namespace DemoLib
{
    using Exceptions;
    using Interfaces;
    using Model;

    public class CustomerHandler : ICustomerHandler
    {
        private readonly ILogging _logging;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Person> _personRepository;

        public CustomerHandler(ILogging logging, 
                               IRepository<Customer> customerRepository,
                               IRepository<Person> personRepository )
        {
            _logging = logging;
            _customerRepository = customerRepository;
            _personRepository = personRepository;
        }

        public void AddNewPersonCustomer(Person customer)
        {
            if (customer.Age < 18)
                throw new AgeTooLowException();

            _personRepository.Add(customer);
           _logging.Info("Added customer");
        }

        public bool AddNewCustomer(Customer customer)
        {
            if (customer == null)
                return false;

            _customerRepository.Add(customer);

            return true;
        }
    }
}
