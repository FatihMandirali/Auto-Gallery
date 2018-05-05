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
    [Table("IkinciElSatilmisArabalar")]
    public class IkinciElSatilmisArabalar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Araba"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public virtual IkinciEl IkinciElArabalar { get; set; }

        [DisplayName("Satan Personel"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string SatanPersonel { get; set; }

        [DisplayName("Satılan KuLLanıcı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public virtual Kullanicilar SatilanKullanici { get; set; }

        [DisplayName("Satılma Tarihi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DateTime SatilmaTarih { get; set; }

        [DisplayName("Satılma Ücreti"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int SatilanUcret { get; set; }
    }
}
