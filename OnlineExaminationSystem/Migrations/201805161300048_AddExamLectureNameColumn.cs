namespace OnlineExaminationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExamLectureNameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("Exams", "ExamLectureName", c => c.String(nullable: false, maxLength: 100, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("Exams", "ExamLectureName");
        }
    }
}
