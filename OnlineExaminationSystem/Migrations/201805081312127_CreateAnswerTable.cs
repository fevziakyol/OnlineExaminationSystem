namespace OnlineExaminationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAnswerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Answers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        QuestionId = c.Long(nullable: false),
                        UserAnswer = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Date = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserID)
                .Index(t => t.QuestionId);
            AddForeignKey("Answers", "QuestionId", "Questions", "Id");
            AddForeignKey("Answers", "UserID", "Users", "ID");

        }
        
        public override void Down()
        {
            DropForeignKey("Answers", "UserID", "Users");
            DropForeignKey("Answers", "QuestionId", "Questions");
            DropIndex("Answers", new[] { "QuestionId" });
            DropIndex("Answers", new[] { "UserID" });
            DropTable("Answers");
        }
    }
}
