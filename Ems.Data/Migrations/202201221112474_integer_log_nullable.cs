namespace Ems.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class integer_log_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "UserId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "UserId", c => c.Int(nullable: false));
        }
    }
}
