namespace DemoLibUnitTest.Demos.TestDemo1
{
    using DemoLib.Demo1;
    using DemoLib.Model;
    using global::NUnit.Framework;
    using Ploeh.AutoFixture;

    [TestFixture]
    public class CustomerInfoCleanerTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void When_customer_info_is_sent_to_clean_Then_remove_social_security_number()
        {
            var sut = new CustomerInfoCleaner();

            var cleanedCustomer = sut.CleanSensitiveCustomerInfo(_fixture.Create<Customer>());

            Assert.IsNullOrEmpty(cleanedCustomer.SocialSecurityNumber);
        }
    }
}