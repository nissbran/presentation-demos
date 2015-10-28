using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Bank.Repository.Context;

namespace Bank.Repository.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20151027123651_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                .Annotation("SqlServer:Sequence:.EntityFrameworkHiLoSequence", "'EntityFrameworkHiLoSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

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

                    b.Annotation("Relational:DiscriminatorProperty", "CustomerType");

                    b.Annotation("Relational:DiscriminatorValue", "BankCustomer");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customer.Company", b =>
                {
                    b.BaseType("Bank.Domain.Models.Customer.BankCustomer");

                    b.Property<string>("Name");

                    b.Annotation("Relational:DiscriminatorValue", "Company");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customer.PrivatePerson", b =>
                {
                    b.BaseType("Bank.Domain.Models.Customer.BankCustomer");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Annotation("Relational:DiscriminatorValue", "PrivatePerson");
                });

            modelBuilder.Entity("Bank.Domain.Models.BankTransaction", b =>
                {
                    b.HasOne("Bank.Domain.Models.Customer.BankCustomer")
                        .WithMany()
                        .ForeignKey("CustomerId");
                });
        }
    }
}
