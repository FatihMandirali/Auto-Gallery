using OtoGaleri_Common.Helpers;
using OtoGaleri_Entities.Tablolar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoGaleri.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUserName()
        {
            if (HttpContext.Current.Session["login"] != null)
            {
                Ortak123 k = HttpContext.Current.Session["login"] as Ortak123;
                return k.KullaniciAdi;
            }
            return "system";
        }
    }
}