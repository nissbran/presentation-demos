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
    public class PersonHandlerNUnitAutoDataTest
    {
        [Test, AutoNSubsituteData, ExpectedException(typeof(AgeTooLowException))]
        public void When_adding_an_customer_and_age_is_under_18_Then_cast_an_age_too_low_exception(
            PersonHandler sut,
            [Frozen]IFixture fixture)
        {
            var customer = fixture.Build<Customer>()
                                  .With(c => c.Age, fixture.CreateWithRange(0, 17))
                                  .Create();

            sut.AddNewCustomer(customer);
        }

        [Test, AutoNSubsituteData]
        public void When_adding_an_customer_with_the_correct_age_Then_it_should_persist_the_data_in_repository(
            [Frozen]IRepository<Person> repository,
            PersonHandler sut, 
            Customer customer)
        {
            sut.AddNewCustomer(customer);

            repository.Received().Add(Arg.Any<Customer>());
        }

        [Test, AutoNSubsituteData]
        public void When_adding_an_customer_Then_logg_info_row(
            [Frozen]ILogging logging,
            PersonHandler sut,
            Customer customer)
        {
            sut.AddNewCustomer(customer);

            logging.Received().Info(Arg.Any<string>());
        }
    }
}
