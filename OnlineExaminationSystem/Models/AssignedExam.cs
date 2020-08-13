using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models
{
    public class AssignedExam
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public long ExamId { get; set; }
        public bool IsActiv { get; set; }
        public DateTime? ExamStartDate { get; set; }
        public virtual User User { get; set; }
        public virtual Exam Exam { get; set; }
    }
}