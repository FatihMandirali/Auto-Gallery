using OtoGaleri.ViewModels;
using OtoGaleri_BusinessLayer;
using OtoGaleri_DataAccessLayer.Entity_Framework;
using OtoGaleri_Entities.Tablolar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace OtoGaleri.Controllers
{
    public class EkonomiController : Controller
    {
        PersonelManager p_manager = new PersonelManager();
        HizmetlilerManager h_manager = new HizmetlilerManager();
        CalisanUcretleriManager c_manager = new CalisanUcretleriManager();
        CalisanUcretleriControlManager ck_manager = new CalisanUcretleriControlManager();
        FaturalarManager f_manager = new FaturalarManager();
        FaturaTurManager ft_manager = new FaturaTurManager();
        DigerMasraflarManager dm_manager = new DigerMasraflarManager();
        DigerMasrafTurleriManager dmt_manager = new DigerMasrafTurleriManager();
        SifirSatilanArabaManager ss_manager = new SifirSatilanArabaManager();
        KirayaVerilmisArabalarManager kv_manager = new KirayaVerilmisArabalarManager();
        IkinciElSatilmisArabalarManager is_manager = new IkinciElSatilmisArabalarManager();
         int personelaylik;

      
      
        // GET: Ekonomi
        public ActionResult Masraflar()
        {
            if (DateTime.Now.Day != 22)
            {
                int idsi = ck_manager.List().Max(x => x.Id);
                CalisanUcretleriControl ck = ck_manager.Find(x => x.Id == idsi);
                ck.OdemeYapildimi = false;
                ck_manager.Update(ck);
            }
            int personelkiralik = p_manager.List().Sum(x => x.Ucret);
            TempData["PersonelAylik"] = personelkiralik;

            int hizmetlikiralik = h_manager.List().Sum(x => x.Ucret);
            TempData["HizmetliAylik"] = hizmetlikiralik;

            int toplampara = personelkiralik + hizmetlikiralik;
            TempData["ToplamPara"] = toplampara;
            //if (c_manager.List().Count() == 0)
            //{
            //    OkViewModel notifyobj = new OkViewModel()
            //    {

            //        Title = "İçerik Olmadığı için yüklenemedi...",
            //        RedirectingUrl = "/Home/Index",

            //    };
            //    notifyobj.Items.Add("Şuana kadar çalışanların aylık zamanı gelmediği için içerik yoktur.");
            //    return View("Ok", notifyobj);
            //}
            //else
            return View(c_manager.List());
        }

    

        [HttpPost, ActionName("Masraflar")]
        public ActionResult MasraflarPostt()
        {
           
            int idsi = ck_manager.List().Max(x => x.Id);
            CalisanUcretleriControl ck = ck_manager.Find(x => x.Id == idsi);
            if (ck.OdemeYapildimi == false)
            {

                ck.OdemeYapildimi = true;
                ck_manager.Update(ck);

                foreach (var item in p_manager.List())
                {
                    CalisanUcretleri calisan = new CalisanUcretleri();
                    calisan.Adi = item.Adi;
                    calisan.Soyadi = item.Soyadi;
                    calisan.Tc = item.Tc;
                    calisan.Telefon = item.Telefon;
                    calisan.EPosta = item.Eposta;
                    calisan.Ucret = item.Ucret;
                    calisan.UcretPeriyodu = item.UcretPeriyodu;
                    c_manager.Insert(calisan);
                    personelaylik += calisan.Ucret;

                }
                foreach (var item in h_manager.List())
                {
                    CalisanUcretleri calisan = new CalisanUcretleri();
                    calisan.Adi = item.Adi;
                    calisan.Soyadi = item.Soyadi;
                    calisan.Tc = item.Tc;
                    calisan.Telefon = item.Telefon;
                    calisan.EPosta = "Gerek Yok";
                    calisan.Ucret = item.Ucret;
                    calisan.UcretPeriyodu = item.UcretPeriyodu;
                    c_manager.Insert(calisan);

                }

            }
           

            return RedirectToAction("Masraflar","Ekonomi") ;
            
        }

      public ActionResult FaturaGor()
        {

            int digertumfatura = f_manager.List().Sum(x => x.Tutar) + f_manager.List().Sum(x => x.GecikmeTutar);
            int aylikfatura = f_manager.List(x => x.OdemeTarihi > DateTime.Now).Sum(x => x.Tutar) + f_manager.List(x => x.OdemeTarihi > DateTime.Now).Sum(x => x.GecikmeTutar);
            //bu işlemde hata olabilir 1 ay öncesini aldım     .AddMonths(-1) vardı hata veriyor onu düzelt...
            TempData["digertumfatura"] = digertumfatura;
            TempData["aylikfatura"] = aylikfatura;
            return View(f_manager.List()) ;
        }


        public ActionResult FaturaTuru()
        {


            return View();
        }
        public ActionResult FaturaTuruGor()
        {
           

            return View(ft_manager.List());
        }
        [HttpPost]
        public ActionResult FaturaTuru(FaturaTur model)
        {
            if (ModelState.IsValid)
            {
                ft_manager.Insert(model);
                return RedirectToAction("FaturaGor", "Ekonomi");
            }
            return View();
        }

        public ActionResult FaturaEkle()
        {
            List<FaturaTur> faturalar=ft_manager.List();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var item in faturalar)
            {
               
                 list.Add(new SelectListItem() {Text=item.FaturaAdi,Value=item.FaturaAdi });
            }
         
            ViewBag.Fatura = list;

            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Faturalar model)
        {
            try
            {
                f_manager.Insert(model);
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Fatura Ekleme",
                    RedirectingUrl = "/Ekonomi/FaturaGor",

                };
                notifyobj.Items.Add("Fatura Ekleme işleminiz başarılı bir şekilde gerçekleşmiştir.");
                return View("Ok", notifyobj);
            }
            catch (Exception)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                };
                return View("Error", notifyonj);
            }

           
        }
        public ActionResult FaturaOde(int? id)
        {
            Faturalar f = f_manager.Find(x => x.Id == id);

            return View(f);
        }
        [HttpPost]
        public ActionResult FaturaOde(Faturalar model)
        {
            try
            {
             Faturalar f= f_manager.Find(x => x.Id == model.Id);
            f.Odendimi = true;
            f_manager.Update(f);
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Fatura Ödendi",
                    RedirectingUrl = "/Ekonomi/FaturaGor",

                };
                notifyobj.Items.Add("Fatura Ödeme işleminiz başarılı bir şekilde gerçekleşmiştir.");
                return View("Ok", notifyobj);
            }
            catch (Exception)
            {

                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                };
                return View("Error", notifyonj);
            }
          

        }

        public ActionResult DigerMasraflar()
        {
            int digertummasraf = dm_manager.List().Sum(x => x.Tutar);
            int aylikmasraf = dm_manager.List(x => x.OdemeTarihi > DateTime.Now).Sum(x => x.Tutar);//bu işlemde hata olabilir 1 ay öncesini aldım
            TempData["digertummasraf"] = digertummasraf;
            TempData["aylikmasraf"] = aylikmasraf;

            return View(dm_manager.List());
        }
        public ActionResult DigerMasraflarEkle()
        {

            List<DigerMasrafTurleri> faturalar = dmt_manager.List();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in faturalar)
            {

                list.Add(new SelectListItem() { Text = item.MasrafAdi, Value = item.MasrafAdi });
            }

            ViewBag.Fatura = list;
            return View();
        }
        [HttpPost]
        public ActionResult DigerMasraflarEkle(DigerMasraflar model)
        {

            try
            {
                dm_manager.Insert(model);
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Diğer Masraf Ekleme",
                    RedirectingUrl = "/Ekonomi/DigerMasraflar",

                };
                notifyobj.Items.Add("Diğer Masraf Ekleme işleminiz başarılı bir şekilde gerçekleşmiştir.");
                return View("Ok", notifyobj);
            }
            catch (Exception)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                };
                return View("Error", notifyonj);
            }

        }
        public ActionResult DigerMasraflarDetay(int? id)
        {
            DigerMasraflar d = dm_manager.Find(x => x.Id == id);
            return View(d);
        }

        [HttpPost]
        public ActionResult DigerMasraflarDetay(DigerMasraflar model)
        {
            try
            {
                DigerMasraflar f = dm_manager.Find(x => x.Id == model.Id);
                f.Odendimi = true;
                dm_manager.Update(f);
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Fatura Ödendi",
                    RedirectingUrl = "/Ekonomi/DigerMasraflar",

                };
                notifyobj.Items.Add("Diğer Masraf Ödeme işleminiz başarılı bir şekilde gerçekleşmiştir.");
                return View("Ok", notifyobj);
            }
            catch (Exception)
            {

                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                };
                return View("Error", notifyonj);
            }

        }

        public ActionResult DigerMasrafTuruEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DigerMasrafTuruEkle(DigerMasrafTurleri model)
        {

            if (ModelState.IsValid)
            {
                dmt_manager.Insert(model);
                return RedirectToAction("DigerMasrafTuru", "Ekonomi");
            }
            return View();
        }

        public ActionResult DigerMasrafTuru()
        {


            return View(dmt_manager.List());
        }

        public ActionResult TumMasraflar()
        {
            int digertummasraf1 = dm_manager.List().Sum(x => x.Tutar);
            int digertumfatura1 = f_manager.List().Sum(x => x.Tutar) + f_manager.List().Sum(x => x.GecikmeTutar);

            int personelkiralik = c_manager.List().Sum(x => x.Ucret);
            
            TempData["toplamfaturalar"] = digertumfatura1;
            TempData["toplamdigermasraf"] = digertummasraf1;
            TempData["toplamcalisan"] = personelkiralik;
            TempData["toplammasraf"] = personelkiralik+digertumfatura1+digertummasraf1;
            return View();
        }

        public ActionResult FaturalarGraph()
        {           
            var context =new DatabaseContext();

            var query = from c in context.Faturalar
                        group c by c.FaturaTuru.FaturaAdi
                      into g
                        select g;

            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            foreach(var group in query)
            {
                xvalue.Add(group.Key);
                yvalue.Add(group.Sum(x=>x.Tutar)+group.Sum(x=>x.GecikmeTutar));
            }
            new Chart(width: 700, height: 300).AddSeries("Default",
                chartType: "column",
                xValue:xvalue,
                yValues:yvalue).AddTitle("Faturalar Grafiği").Write("bmp");

            return null;
        }
        public ActionResult DigerMasraflarGraph()
        {
            var context = new DatabaseContext();

            var query = from c in context.DigerMasraflar
                        group c by c.MasrafTuru.MasrafAdi
                      into g
                        select g;

            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            foreach (var group in query)
            {
                xvalue.Add(group.Key);
                yvalue.Add(group.Sum(x => x.Tutar));
            }
            new Chart(width: 700, height: 300).AddSeries("Default",
                chartType: "column",
                xValue: xvalue,
                yValues: yvalue).AddTitle("Diğer Masraflar Grafiği").Write("bmp");

            return null;
        }

        public ActionResult CalisanOdemeleriGraph()
        {
            var context = new DatabaseContext();

            var query = from c in context.CalisanUcretleri
                        group c by c.Tc
                      into g
                        select g;

            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            foreach (var group in query)
            {
                xvalue.Add(group.Key);
                yvalue.Add(group.Sum(x => x.Ucret));
            }
            new Chart(width: 700, height: 300).AddSeries("Default",
                chartType: "column",
                xValue: xvalue,
                yValues: yvalue).AddTitle("Çalışan Ödemeleri Grafiği").Write("bmp");

            return null;
        }

        public ActionResult KazanclariListele()
        {
            int sifirarabakazanc = ss_manager.List().Sum(x => x.SatilmaUcret);

            int kiralikarabakazanc = kv_manager.List(x=>x.IslemAktiflik==false).Sum(x => x.AlinacakUcret);

            int ikincielarabakazanc = is_manager.List().Sum(x => x.SatilanUcret);

            TempData["sifirarabakazanc"] = sifirarabakazanc;
            TempData["kiralikarabakazanc"] = kiralikarabakazanc;
            TempData["ikincielarabakazanc"] = ikincielarabakazanc;
            TempData["toplamarabakazanc"] = kiralikarabakazanc+sifirarabakazanc+ikincielarabakazanc;
            return View();
        }

        public ActionResult SifirArabaGraph()
        {
            var context = new DatabaseContext();

            var query = from c in context.SifirSatilanArabalar
                        group c by c.SifirArabalar.Arabaid.Marka
                      into g
                        select g;

            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            foreach (var group in query)
            {
                xvalue.Add(group.Key);
                yvalue.Add(group.Sum(x => x.SatilmaUcret));
            }
            new Chart(width: 700, height: 300).AddSeries("Default",
                chartType: "column",
                xValue: xvalue,
                yValues: yvalue).AddTitle("Sıfır Arabalar Grafiği").Write("bmp");


            return null;
        }

        public ActionResult KiralikArabaGraph()
        {
            var context = new DatabaseContext();

            var query = from c in context.KirayaVerilmisArabalar where c.IslemAktiflik==false
                        group c by c.KiralikAraba.Arabaid.Marka 
                      into g
                        select g;

            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            foreach (var group in query)
            {
                xvalue.Add(group.Key);
                yvalue.Add(group.Sum(x => x.AlinacakUcret));
            }
            new Chart(width: 700, height: 300).AddSeries("Default",
                chartType: "column",
                xValue: xvalue,
                yValues: yvalue).AddTitle("Kiralık Arabalar Grafiği").Write("bmp");


            return null;
        }

        public ActionResult IkinciElArabaGraph()
        {
            var context = new DatabaseContext();

            var query = from c in context.IkinciElSatilmisArabalar
                        
                        group c by c.IkinciElArabalar.Arabaid.Marka
                      into g
                        select g;

            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            foreach (var group in query)
            {
                xvalue.Add(group.Key);
                yvalue.Add(group.Sum(x => x.SatilanUcret));
            }
            new Chart(width: 700, height: 300).AddSeries("Default",
                chartType: "column",
                xValue: xvalue,
                yValues: yvalue).AddTitle("İkinci El Araba Satışı").Write("bmp");


            return null;
        }
        public ActionResult GenelDurum()
        {
            int sifirarabakazanc = ss_manager.List().Sum(x => x.SatilmaUcret);
            int kiralikarabakazanc = kv_manager.List(x => x.IslemAktiflik == false).Sum(x => x.AlinacakUcret);
            int ikincielarabakazanc = is_manager.List().Sum(x => x.SatilanUcret);

            TempData["ToplamKazanc"] = kiralikarabakazanc + sifirarabakazanc + ikincielarabakazanc;

            int digertummasraf1 = dm_manager.List().Sum(x => x.Tutar);
            int digertumfatura1 = f_manager.List().Sum(x => x.Tutar) + f_manager.List().Sum(x => x.GecikmeTutar);
            int personelkiralik = c_manager.List().Sum(x => x.Ucret);

            TempData["ToplamMasraf"] = personelkiralik + digertumfatura1 + digertummasraf1;
            int bakiye = (kiralikarabakazanc + sifirarabakazanc + ikincielarabakazanc) - (personelkiralik + digertumfatura1 + digertummasraf1);
            TempData["Bakiye"] = bakiye;
            return View();
        }

        public ActionResult GenelDurumGraph()
        {
            //var context = new DatabaseContext();

            //var query = from c in context.IkinciElSatilmisArabalar

            //            group c by c.IkinciElArabalar.Arabaid.Marka
            //          into g
            //            select g;
            int sifirarabakazanc = ss_manager.List().Sum(x => x.SatilmaUcret);
            int kiralikarabakazanc = kv_manager.List(x => x.IslemAktiflik == false).Sum(x => x.AlinacakUcret);
            int ikincielarabakazanc = is_manager.List().Sum(x => x.SatilanUcret);

            int toplamkaznc = kiralikarabakazanc + sifirarabakazanc + ikincielarabakazanc;

            int digertummasraf1 = dm_manager.List().Sum(x => x.Tutar);
            int digertumfatura1 = f_manager.List().Sum(x => x.Tutar) + f_manager.List().Sum(x => x.GecikmeTutar);
            int personelkiralik = c_manager.List().Sum(x => x.Ucret);

            int toplammasraf = personelkiralik + digertumfatura1 + digertummasraf1;
            int []dizi = { toplamkaznc, toplammasraf };
            string[] dizi1 = { "Toplam Kazanç = " + toplamkaznc, "Toplam Masraf = " + toplammasraf };
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            foreach (var group in dizi)
            {
             //  xvalue.Add("Toplam Kazanç");
                yvalue.Add(group);
            }
            foreach (var group in dizi1)
            {
                  xvalue.Add(group);
               // yvalue.Add(group);
            }
            new Chart(width: 600, height: 400, theme:ChartTheme.Green).AddSeries("Default",
                chartType: "pie",
                xValue: xvalue,
                yValues: yvalue).AddTitle("Genel Durum Grafiği").Write("bmp");


            return null;
        }

    }
}          
