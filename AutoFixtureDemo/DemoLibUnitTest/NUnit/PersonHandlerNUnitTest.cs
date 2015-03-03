namespace DemoLibUnitTest.NUnit
{
    using Customizations;
    using DemoLib;
    using DemoLib.Exceptions;
    using DemoLib.Interfaces;
    using DemoLib.Model;
    using global::NUnit.Framework;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.NUnit2;

    [TestFixture]
    public class PersonHandlerNUnitTest
    {
        [Test, AutoNSubsituteData, ExpectedException(typeof(AgeTooLowException))]
        public void When_adding_an_customer_and_age_is_under_18_Then_cast_an_age_too_low_exception(
            PersonHandler handler,
            [Frozen]IFixture fixture)
        {
            var age = fixture.CreateWithRange(0, 17);

            handler.AddNewCustomer(fixture.Build<Customer>().With(customer => customer.Age, age).Create());
        }

        [Test, AutoNSubsituteData]
        public void When_adding_an_customer_with_the_correct_age_Then_it_should_persist_the_data_in_repository(
            [Frozen]IRepository<Person> repository,
            PersonHandler handler, Customer customer)
        {
            handler.AddNewCustomer(customer);

            repository.Received().Add(Arg.Any<Customer>());
        }

        [Test, AutoNSubsituteData]
        public void When_adding_an_customer_Then_logg_info_row(
            [Frozen]ILogging logging,
            PersonHandler handler,
            Customer customer)
        {
            handler.AddNewCustomer(customer);

            logging.Received().Info(Arg.Any<string>());
        }
    }
}
