using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models
{
    public class Answer
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public long QuestionId { get; set; }
        [Display(Name = "Kullanıcı Cevap")]
        [Required(ErrorMessage = "Kullanıcı cevap alanı boş olamaz"), MaxLength(50, ErrorMessage = "Ad alanı en fazla elli(50) karakter olmalıdır")]
        public string UserAnswer { get; set; }
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
    }
}