namespace Recruitment.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidate",
                c => new
                    {
                        CandidateId = c.Int(nullable: false, identity: true),
                        PositionId = c.Int(),
                        LevelId = c.Int(),
                        FullName = c.String(),
                        Birthday = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        CV = c.String(),
                        IsPDF = c.Boolean(nullable: false),
                        IntroduceName = c.String(),
                        IsApplied = c.Int(defaultValue: null),
                        IsContacted = c.Boolean(defaultValue: null),
                        InterviewTime = c.DateTime(),
                        InterviewLocation = c.String(),
                        Note = c.String(),
                        TestPoint = c.Int(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateId)
                .ForeignKey("dbo.Level", t => t.LevelId)
                .ForeignKey("dbo.Position", t => t.PositionId)
                .Index(t => t.PositionId)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Level",
                c => new
                    {
                        LevelId = c.Int(nullable: false, identity: true),
                        LevelName = c.String(),
                    })
                .PrimaryKey(t => t.LevelId);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        PositionName = c.String(),
                    })
                .PrimaryKey(t => t.PositionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candidate", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Candidate", "LevelId", "dbo.Level");
            DropIndex("dbo.Candidate", new[] { "LevelId" });
            DropIndex("dbo.Candidate", new[] { "PositionId" });
            DropTable("dbo.Position");
            DropTable("dbo.Level");
            DropTable("dbo.Candidate");
        }
    }
}
