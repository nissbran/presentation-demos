using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Bank.Repository.SQLServer.Migrations.V0_1
{
    public partial class CreatedOnOnCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "BankCustomer",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CreatedOn", table: "BankCustomer");
        }
    }
}
