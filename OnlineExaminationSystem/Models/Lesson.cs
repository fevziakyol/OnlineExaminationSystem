using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models
{
    public class Lesson
    {
        public long Id { get; set; }
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Ders adı alanı boş olamaz"), MaxLength(50, ErrorMessage = "Ad alanı en fazla elli(50) karakter olmalıdır")]
        public string LessonName { get; set; }
    }
}