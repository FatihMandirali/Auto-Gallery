using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OtoGaleri_Entities.Tablolar
{
    public class Ortak123
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Adınız"), Required(ErrorMessage = "{0} Alanı Gereklidir..."),
            StringLength(20, ErrorMessage = "Karakter Sınırını Aştınız..")]
        public string Adi { get; set; }

        [DisplayName("Soyadınız"), Required(ErrorMessage = "{0} Alanı Gereklidir..."),
            StringLength(30, ErrorMessage = "Karakter Sınırını Aştınız..")]
        public string Soyadi { get; set; }

        [DisplayName("Tc"), Required(ErrorMessage = "{0} Alanı Gereklidir..."),
            StringLength(11, ErrorMessage = "Karakter Sınırını Aştınız..")]
        public string Tc { get; set; }

        [DisplayName("E-Posta"), Required(ErrorMessage = "{0} Alanı Gereklidir...")]
        public string Eposta { get; set; }

        [StringLength(30), ScaffoldColumn(false)]  //admin false dediğimiz yerlere karışamıcak...
        public string Resim { get; set; }

        [Required]
        public string DogumTarih { get; set; } //İlerde hata çıkartabilir tarih eklemek......

        [Required]
        public string Telefon { get; set; }

        [Required]
        public DateTime KayitTarih { get; set; }

        [Required,ScaffoldColumn(false)]//scaffoldColumn ile bu satır edit sayfasında index sayfasında karşımıza gelmicek.
        public string KimKayitEtti { get; set; }

        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} Alanı Gereklidir...")]
        public string KullaniciAdi { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} Alanı Gereklidir...")]
        public string Sifre { get; set; }

        [DisplayName("Adres"), Required(ErrorMessage = "{0} Alanı Gereklidir...")]
        public string Adres { get; set; }

        [DisplayName("Aktiflik")]
        public bool IsActive { get; set; }

        public Guid AktiflikGuid { get; set; }
    }
}
