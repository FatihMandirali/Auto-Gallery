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
   public class Mesajlasma
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Gönderen Kullanıcı")]
        public virtual Kullanicilar GonderenKullanici { get; set; }

        [DisplayName("AlanKullanici")]
        public string AlanKullanici { get; set; }

        [DisplayName("Mesaj")]
        public string Mesaj { get; set; }

        [DisplayName("Yanıt")]
        public string Yanit { get; set; }

        [DisplayName("Yönetici Sil")]
        public bool Ysil { get; set; } = false;

        [DisplayName("Kullanıcı Sil")]
        public bool Ksil { get; set; } = false;

        [DisplayName("Okundu Mu")]
        public bool Okundumu { get; set; } = false;

        [DisplayName("Kullanıcı Okundu Mu")]
        public bool Kokudumu { get; set; } = true;


        [DisplayName("Gönderme Tarihi")]
        public DateTime GondermeTarihi { get; set; }
    }
}
