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

    [Table("Gelirler")]
    public  class Gelirler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }

        [DisplayName("Satılan Kullanıcı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public virtual Kullanicilar Kullanici { get; set; }

        [DisplayName("Satılan Araba"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public virtual Arabalar Araba { get; set; }

        [DisplayName("Satılan Fiyat"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int Fiyat { get; set; }

        [DisplayName("Satılma Tarihi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DateTime Tarih { get; set; }

        [DisplayName("Satan Personel"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Personel { get; set; }

        [DisplayName("Satılan Arabanın Durumu"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public OtoGaleri_Entities.ArabalarEnums.Durum2 ArabaninDurumu { get; set; }

    }
}
