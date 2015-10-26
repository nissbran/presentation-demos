using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Bank.Repository.Migrations
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
                    CustomerType = table.Column<int>(nullable: false),
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
                    CustomerId = table.Column<long>(nullable: false)
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
