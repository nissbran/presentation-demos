namespace DemoLib.Web.Tests.Controllers
{
    using ApiControllerCustomizations;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;
    using Web.Controllers;

    [TestFixture]
    public class CustomerControllerTest
    {
        [Test]
        public void When_get_is_called_with_an_id_Then_get_the_customer_from_repository()
        {
            var fixture = new Fixture().Customize(
                new CompositeCustomization(
                    new HttpRequestMessageCustomization(),
                    new ApiControllerCustomization(),
                    new AutoConfiguredNSubstituteCustomization()));

            var controller = fixture.Create<CustomerController>();

            var customer = controller.Get();

            Assert.IsNotNull(customer);
        }
    }
}
