using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models
{
    public class Question : IEnumerable
    {
        public long Id { get; set; }
        public long LessonId { get; set; }
        public long ExamId { get; set; }
        [Display(Name = "Soru")]
        [Required(ErrorMessage = "Soru alanı boş olamaz"), MaxLength(int.MaxValue)]
        public string QuestionText { get; set; }
        [Display(Name = "A Şıkkı")]
        [Required(ErrorMessage = "A şıkkı alanı boş olamaz"), MaxLength(int.MaxValue)]
        public string ChoiceA { get; set; }
        [Display(Name = "B Şıkkı")]
        [Required(ErrorMessage = "B şıkkı alanı boş olamaz"), MaxLength(int.MaxValue)]
        public string ChoiceB { get; set; }
        [Display(Name = "C Şıkkı")]
        [Required(ErrorMessage = "C şıkkı alanı boş olamaz"), MaxLength(int.MaxValue)]
        public string ChoiceC { get; set; }
        [Display(Name = "D Şıkkı")]
        [Required(ErrorMessage = "D şıkkı alanı boş olamaz"), MaxLength(int.MaxValue)]
        public string ChoiceD { get; set; }
        [Display(Name = "Doğru Cevap")]
        [Required(ErrorMessage = "Doğru cevap alanı boş olamaz"), MaxLength(50, ErrorMessage = "Ad alanı en fazla elli(50) karakter olmalıdır")]
        public string Answer { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Exam Exam { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}