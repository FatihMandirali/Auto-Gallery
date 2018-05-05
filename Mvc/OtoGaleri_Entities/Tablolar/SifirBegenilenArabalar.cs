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
    [Table("SifirBegenilenArabalar")]
    public class SifirBegenilenArabalar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Beğenen Kullanıcı"), Required]
        public virtual Kullanicilar Kullanici { get; set; }

        [DisplayName("Beğenilen Araba"), Required]
        public virtual Arabalar Araba { get; set; }

    }
}
