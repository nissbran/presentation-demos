namespace DemoLib.Old_Demo2
{
    using System;
    using Exceptions;
    using Interfaces;
    using Model;
    using Model.Vehicles;

    public class CustomerRegistrationProcess
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Vehicle> _carRepository;
        private readonly ILogging _logging;
        private readonly IExternalCreditCheckProcess _externalCreditCheckProcess;

        public CustomerRegistrationProcess(
            IRepository<Customer> customerRepository,
            IRepository<Vehicle> carRepository,
            ILogging logging,
            IExternalCreditCheckProcess externalCreditCheckProcess)
        {
            _customerRepository = customerRepository;
            _carRepository = carRepository;
            _logging = logging;
            _externalCreditCheckProcess = externalCreditCheckProcess;
        }

        public void RegisterCustomer(Customer customer)
        {
            var creditResult = _externalCreditCheckProcess.ScoreCustomer(customer.SocialSecurityNumber);
            if (creditResult.Score < 20)
                throw new CustomerHasToLowCreditScoreException();
            _customerRepository.Add(customer);
            _logging.Info(string.Format("Customer accountnumber: {0}", customer.AccountNumber));

            var car = _carRepository.Get(2);
            if (car == null)
                throw new Exception();
        }
    }
}