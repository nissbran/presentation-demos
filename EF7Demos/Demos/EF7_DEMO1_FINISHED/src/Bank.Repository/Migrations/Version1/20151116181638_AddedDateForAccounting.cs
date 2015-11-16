using System;
using Microsoft.Data.Entity.Migrations;

namespace Bank.Repository.Migrations.Version1
{
    public partial class AddedDateForAccounting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateForAccounting",
                table: "BankTransaction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "DateForAccounting", table: "BankTransaction");
        }
    }
}
