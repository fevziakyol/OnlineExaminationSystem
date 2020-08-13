using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models.ViewModel
{
    public class TestViewModel
    {
        public List<Question> QuestionList { get; set; }
        public Question Question { get; set; }
        public Exam Exam { get; set; }
    }
}