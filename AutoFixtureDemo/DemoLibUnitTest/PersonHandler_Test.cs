namespace DemoLibUnitTest
{
    using DemoLib;
    using DemoLib.Exceptions;
    using DemoLib.Model;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    [TestFixture]
    public class PersonHandlerTest
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredNSubstituteCustomization());
        }

        [Test]
        [ExpectedException(typeof(AgeTooLowException))]
        public void When_adding_an_customer_and_age_is_under_18_Then_cast_an_age_too_low_exception()
        {
            var handler = _fixture.Create<PersonHandler>();

            handler.AddNewCustomer(_fixture.Build<Customer>().With(customer => customer.Age, 14).Create());
        }
    }
}
