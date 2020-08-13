namespace OnlineExaminationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionLessonAndExamTablesCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Lessons",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LessonName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Questions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LessonId = c.Long(nullable: false),
                        ExamId = c.Long(),
                        QuestionText = c.String(nullable: false, unicode: false),
                        ChoiceA = c.String(nullable: false, unicode: false),
                        ChoiceB = c.String(nullable: false, unicode: false),
                        ChoiceC = c.String(nullable: false, unicode: false),
                        ChoiceD = c.String(nullable: false, unicode: false),
                        Answer = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Exams", t => t.ExamId)
                .ForeignKey("Lessons", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.LessonId)
                .Index(t => t.ExamId);
            
            CreateTable(
                "Exams",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LessonId = c.Long(nullable: false),
                        ExamName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        ExamQuestionNumber = c.Long(nullable: false),
                        ExamTime = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Lessons", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.LessonId);
            Sql("INSERT INTO Users (Name, Surname, Email, Password, IsAdmin,StudentNumber,Department) VALUES('Admin', 'User', 'admin@ybu.com', '202cb962ac59075b964b07152d234b70', '1', '1', 'Default')");//password: 123 ADMÝN
            Sql("INSERT INTO Users (Name, Surname, Email, Password, IsAdmin,StudentNumber,Department) VALUES('User', 'User', 'user@ybu.com', '202cb962ac59075b964b07152d234b70', '0', '1305012000', 'Default')");//password: 123 USER
            Sql("INSERT INTO Users (Name, Surname, Email, Password, IsAdmin,StudentNumber,Department) VALUES('Fevzi', 'Akyol', 'fevziakyol1994@gmail.com', '202cb962ac59075b964b07152d234b70', '0', '1305012004', 'Computer Engineering')");//password: 123 Fevzi Akyol

            
        }
        
        public override void Down()
        {
            DropForeignKey("Questions", "LessonId", "Lessons");
            DropForeignKey("Questions", "ExamId", "Exams");
            DropForeignKey("Exams", "LessonId", "Lessons");
            DropIndex("Exams", new[] { "LessonId" });
            DropIndex("Questions", new[] { "ExamId" });
            DropIndex("Questions", new[] { "LessonId" });
            DropTable("Exams");
            DropTable("Questions");
            DropTable("Lessons");
        }
    }
}
