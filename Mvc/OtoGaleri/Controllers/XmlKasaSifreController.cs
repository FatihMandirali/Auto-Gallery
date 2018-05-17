using OtoGaleri.Utils;
using OtoGaleri_Entities.XmlTablo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace OtoGaleri.Controllers
{
    public class XmlKasaSifreController : BaseController
    {
        // GET: XmlKasaSifre
        public ActionResult GosterXML()
        {
            //XElement yeni = new XElement("Tablo", new XElement("ID", 3), new XElement("Sifre", "SARIKIRMIZI"));
            //XDocument doc = XDocument.Load(Server.MapPath("~/XMLFile/KasaSifre.xml"));
            //doc.Element("Tablo").Add(yeni);
            //doc.Save(Server.MapPath("~/XMLFile/KasaSifre.xml")); Yeni xml ekleme kodu


            var data = new List<KasaSifresi>();

            data = VerileriDondur();
            return View(data);
        }

        private List<KasaSifresi> VerileriDondur()
        {
            string xmldata = Server.MapPath("~/XMLFile/KasaSifre.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(xmldata);

            var list = new List<KasaSifresi>();
            list = (from rows in ds.Tables[0].AsEnumerable()
                    select new KasaSifresi
                    {
                        ID=Convert.ToInt32(rows[0].ToString()),
                        Sifre=rows[1].ToString()

                    }).ToList();
            return list;
        }
    }
}