namespace DemoLibUnitTest.NUnit
{
    using System.Runtime.InteropServices;
    using Customizations;
    using DemoLib;
    using DemoLib.Exceptions;
    using DemoLib.Interfaces;
    using DemoLib.Model;
    using global::NUnit.Framework;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    [TestFixture]
    public class CustomerHandlerNUnitTest
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            _fixture.Customizations.Add(new RandomRangedNumberGenerator());
        }

        [TestCase(10, ExpectedException = typeof(AgeTooLowException))]
        [TestCase(17, ExpectedException = typeof(AgeTooLowException))]
        public void When_adding_an_customer_and_age_is_under_18_Then_cast_an_age_too_low_exception(int age)
        {
            var logging = Substitute.For<ILogging>();
            var personRepository = Substitute.For<IRepository<Person>>();
            var customerRepository = Substitute.For<IRepository<Customer>>();
            var sut = new CustomerHandler(logging, customerRepository, personRepository);
            var customer = new Person
            {
                Age = age
            };

            sut.AddNewPersonCustomer(customer);
        }

        [Test, ExpectedException(typeof(AgeTooLowException))]
        public void When_adding_an_customer_and_age_is_under_18_Then_cast_an_age_too_low_exception()
        {
            var customer = _fixture.Build<Person>()
                                   .With(c => c.Age, _fixture.CreateWithRange(0, 17))
                                   .Create();
            var sut = _fixture.Create<CustomerHandler>();

            sut.AddNewPersonCustomer(customer);
        }

        [Test]
        public void When_adding_an_customer_with_the_correct_age_Then_it_should_persist_the_data_in_repository()
        {
            var repository = _fixture.Freeze<IRepository<Person>>();
            var sut = _fixture.Create<CustomerHandler>();

            sut.AddNewPersonCustomer(_fixture.Create<Person>());

            repository.Received().Add(Arg.Any<Person>());
        }

        [Test]
        public void When_adding_an_customer_Then_logg_info_row()
        {
            var logging = _fixture.Freeze<ILogging>();
            var customer = _fixture.Create<Person>();
            var sut = _fixture.Create<CustomerHandler>();

            sut.AddNewPersonCustomer(customer);

            logging.Received().Info(Arg.Any<string>());
        }

        [Test]
        public void InjectTest()
        {
            _fixture.Inject<Person>(new Owner());

            var test = _fixture.Create<Person>();

            _fixture.Inject<Person>(new Person());

            var test2 = _fixture.Create<Person>();
        }


        private Customer CreateCustomer()
        {
            return new Customer
            {
                Id = 213,
                FirstName = "first name",
                LastName = "last name",
                Age = 23,
                Company = new Company
                {
                    Address = "Address 1",
                    PostalCode = "32444",
                    City = "City"
                }
            };
        }
    }
}
