namespace DemoLibUnitTest.Demos.TestDemo2
{
    using global::NUnit.Framework;
    using Ploeh.AutoFixture;

    [TestFixture]
    public class CustomerRegistrationProcessTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void When_registering_customer_Then_add_to_customer_repository()
        {
        }
    }
}