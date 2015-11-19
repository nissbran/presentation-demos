namespace Bank.Repository.Migrations.Initial
{
    using System;
    using Context;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;
    using Microsoft.Data.Entity.Metadata;
    using Microsoft.Data.Entity.Migrations;

    [DbContext(typeof(BankContext))]
    [Migration("20151119114431_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bank.Domain.Models.BankTransaction", b =>
                {
                    b.Property<Guid>("BankTransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<long>("CustomerId");

                    b.HasKey("BankTransactionId");
                });

            modelBuilder.Entity("Bank.Domain.Models.CreditCheckResult", b =>
                {
                    b.Property<Guid>("CreditCheckResultId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AllowedCreditAmount");

                    b.Property<string>("Institute");

                    b.HasKey("CreditCheckResultId");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customers.BankCustomer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.HasAnnotation("Relational:DiscriminatorProperty", "Discriminator");

                    b.HasAnnotation("Relational:DiscriminatorValue", "BankCustomer");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customers.Company", b =>
                {
                    b.HasBaseType("Bank.Domain.Models.Customers.BankCustomer");

                    b.Property<string>("Name");

                    b.HasAnnotation("Relational:DiscriminatorValue", "Company");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customers.PrivatePerson", b =>
                {
                    b.HasBaseType("Bank.Domain.Models.Customers.BankCustomer");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasAnnotation("Relational:DiscriminatorValue", "PrivatePerson");
                });

            modelBuilder.Entity("Bank.Domain.Models.BankTransaction", b =>
                {
                    b.HasOne("Bank.Domain.Models.Customers.BankCustomer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });
        }
    }
}
