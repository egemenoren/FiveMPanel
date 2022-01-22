namespace Ems.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menuTablesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubMenu = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        MainMenuId = c.Int(nullable: false),
                        RankId = c.Int(nullable: true),
                        UserId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MenuPermissions");
            DropTable("dbo.MainMenus");
        }
    }
}
