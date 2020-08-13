using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models.ViewModel
{
    public class MergedLessonExam
    {
        public List<Lesson> LessonList { get; set; }
        public List<Exam> ExamList { get; set; }
    }
}