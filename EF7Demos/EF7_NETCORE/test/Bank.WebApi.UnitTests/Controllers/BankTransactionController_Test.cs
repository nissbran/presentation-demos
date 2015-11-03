namespace Bank.WebApi.UnitTests.Controllers
{
    using Xunit;
    using Bank.WebApi.UnitTests.Controllers.Utils;
    using Bank.WebApi.Controllers;
    using Bank.Domain.Models.Customers;
    using Bank.Domain.Models;
    using System.Linq;

    public class BankTransactionController_Test
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var context = ContextFactory.CreateContext();
            var customer = new PrivatePerson("Test", "Testsson");
            context.Customers.Add(customer);
            context.Transactions.Add(new BankTransaction(customer, 23m));
            var controller = new BankTransactionsController(context);
            
            // Act
            var result = controller.Get();
            
            // Assert
            Assert.Equal(1, result.Count());
        }
    }
}