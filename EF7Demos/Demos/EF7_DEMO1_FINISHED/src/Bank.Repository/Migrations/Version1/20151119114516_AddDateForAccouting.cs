namespace Bank.Repository.Migrations.Version1
{
    using System;
    using Microsoft.Data.Entity.Migrations;

    public partial class AddDateForAccouting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_BankTransaction_BankCustomer_CustomerId", table: "BankTransaction");
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateForAccounting",
                table: "BankTransaction",
                nullable: true);
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
            migrationBuilder.DropColumn(name: "DateForAccounting", table: "BankTransaction");
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
