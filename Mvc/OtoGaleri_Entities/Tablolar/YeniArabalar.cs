using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OtoGaleri_Entities.ArabalarEnums;
using OtoGaleri_Entities.IkincielEnums;

namespace OtoGaleri_Entities.Tablolar
{
    [Table("YeniArabalar")]
    public class YeniArabalar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Araba Numarası"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Arabalar Arabaid { get; set; }//arabalardan id alacaktık burası yanlış olabilir..

        [DisplayName("Durumu"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Durum Durum { get; set; }
    }
}
