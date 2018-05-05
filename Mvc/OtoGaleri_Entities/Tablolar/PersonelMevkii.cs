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
    [Table("PersonelMevkii")]
    public class PersonelMevkii
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Personel Adı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Personeller PersonelAdi { get; set; }

        [DisplayName("Pozisyonu"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string Pozisyon { get; set; }

        [DisplayName("Ucreti"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int Ucret { get; set; }

        [DisplayName("Puanı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int Puan { get; set; }
    }
}
