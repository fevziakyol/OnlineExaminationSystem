using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineExaminationSystem.Models
{
    public class User
    {

        public long ID { get; set; }
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Ad alanı boş olamaz"), MaxLength(50, ErrorMessage = "Ad alanı en fazla elli(50) karakter olmalıdır")]
        public string Name { get; set; }
        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Soyad alanı boş olamaz"), MaxLength(50, ErrorMessage = "Soyad alanı en fazla elli(50) karakter olmalıdır")]
        public string Surname { get; set; }
        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "E-posta alanı boş olamaz"), MaxLength(50, ErrorMessage = "E-posta alanı en fazla elli(50) karakter olmalıdır")]
        public string Email { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş olamaz"), MinLength(3, ErrorMessage = "Şifre alanı beş(5) karakterden fazla olmalıdır"), MaxLength(50, ErrorMessage = "Şifre alanı elli(50) karakterden fazla olamaz")]
        public string Password { get; set; }
        [Display(Name = "Öğrenci No")]
        [Required(ErrorMessage = "Öğrenci alanı boş olamaz")]
        public long StudentNumber { get; set; }
        [Display(Name = "Bölüm")]
        [Required(ErrorMessage = "Bölüm alanı boş olamaz"), MaxLength(50, ErrorMessage = "Bölüm alanı en fazla elli(50) karakter olmalıdır")]
        public string Department { get; set; }

        public bool IsAdmin { get; set; }
    }
}