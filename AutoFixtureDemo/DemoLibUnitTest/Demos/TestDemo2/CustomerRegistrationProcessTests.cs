namespace DemoLibUnitTest.Demos.TestDemo2
{
    using DemoLib.Demo2;
    using DemoLib.Interfaces;
    using DemoLib.Model;
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
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
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
        public void When_registering_customer_Then_logg()
        {
            var logging = _fixture.Freeze<ILogging>();
            var sut = _fixture.Create<CustomerRegistrationProcess>();

            sut.RegisterCustomer(_fixture.Create<Customer>());

            logging.Received().Info(Arg.Any<string>());
        }
    }
}