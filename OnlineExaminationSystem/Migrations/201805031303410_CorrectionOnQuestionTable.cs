namespace OnlineExaminationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionOnQuestionTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Questions", "ExamId", "Exams");
            DropIndex("Questions", new[] { "ExamId" });
            AlterColumn("Questions", "ExamId", c => c.Long(nullable: false));
            CreateIndex("Questions", "ExamId");
            AddForeignKey("Questions", "ExamId", "Exams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Questions", "ExamId", "Exams");
            DropIndex("Questions", new[] { "ExamId" });
            AlterColumn("Questions", "ExamId", c => c.Long());
            CreateIndex("Questions", "ExamId");
            AddForeignKey("Questions", "ExamId", "Exams", "Id");
        }
    }
}
