using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models
{
    public class Exam
    {
        public long Id { get; set; }
        public long LessonId { get; set; }
        [Display(Name = "Sınav Adı")]
        [Required(ErrorMessage = "Sınav adı alanı boş olamaz"), MaxLength(50, ErrorMessage = "Ad alanı en fazla elli(50) karakter olmalıdır")]
        public string ExamName { get; set; }
        [Display(Name = "Sınavın Hocası")]
        [Required(ErrorMessage = "Sınavın hocası alanı boş olamaz"), MaxLength(100, ErrorMessage = "Sınavın hocası alanı en fazla elli(100) karakter olmalıdır")]
        public string ExamLectureName { get; set; }
        [Display(Name = "Sınav Soru Sayısı")]
        public long ExamQuestionNumber { get; set; }
        [Display(Name = "Sınav Süresi(dk)")]
        public long ExamTime { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}