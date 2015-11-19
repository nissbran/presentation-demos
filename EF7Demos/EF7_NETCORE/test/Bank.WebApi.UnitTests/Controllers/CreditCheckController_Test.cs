namespace Bank.WebApi.UnitTests.Controllers
{
    using Xunit;
    using Bank.WebApi.Controllers;
    using Bank.Domain.Models;
    using System.Linq;
    using Utils;

    public class CreditCheckController_Test
    {
        [Fact]
        public void Get_all_credit_check_results()
        {
            // Arrange
            var context = ContextFactory.CreateContext();
            context.CreditCheckResults.Add(new CreditCheckResult(30, "UC"));
            context.SaveChanges();

            var controller = new CreditCheckController(context);
            
            // Act
            var result = controller.Get();
            
            // Assert
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void Get_specific_credit_check_results()
        {
            // Arrange
            var context = ContextFactory.CreateContext();
            var expectedCreditCheckResult = new CreditCheckResult(6000, "UC4");
            context.CreditCheckResults.Add(new CreditCheckResult(5000, "UC1"));
            context.CreditCheckResults.Add(expectedCreditCheckResult);
            context.SaveChanges();

            var controller = new CreditCheckController(context);

            // Act
            var result = controller.Get(expectedCreditCheckResult.CreditCheckResultId);

            // Assert
            Assert.Equal(result.Institute, "UC4");
        }

    }
}