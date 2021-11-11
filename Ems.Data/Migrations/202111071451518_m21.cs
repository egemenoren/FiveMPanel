namespace Ems.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ranks", "AccessJobPanel", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ranks", "AccessManagementPanel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ranks", "AccessManagementPanel");
            DropColumn("dbo.Ranks", "AccessJobPanel");
        }
    }
}
