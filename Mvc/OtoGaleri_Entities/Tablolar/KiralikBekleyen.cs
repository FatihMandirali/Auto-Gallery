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
    [Table("KiralikBekleyenAraba")]
    public class KiralikBekleyen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Araba"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public KiralikArabalar KiralikAraba { get; set; }
    }
}
