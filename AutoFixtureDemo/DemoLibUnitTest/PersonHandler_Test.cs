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

            _fixture.Customizations.Add(new RandomRangedNumberGenerator());
        }

        [Test]
        [ExpectedException(typeof(AgeTooLowException))]
        public void When_adding_an_customer_and_age_is_under_18_Then_cast_an_age_too_low_exception()
        {
            var handler = _fixture.Create<PersonHandler>();
            var age = _fixture.CreateWithRange(0, 17);

            handler.AddNewCustomer(_fixture.Build<Customer>().With(customer => customer.Age, age).Create());
        }

        [Test]
        public void When_adding_an_customer_with_the_correct_age_Then_it_should_persist_the_data_in_repository()
        {
            var repository = _fixture.Freeze<IRepository<Person>>();
            var handler = _fixture.Create<PersonHandler>();

            handler.AddNewCustomer(_fixture.Create<Customer>());

            repository.Received().Add(Arg.Any<Customer>());
        }

        [Test]
        public void When_adding_an_customer_Then_logg_info_row()
        {
            var logging = _fixture.Freeze<ILogging>();
            var handler = _fixture.Create<PersonHandler>();

            handler.AddNewCustomer(_fixture.Create<Customer>());

            logging.Received().Info(Arg.Any<string>());
        }
    }
}
