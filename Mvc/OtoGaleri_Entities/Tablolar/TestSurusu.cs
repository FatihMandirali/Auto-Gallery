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
    [Table("TestSurusu")]
    public class TestSurusu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Arabalar"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Arabalar Araba { get; set; }
        [DisplayName("Randavu Tarihi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DateTime RandavTarihi { get; set; }
        [DisplayName("Randavu Saati"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public DateTime Saati { get; set; }//randavu saati almada hata olabilir......
        [DisplayName("Randavuyu Alan Kullanıcı"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public Kullanicilar AlanKullanici { get; set; }



    }
}
