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
    [Table("CalisanUcretleri")]
    public class CalisanUcretleri
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Adı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Adi { get; set; }
        [DisplayName("Soyadı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Soyadi { get; set; }
        [DisplayName("Tc"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string  Tc { get; set; }
        [DisplayName("Tc"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Telefon { get; set; }
        [DisplayName("Tc"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string EPosta { get; set; }
        [DisplayName("Ücreti"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int Ucret { get; set; }
        [DisplayName("Ücret Periyodu"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public UcretPeriyodu UcretPeriyodu { get; set; }
        [DisplayName("Ödemi Tarihi "), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DateTime OdemeTarihi { get; set; } = DateTime.Now;
       


    }
}
