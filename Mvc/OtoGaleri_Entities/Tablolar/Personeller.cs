using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OtoGaleri_Entities.UcretEnums;

namespace OtoGaleri_Entities.Tablolar
{
    [Table("Personeller")]
    public class Personeller : Ortak123
    {
        [DisplayName("Ücreti"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int Ucret { get; set; }

        [DisplayName("Ücret Periyodu"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public UcretPeriyodu UcretPeriyodu { get; set; }
    }
}
