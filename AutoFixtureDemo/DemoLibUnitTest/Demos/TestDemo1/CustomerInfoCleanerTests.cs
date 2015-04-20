namespace DemoLibUnitTest.Demos.TestDemo1
{
    using DemoLib.Demo1;
    using DemoLib.Model;
    using global::NUnit.Framework;
    using Ploeh.AutoFixture;

    [TestFixture]
    public class CustomerInfoCleanerTests
    {
        [Test]
        public void When_customer_info_is_sent_to_clean_Then_remove_social_security_number()
        {
            var fixture = new Fixture();
            var sut = new CustomerInfoCleaner();
            var customer = fixture.Create<Customer>();
            //var customer = fixture.Customize<Customer>(composer => composer.Without(c => c.SocialSecurityNumber));

            var result = sut.CleanSensitiveCustomerInfo(customer);

            Assert.AreEqual(null, result.SocialSecurityNumber);
        }
    }
}