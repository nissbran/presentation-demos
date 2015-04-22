namespace DemoLib.Old_Demo2
{
    using Exceptions;
    using Interfaces;
    using Model;

    public class CustomerRegistrationProcess
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ILogging _logging;
        private readonly IExternalCreditCheckProcess _externalCreditCheckProcess;

        public CustomerRegistrationProcess(
            IRepository<Customer> customerRepository,
            ILogging logging,
            IExternalCreditCheckProcess externalCreditCheckProcess)
        {
            _customerRepository = customerRepository;
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
        }
    }
}