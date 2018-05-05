using OtoGaleri_Entities.Tablolar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoGaleri.Models
{
    public class ViewModels
    {
        public IEnumerable<Arabalar> araba { get; set; }
        public IEnumerable<Kullanicilar> kullanici { get; set; }
    }
}