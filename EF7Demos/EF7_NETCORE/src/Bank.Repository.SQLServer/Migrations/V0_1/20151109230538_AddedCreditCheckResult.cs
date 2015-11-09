using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace Bank.Repository.SQLServer.Migrations.V0_1
{
    public partial class AddedCreditCheckResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCheckResult",
                columns: table => new
                {
                    CreditCheckResultId = table.Column<Guid>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo),
                    AllowedCreditAmount = table.Column<decimal>(nullable: false),
                    Institute = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCheckResult", x => x.CreditCheckResultId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("CreditCheckResult");
        }
    }
}
