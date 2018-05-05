using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_Entities.ValueObject
{
   public class RegisterViewModel
    {
        [DisplayName("Adınız"), Required(ErrorMessage = "{0} alanı boş geçilemez.")
           , StringLength(25, ErrorMessage = "{0} max {1} karakterden oluşmalı.")]
        public string Adi { get; set; }

        [DisplayName("Soyadınız"), Required(ErrorMessage = "{0} alanı boş geçilemez.")
           , StringLength(25, ErrorMessage = "{0} max {1} karakterden oluşmalı.")]
        public string Soyad { get; set; }

        [DisplayName("Tc"), Required(ErrorMessage = "{0} alanı boş geçilemez.")
           , StringLength(11, ErrorMessage = "{0} max {1} karakterden oluşmalı.")]
        public string Tc { get; set; }

        [DisplayName("E-Posta"), Required(ErrorMessage = "{0} alanı boş geçilemez.")
           , StringLength(50, ErrorMessage = "{0} max {1} karakterden oluşmalı.")]
        public string EPosta { get; set; }

        [DisplayName("Doğum Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")
           , StringLength(50, ErrorMessage = "{0} max {1} karakterden oluşmalı.")]
        public string DogumTarihi { get; set; }

        [DisplayName("Telefonu"), Required(ErrorMessage = "{0} alanı boş geçilemez.")
           , StringLength(25, ErrorMessage = "{0} max {1} karakterden oluşmalı.")]
        public string Telefon { get; set; }

        [DisplayName("Adres"), Required(ErrorMessage = "{0} alanı boş geçilemez.")
           , StringLength(50, ErrorMessage = "{0} max {1} karakterden oluşmalı.")]
        public string Adres { get; set; }

        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")
            , StringLength(25, ErrorMessage = "{0} max {1} karakterden oluşmalı.")]
        public string UserName { get; set; }

       

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password),
           StringLength(25, ErrorMessage = "{0} max {1} karakterden oluşmalı.")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password),
           StringLength(25, ErrorMessage = "{0} max {1} karakterden oluşmalı.")
            , Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor")]
        public string RePassword { get; set; }
    }
}
