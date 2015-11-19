using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Bank.Repository.Migrations.Version1
{
    public partial class AddAccountingDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_BankTransaction_BankCustomer_CustomerId", table: "BankTransaction");
            migrationBuilder.AddColumn<bool>(
                name: "AccountingIsDone",
                table: "BankTransaction",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddForeignKey(
                name: "FK_BankTransaction_BankCustomer_CustomerId",
                table: "BankTransaction",
                column: "CustomerId",
                principalTable: "BankCustomer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_BankTransaction_BankCustomer_CustomerId", table: "BankTransaction");
            migrationBuilder.DropColumn(name: "AccountingIsDone", table: "BankTransaction");
            migrationBuilder.AddForeignKey(
                name: "FK_BankTransaction_BankCustomer_CustomerId",
                table: "BankTransaction",
                column: "CustomerId",
                principalTable: "BankCustomer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
