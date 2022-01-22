namespace Ems.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keyChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainMenus", "Fix", c => c.Boolean(nullable: false));
            AddColumn("dbo.MainMenus", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.MainMenus", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MenuPermissions", "Fix", c => c.Boolean(nullable: false));
            AddColumn("dbo.MenuPermissions", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.MenuPermissions", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuPermissions", "CreateTime");
            DropColumn("dbo.MenuPermissions", "CreatedBy");
            DropColumn("dbo.MenuPermissions", "Fix");
            DropColumn("dbo.MainMenus", "CreateTime");
            DropColumn("dbo.MainMenus", "CreatedBy");
            DropColumn("dbo.MainMenus", "Fix");
        }
    }
}
