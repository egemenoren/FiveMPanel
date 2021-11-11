namespace Ems.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Examinations", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Insurances", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Processes", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Ranks", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RegisterInsurances", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisterInsurances", "CreateTime");
            DropColumn("dbo.Ranks", "CreateTime");
            DropColumn("dbo.Processes", "CreateTime");
            DropColumn("dbo.Insurances", "CreateTime");
            DropColumn("dbo.Examinations", "CreateTime");
            DropColumn("dbo.Doctors", "CreateTime");
        }
    }
}
