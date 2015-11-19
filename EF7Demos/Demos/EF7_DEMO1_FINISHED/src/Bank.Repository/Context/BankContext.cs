namespace Bank.Repository.Context
{
    using Domain.Models;
    using Domain.Models.Customers;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;

    public class BankContext : DbContext
    {
        public DbSet<BankCustomer> Customers { get; set; }

        public DbSet<BankTransaction> Transactions { get; set; }

        public DbSet<CreditCheckResult> CreditCheckResults { get; set; }

        public BankContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankCustomer>(builder =>
            {
                builder.HasKey(customer => customer.CustomerId);
            });
            modelBuilder.Entity<Company>().HasBaseType<BankCustomer>();
            modelBuilder.Entity<PrivatePerson>().HasBaseType<BankCustomer>();

            modelBuilder.Entity<BankTransaction>(builder =>
            {
                builder.HasKey(t => t.BankTransactionId);

                builder.Property<long>("CustomerId");
                builder.HasOne(t => t.Customer)
                    .WithMany()
                    .HasForeignKey("CustomerId")
                    .IsRequired();

            });

            modelBuilder.Entity<CreditCheckResult>(builder =>
            {
                builder.HasKey(ccr => ccr.CreditCheckResultId);
            });
        }
    }
}
