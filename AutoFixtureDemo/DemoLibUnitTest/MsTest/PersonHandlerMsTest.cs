namespace DemoLibUnitTest.MsTest
{
    using Customizations;
    using DemoLib;
    using DemoLib.Exceptions;
    using DemoLib.Interfaces;
    using DemoLib.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    [TestClass]
    public class PersonHandlerMsTest
    {
        private IFixture _fixture;

        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            _fixture.Customizations.Add(new RandomRangedNumberGenerator());
        }

        [TestMethod]
        [ExpectedException(typeof(AgeTooLowException))]
        public void When_adding_an_customer_and_age_is_under_18_Then_cast_an_age_too_low_exception()
        {
            var age = _fixture.CreateWithRange(0, 17);
            var sut = _fixture.Create<PersonHandler>();

            sut.AddNewCustomer(_fixture.Build<Customer>().With(customer => customer.Age, age).Create());
        }

        [TestMethod]
        public void When_adding_an_customer_with_the_correct_age_Then_it_should_persist_the_data_in_repository()
        {
            var repository = _fixture.Freeze<IRepository<Person>>();
            var sut = _fixture.Create<PersonHandler>();

            sut.AddNewCustomer(_fixture.Create<Customer>());

            repository.Received().Add(Arg.Any<Customer>());
        }

        [TestMethod]
        public void When_adding_an_customer_Then_logg_info_row()
        {
            var logging = _fixture.Freeze<ILogging>();
            var sut = _fixture.Create<PersonHandler>();

            sut.AddNewCustomer(_fixture.Create<Customer>());

            logging.Received().Info(Arg.Any<string>());
        }
    }
}
