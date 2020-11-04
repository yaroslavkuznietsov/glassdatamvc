namespace GlassData.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeSpanAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DateTimeSpans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(),
                        DateEnd = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DateTimeSpans");
        }
    }
}
