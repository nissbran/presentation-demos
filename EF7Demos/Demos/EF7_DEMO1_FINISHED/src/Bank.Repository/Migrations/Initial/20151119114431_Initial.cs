namespace Bank.Repository.Migrations.Initial
{
    using System;
    using Microsoft.Data.Entity.Metadata;
    using Microsoft.Data.Entity.Migrations;

    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCheckResult",
                columns: table => new
                {
                    CreditCheckResultId = table.Column<Guid>(nullable: false),
                    AllowedCreditAmount = table.Column<decimal>(nullable: false),
                    Institute = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCheckResult", x => x.CreditCheckResultId);
                });
            migrationBuilder.CreateTable(
                name: "BankCustomer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCustomer", x => x.CustomerId);
                });
            migrationBuilder.CreateTable(
                name: "BankTransaction",
                columns: table => new
                {
                    BankTransactionId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransaction", x => x.BankTransactionId);
                    table.ForeignKey(
                        name: "FK_BankTransaction_BankCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "BankCustomer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("BankTransaction");
            migrationBuilder.DropTable("CreditCheckResult");
            migrationBuilder.DropTable("BankCustomer");
        }
    }
}
