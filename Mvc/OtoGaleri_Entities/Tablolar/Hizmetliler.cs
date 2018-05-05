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
    [Table("Hizmetliler")]
    public class Hizmetliler
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Adı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Adi { get; set; }

        [DisplayName("Soyadı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Soyadi { get; set; }

        [DisplayName("Görevi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Görevi { get; set; }

        [DisplayName("Tc"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Tc { get; set; }

        [DisplayName("Telefonu"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Telefon { get; set; }

        [DisplayName("Adres"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Acres { get; set; }

        [DisplayName("Ekleyen Personel"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string EkleyenPersonel { get; set; }

        [DisplayName("Eklenme Tarihi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DateTime EklenmeTarihi { get; set; }

        [DisplayName("Ücreti"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int Ucret { get; set; }

        [DisplayName("Ücret Periyodu"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public UcretPeriyodu UcretPeriyodu { get; set; }


    }
}
