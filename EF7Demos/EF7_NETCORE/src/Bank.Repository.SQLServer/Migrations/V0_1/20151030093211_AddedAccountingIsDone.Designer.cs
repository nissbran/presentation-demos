namespace Bank.Repository.SQLServer.Migrations.V0_1
{
    using System;
    using Context;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;
    using Microsoft.Data.Entity.Metadata;
    using Microsoft.Data.Entity.Migrations;

    [DbContext(typeof(MigrationContext))]
    [Migration("20151030093211_AddedAccountingIsDone")]
    partial class AddedAccountingIsDone
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bank.Domain.Models.BankTransaction", b =>
                {
                    b.Property<Guid>("BankTransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AccountingIsDone");

                    b.Property<decimal>("Amount");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<long>("CustomerId");

                    b.Property<DateTimeOffset?>("DateForAccounting");

                    b.HasKey("BankTransactionId");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customers.BankCustomer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerType")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.Annotation("Relational:DiscriminatorProperty", "CustomerType");

                    b.Annotation("Relational:DiscriminatorValue", "BankCustomer");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customers.Company", b =>
                {
                    b.BaseType("Bank.Domain.Models.Customers.BankCustomer");

                    b.Property<string>("Name");

                    b.Annotation("Relational:DiscriminatorValue", "Company");
                });

            modelBuilder.Entity("Bank.Domain.Models.Customers.PrivatePerson", b =>
                {
                    b.BaseType("Bank.Domain.Models.Customers.BankCustomer");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Annotation("Relational:DiscriminatorValue", "PrivatePerson");
                });

            modelBuilder.Entity("Bank.Domain.Models.BankTransaction", b =>
                {
                    b.HasOne("Bank.Domain.Models.Customers.BankCustomer")
                        .WithMany()
                        .ForeignKey("CustomerId");
                });
        }
    }
}
