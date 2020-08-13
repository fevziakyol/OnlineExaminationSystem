namespace OnlineExaminationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAsignedExamTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AssignedExams",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        ExamId = c.Long(nullable: false),
                        IsActiv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserID)
                .Index(t => t.ExamId);
            AddForeignKey("AssignedExams", "ExamId", "Exams", "Id");
            AddForeignKey("AssignedExams", "UserID", "Users", "ID");

        }
        
        public override void Down()
        {
            DropForeignKey("AssignedExams", "UserID", "Users");
            DropForeignKey("AssignedExams", "ExamId", "Exams");
            DropIndex("AssignedExams", new[] { "ExamId" });
            DropIndex("AssignedExams", new[] { "UserID" });
            DropTable("AssignedExams");
        }
    }
}
