namespace Bank.WebApi.UnitTests.Controllers.Utils
{
    using Bank.Repository.Context;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;

    public static class ContextFactory
    {
        private static readonly DbContextOptions ContextOptions;

        static ContextFactory()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder();
            contextOptionsBuilder.UseInMemoryDatabase();

            ContextOptions = contextOptionsBuilder.Options;
        }

        public static BankContext CreateContext()
        {
            return new BankContext(ContextOptions);
        }
    }
}