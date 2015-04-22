namespace DemoLibUnitTest.Demos.Old_TestDemo1
{
    using DemoLib.Model;
    using DemoLib.Old_Demo1;
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
            var customer = _fixture.Create<Customer>();

            var result = sut.CleanSensitiveCustomerInfo(customer);

            Assert.IsNullOrEmpty(result.SocialSecurityNumber);
        }
    }
}