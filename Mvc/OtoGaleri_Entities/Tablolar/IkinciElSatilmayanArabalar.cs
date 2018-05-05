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
    [Table("IkinciElSatilmayanArabalar")]
    public class IkinciElSatilmayanArabalar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Araba"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public IkinciEl IkinciEller { get; set; }

    }
}
