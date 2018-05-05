using OtoGaleri_Entities.UcretEnums;
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
    [Table("CalismaGunleri")]
    public class CalismaGunleri
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Çalışan Hizmetli"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Hizmetliler Hizmetli { get; set; }
        [DisplayName("Çalıştığı Günler"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Gunler CalistigiGunler { get; set; }
    }
}
