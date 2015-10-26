namespace Bank.Repository.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankCustomers",
                c => new
                    {
                        CustomerId = c.Long(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        Name = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CustomerType = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.BankTransactions",
                c => new
                    {
                        BankTransactionId = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        CustomerId = c.Long(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.BankTransactionId)
                .ForeignKey("dbo.BankCustomers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankTransactions", "CustomerId", "dbo.BankCustomers");
            DropIndex("dbo.BankTransactions", new[] { "CustomerId" });
            DropTable("dbo.BankTransactions");
            DropTable("dbo.BankCustomers");
        }
    }
}
