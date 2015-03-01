namespace DemoLibUnitTest
{
    using DemoLib;
    using DemoLib.Exceptions;
    using DemoLib.Interfaces;
    using DemoLib.Model;
    using NSubstitute;
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

        [Test]
        public void When_adding_an_customer_with_the_correct_age_Then_it_should_persist_the_data_in_repository()
        {
            var repository = _fixture.Freeze<IRepository<Person>>();
            var handler = _fixture.Create<PersonHandler>();

            handler.AddNewCustomer(_fixture.Build<Customer>().With(c => c.Age, _fixture.Create<int>() + 18).Create());

            repository.Received().Add(Arg.Any<Customer>());
        }
    }
}
