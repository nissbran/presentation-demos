namespace Bank.Repository.SQLServer.Migrations.V0_1
{
    using System;
    using Microsoft.Data.Entity.Migrations;

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
