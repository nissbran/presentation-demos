namespace DemoLib.Demo2
{
    using System;
    using Interfaces;
    using Model;

    public class CustomerRegistrationProcess
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ILogging _logging;

        public CustomerRegistrationProcess(IRepository<Customer> customerRepository,
            ILogging logging)
        {
            _customerRepository = customerRepository;
            _logging = logging;
        }

        public void RegisterCustomer(Customer customer)
        {
            _customerRepository.Add(customer);
            _logging.Info("Hej");
            var test = _customerRepository.Get(customer.Id);

        }
    }
}