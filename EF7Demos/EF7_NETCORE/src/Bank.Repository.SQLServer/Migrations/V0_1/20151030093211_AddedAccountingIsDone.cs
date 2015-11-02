namespace Bank.Repository.SQLServer.Migrations.V0_1
{
    using Microsoft.Data.Entity.Migrations;

    public partial class AddedAccountingIsDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccountingIsDone",
                table: "BankTransaction",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "AccountingIsDone", table: "BankTransaction");
        }
    }
}
