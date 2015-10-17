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
                name: "EntityFrameworkHiLoSequence",
                incrementBy: 10);
            migrationBuilder.CreateTable(
                name: "BankCustomer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerType = table.Column<string>(nullable: false),
                    RegistrationNumber = table.Column<string>(nullable: true),
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
            migrationBuilder.DropSequence("EntityFrameworkHiLoSequence");
            migrationBuilder.DropTable("BankTransaction");
            migrationBuilder.DropTable("BankCustomer");
        }
    }
}
