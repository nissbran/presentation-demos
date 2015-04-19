namespace DemoLib
{
    using System.Data;
    using Exceptions;
    using Interfaces;
    using Model;

    public class CustomerHandler : ICustomerHandler
    {
        private readonly ILogging _logging;
        private readonly IRepository<Customer> _customerRepository;

        public CustomerHandler(ILogging logging, 
                               IRepository<Customer> customerRepository)
        {
            _logging = logging;
            _customerRepository = customerRepository;
        }

        public void AddNewPersonCustomer(Person customer)
        {
            if (customer.Age < 18)
                throw new AgeTooLowException();

            _customerRepository.Add(customer);
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
