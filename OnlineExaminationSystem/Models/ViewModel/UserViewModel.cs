using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models.ViewModel
{
    public class UserViewModel
    {
        public int? ID { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı alanı zorunludur!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kullanıcı soyadı alanı zorunludur!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Kullanıcı öğrenci no alanı zorunludur!")]
        public long StudentNumber { get; set; }
        [Required(ErrorMessage = "Kullanıcı bölüm alanı zorunludur!")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Kullanıcı e-mail alanı zorunludur!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Kullanıcı şifre alanı zorunludur!")]
        public string Password { get; set; }
    }
}