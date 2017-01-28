namespace Demo.Bank.Sql.Context
{
    using Microsoft.EntityFrameworkCore;
    using Model;

    public class AccountInformationContext : DbContext
    {
        public DbSet<AccountInformation> Accounts { get; set; }
        public AccountInformationContext(DbContextOptions<AccountInformationContext> options) :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccountInformation>().HasKey(m => m.AccountInformationId);

            base.OnModelCreating(builder);
        }
    }
}