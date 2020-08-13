using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models.ViewModel
{
    public class UserHomeViewModel
    {
        public List<AssignedExam> AssignedExamList { get; set; }
        public List<Exam> ExamList { get; set; }
    }
}