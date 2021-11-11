namespace Ems.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(),
                        Mail = c.String(),
                        Password = c.String(),
                        RankId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Fix = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Examinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(),
                        ProcessId = c.Int(nullable: false),
                        InsuranceId = c.Int(nullable: false),
                        HaveInsurance = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                        isJudical = c.Boolean(nullable: false),
                        OfficerName = c.String(),
                        DoctorId = c.Int(nullable: false),
                        Fix = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Insurances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageName = c.String(),
                        DiscountRateForProcesses = c.Double(nullable: false),
                        DiscountRateForTests = c.Double(nullable: false),
                        DiscountRateForExtras = c.Double(nullable: false),
                        Price = c.Int(nullable: false),
                        Fix = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessName = c.String(),
                        ProcessType = c.String(),
                        Price = c.Int(nullable: false),
                        Fix = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Job = c.String(),
                        RankName = c.String(),
                        Fix = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegisterInsurances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(),
                        DoctorId = c.Int(nullable: false),
                        InsuranceId = c.Int(nullable: false),
                        Fix = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegisterInsurances");
            DropTable("dbo.Ranks");
            DropTable("dbo.Processes");
            DropTable("dbo.Insurances");
            DropTable("dbo.Examinations");
            DropTable("dbo.Doctors");
        }
    }
}
