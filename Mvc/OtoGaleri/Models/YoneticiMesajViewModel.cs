using OtoGaleri_Entities.Tablolar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoGaleri.Models
{
    public class YoneticiMesajViewModel
    {
        public IEnumerable<Mesajlasma> mesaj { get; set; }
        public IEnumerable<Kullanicilar> kullanici { get; set; }
    }
}