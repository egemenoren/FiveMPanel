namespace Ems.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Mail = c.String(),
                        ValueEntered = c.String(),
                        Exception = c.String(),
                        StackTrace = c.String(),
                        Url = c.String(),
                        Level = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Fix = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
