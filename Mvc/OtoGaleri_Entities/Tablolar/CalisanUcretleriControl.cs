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
    [Table("CalisanUcretleriControl")]
    public class CalisanUcretleriControl
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Ödeme Yapıldımı")]
        public bool OdemeYapildimi { get; set; } = false;
    }
}
