using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Bank.Repository.Context;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace Bank.Repository.Migrations
{
    [DbContext(typeof(BankContext))]
    partial class Initial
    {
        public override string Id
        {
            get { return "20151016083549_Initial"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:HiLoSequenceName", "DefaultSequence")
                .Annotation("SqlServer:HiLoSequencePoolSize", 5)
                .Annotation("SqlServer:Sequence:.DefaultSequence", "'DefaultSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.SequenceHiLo);

            modelBuilder.Entity("Bank.Domain.Models.BankTransaction", b =>
                {
                    b.Property<Guid>("BankTransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<long>("CustomerId");

                    b.Key("BankTransactionId");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customer.BankCustomer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerType")
                        .Required();

                    b.Key("CustomerId");

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
                    b.Reference("Bank.Domain.Models.Customer.BankCustomer")
                        .InverseCollection()
                        .ForeignKey("CustomerId");
                });
        }
    }
}
