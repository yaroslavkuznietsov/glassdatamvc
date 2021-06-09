namespace GlassData.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlassExcludeCustomer : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Glasses", name: "CustomerId", newName: "Customer_Id");
            RenameIndex(table: "dbo.Glasses", name: "IX_CustomerId", newName: "IX_Customer_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Glasses", name: "IX_Customer_Id", newName: "IX_CustomerId");
            RenameColumn(table: "dbo.Glasses", name: "Customer_Id", newName: "CustomerId");
        }
    }
}
