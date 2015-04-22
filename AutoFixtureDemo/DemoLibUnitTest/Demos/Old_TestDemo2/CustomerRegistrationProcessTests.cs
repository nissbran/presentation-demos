namespace DemoLibUnitTest.Demos.Old_TestDemo2
{
    using DemoLib.Exceptions;
    using DemoLib.Interfaces;
    using DemoLib.Model;
    using DemoLib.Old_Demo2;
    using global::NUnit.Framework;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    [TestFixture]
    public class CustomerRegistrationProcessTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredNSubstituteCustomization());

            _fixture.Customize<CreditCheckResult>(composer => composer.With(result => result.Score, 24));
        }

        [Test]
        public void When_registering_customer_Then_add_to_customer_repository()
        {
            var repository = _fixture.Freeze<IRepository<Customer>>();
            var sut = _fixture.Create<CustomerRegistrationProcess>();

            sut.RegisterCustomer(_fixture.Create<Customer>());

            repository.Received().Add(Arg.Any<Customer>());
        }

        [Test]
        public void When_registering_customer_Then_logg_the_customer_accountNumber()
        {
            var loggning = _fixture.Freeze<ILogging>();
            var sut = _fixture.Create<CustomerRegistrationProcess>();
            var customer = _fixture.Create<Customer>();

            sut.RegisterCustomer(customer);

            loggning.Received().Info(Arg.Is<string>(s => s.Contains(customer.AccountNumber.ToString())));
        }

        [Test, ExpectedException(typeof(CustomerHasToLowCreditScoreException))]
        public void When_registering_a_customer_with_too_low_score_Then_throw_exception()
        {
            _fixture.Customize<CreditCheckResult>(composer => composer.With(result => result.Score, 11));
            var sut = _fixture.Create<CustomerRegistrationProcess>();

            sut.RegisterCustomer(_fixture.Create<Customer>());
        }
    }
}