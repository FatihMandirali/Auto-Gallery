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
    public class SifirSatilanAraba
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Araba"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public virtual YeniArabalar SifirArabalar { get; set; }

        [DisplayName("Satan Personel"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string SatanPersonel { get; set; }

        [DisplayName("Satılan Kullanıcı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public virtual Kullanicilar SatilanKullanici { get; set; }

        [DisplayName("Satılma Tarihi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DateTime SatilmaTarih { get; set; }

        [DisplayName("Satılma Fiyatı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int SatilmaUcret { get; set; }
    }
}
