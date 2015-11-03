using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Bank.Repository.SQLite.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankCustomer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                        principalColumn: "CustomerId");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("BankTransaction");
            migrationBuilder.DropTable("BankCustomer");
        }
    }
}
