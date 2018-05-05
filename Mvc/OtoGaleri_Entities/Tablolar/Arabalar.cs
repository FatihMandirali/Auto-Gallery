using OtoGaleri_Entities.ArabalarEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleri_Entities.Tablolar
{
    [Table("Arabalar")]
    public class Arabalar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("İlan Tarihi"), Required]
        public DateTime IlanTarihi { get; set; }

        [DisplayName("Marka"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Marka { get; set; }

        [DisplayName("Model"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Model { get; set; }

        [DisplayName("Yıl"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int Yil { get; set; }

        [DisplayName("Yakıt"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Yakit Yakit { get; set; } //

        [DisplayName("Vites"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Vites Vites { get; set; }//

        [DisplayName("Garanti"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Garanti { get; set; }

        [DisplayName("Kasa Tipi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public KasaTipi Kasatipi { get; set; }//

        [DisplayName("Motor Gücü"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string MotorGucu { get; set; }

        [DisplayName("Motor Hacmi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string MotorHacmi { get; set; }

        [DisplayName("Çekiş"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Cekis Cekis { get; set; }

        [DisplayName("Renk"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Renk { get; set; }

        [DisplayName("Arabayı Ekleyen"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string ArabayiEkleyen { get; set; }

        [DisplayName("Fiyat"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int Fiyat { get; set; }

        [DisplayName("Eklenme Tarih"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DateTime EklenmeTarihi { get; set; }

       
        public string Resim1 { get; set; }
        
        public string Resim2 { get; set; }
        
        public string Resim3 { get; set; }

        [DisplayName("Durumu"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Durum2 Durum { get; set; }//

        [DisplayName("Tipi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Tipi Tipi { get; set; }//

        [DisplayName("Açıklama"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Aciklama { get; set; }

        [DisplayName("İşlem Yapabilme")]  //burası eklendi
        public bool IslemYapabilme { get; set; } = true;
    }
}
