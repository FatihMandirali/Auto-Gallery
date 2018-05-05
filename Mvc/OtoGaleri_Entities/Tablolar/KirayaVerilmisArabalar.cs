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
    [Table("KirayaVerilmisArabalar")]
    public class KirayaVerilmisArabalar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Araba"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public virtual KiralikArabalar KiralikAraba { get; set; }

        [DisplayName("Kiralama Tarihi")]
        public DateTime KiralamaTarih { get; set; }

        [DisplayName("Kiradan Dönüş Tarihi"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]                                           
        public DateTime KiradanAlamaTarih { get; set; }

        [DisplayName("Alınacak Ücret"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public int AlinacakUcret { get; set; }

        [DisplayName("Hangi Kullanıcıda"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public virtual Kullanicilar HangiKullanici { get; set; }

        [DisplayName("İlgili Personel"), Required(ErrorMessage = "Lütfen Bu Alanı Boş Bırakmayın.")]
        public string KiralayanPersonel { get; set; }

        public bool IslemAktiflik { get; set; } = true;

    }
}
