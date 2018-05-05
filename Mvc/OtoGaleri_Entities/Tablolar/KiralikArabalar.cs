using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OtoGaleri_Entities.IkincielEnums;

namespace OtoGaleri_Entities.Tablolar
{
    [Table("KiralikArabalar")]
    public class KiralikArabalar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Araba Numarası"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Arabalar Arabaid { get; set; }//arabalardan id alacaktık burası yanlış olabilir..

        [DisplayName("Günlük Ücreti"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int GunlukUcret { get; set; }//silmek lazm çnkü arabalar tablosunda zaten giriyoruz degerini....

        [DisplayName("Durumu"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DurumKiralandi Durum { get; set; }

    }
}
