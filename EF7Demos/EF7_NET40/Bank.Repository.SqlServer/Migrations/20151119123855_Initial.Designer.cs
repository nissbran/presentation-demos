using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Bank.Repository.SqlServer.Context;

namespace Bank.Repository.SqlServer.Migrations
{
    [DbContext(typeof(SqlServerMigrationContext))]
    [Migration("20151119123855_Initial")]
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

                    b.Property<long>("CustomerId");

                    b.HasKey("BankTransactionId");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customer.BankCustomer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerType")
                        .IsRequired();

                    b.Property<string>("RegistrationNumber");

                    b.HasKey("CustomerId");

                    b.HasAnnotation("Relational:DiscriminatorProperty", "CustomerType");

                    b.HasAnnotation("Relational:DiscriminatorValue", "BankCustomer");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customer.Company", b =>
                {
                    b.HasBaseType("Bank.Domain.Models.Customer.BankCustomer");

                    b.Property<string>("Name");

                    b.HasAnnotation("Relational:DiscriminatorValue", "Company");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customer.PrivatePerson", b =>
                {
                    b.HasBaseType("Bank.Domain.Models.Customer.BankCustomer");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasAnnotation("Relational:DiscriminatorValue", "PrivatePerson");
                });

            modelBuilder.Entity("Bank.Domain.Models.BankTransaction", b =>
                {
                    b.HasOne("Bank.Domain.Models.Customer.BankCustomer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });
        }
    }
}
