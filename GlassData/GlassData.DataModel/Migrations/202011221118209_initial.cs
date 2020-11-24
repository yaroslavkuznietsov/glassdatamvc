namespace GlassData.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Glasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        LinePos = c.Int(nullable: false),
                        SourcePos = c.Int(nullable: false),
                        SourceSide = c.Int(nullable: false),
                        GlassId = c.String(nullable: false, maxLength: 50),
                        GlassHeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GlassWidth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GlassThickness = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GlassWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DestRackPos = c.Int(nullable: false),
                        DestRackSide = c.Int(nullable: false),
                        PreviousHeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PreviousWidth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GlassResult = c.Int(nullable: false),
                        OrderId = c.Int(),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 50),
                        DateTime = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Glasses", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Glasses", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Glasses", new[] { "CustomerId" });
            DropIndex("dbo.Glasses", new[] { "OrderId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Glasses");
            DropTable("dbo.Customers");
        }
    }
}
