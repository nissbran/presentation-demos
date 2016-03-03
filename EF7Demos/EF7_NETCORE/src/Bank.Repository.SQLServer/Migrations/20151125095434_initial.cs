using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Bank.Repository.SQLServer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Test",
                incrementBy: 10);
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
                    CustomerId = table.Column<long>(nullable: false, defaultValueSql: "next value for Test"),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CustomerType = table.Column<string>(nullable: false),
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
                    AccountingIsDone = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    DateForAccounting = table.Column<DateTimeOffset>(nullable: true)
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
            migrationBuilder.DropSequence("Test");
            migrationBuilder.DropTable("BankTransaction");
            migrationBuilder.DropTable("CreditCheckResult");
            migrationBuilder.DropTable("BankCustomer");
        }
    }
}
