namespace Ems.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rankhierarchy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hierarchies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        HierarchyRank = c.Short(nullable: false),
                        RankId = c.Int(nullable: false),
                        Fix = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "AccessManagementPanel", c => c.Boolean(nullable: false));
            DropColumn("dbo.Ranks", "AccessManagementPanel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ranks", "AccessManagementPanel", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "AccessManagementPanel");
            DropTable("dbo.Hierarchies");
        }
    }
}
