namespace OnlineExaminationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addExamStartDateColumnInAssignedExam : DbMigration
    {
        public override void Up()
        {
            AddColumn("AssignedExams", "ExamStartDate", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("AssignedExams", "ExamStartDate");
        }
    }
}
