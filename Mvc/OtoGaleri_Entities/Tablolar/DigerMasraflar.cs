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
    [Table("DigerMasraflar")]
    public class DigerMasraflar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Masraf Tutarı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int Tutar { get; set; }

        [DisplayName("Ödeme Tarihi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DateTime OdemeTarihi { get; set; }

        [DisplayName("Masraf Türü"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public virtual DigerMasrafTurleri MasrafTuru { get; set; }

        public bool Odendimi { get; set; } = false;
    }
}
