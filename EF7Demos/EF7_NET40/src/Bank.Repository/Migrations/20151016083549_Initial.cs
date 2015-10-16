using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Bank.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "DefaultSequence",
                incrementBy: 10);
            migrationBuilder.CreateTable(
                name: "BankCustomer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(isNullable: false),
                    CustomerType = table.Column<string>(isNullable: false),
                    Name = table.Column<string>(isNullable: true),
                    FirstName = table.Column<string>(isNullable: true),
                    LastName = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCustomer", x => x.CustomerId);
                });
            migrationBuilder.CreateTable(
                name: "BankTransaction",
                columns: table => new
                {
                    BankTransactionId = table.Column<Guid>(isNullable: false),
                    Amount = table.Column<decimal>(isNullable: false),
                    CustomerId = table.Column<long>(isNullable: false)
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
            migrationBuilder.DropSequence("DefaultSequence");
            migrationBuilder.DropTable("BankTransaction");
            migrationBuilder.DropTable("BankCustomer");
        }
    }
}
