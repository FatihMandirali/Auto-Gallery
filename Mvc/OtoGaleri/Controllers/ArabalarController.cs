using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using OtoGaleri_Entities.Tablolar;
using OtoGaleri_BusinessLayer;
using OtoGaleri_BusinessLayer.Result;
using OtoGaleri.ViewModels;
using OtoGaleri.Utils;

namespace OtoGaleri.Controllers
{
    public class ArabalarController : CarController
    {
      private ArabalarManager a = new ArabalarManager();
      private YeniArabalarManager yenia = new YeniArabalarManager();
      private  SifirBegenilenArabalarManager begenilenler = new SifirBegenilenArabalarManager();
      private  SifirSatilmayanArabalarManager satilmayanaraba = new SifirSatilmayanArabalarManager();
      private KiralikArabalarManager kiralikmanager = new KiralikArabalarManager();
        private SifirSatilanArabaManager sifirsatilan = new SifirSatilanArabaManager();
        private KullaniciManager kma = new KullaniciManager();
        private KiralikBekleyenManager kiralik_bekleyen = new KiralikBekleyenManager();
        private IkinciElManager ikincielarabalar = new IkinciElManager();
        private KirayaVerilmisArabalarManager kirayaverilmismanager = new KirayaVerilmisArabalarManager();
        private IkinciElSatilmisArabalarManager ikincielsatilmisarabalarmanager = new IkinciElSatilmisArabalarManager();
        private IkinciElSatilmayanArabalarManager ikincielsatilmayanarabamanager = new IkinciElSatilmayanArabalarManager();
        private GelirlerManager gelirmanager = new GelirlerManager();
        private KullaniciManager kullanicimanager = new KullaniciManager();
        private YoneticiManager yöneticiManager = new YoneticiManager();
        
        // GET: Arabalar
        public ActionResult Index()
        {
            


            return View(a.List());
        }
        
     

        // GET: Arabalar/Details/5
        public ActionResult Details(int? id)
        {
          
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Arabalar arabalar = a.Find(x => x.Id == id.Value);
            if (arabalar.Durum == OtoGaleri_Entities.ArabalarEnums.Durum2.IkinciEl)
            {
                IkinciEl ikinciel = ikincielarabalar.Find(x => x.Arabaid.Id == arabalar.Id);
                int a = ikinciel.Km;
                TempData["km"] = a;
            }
                if (arabalar == null)
                {
                    return HttpNotFound();
                }

                return View(arabalar);
          
        }
    
        public ActionResult BegenilenAraba(int? id)
        {
            ModelState.Remove("Araba");
            if (ModelState.IsValid)
            {
                Kullanicilar ku = Session["logink"] as Kullanicilar;
                if (id == null)
                {
                    return View(begenilenler.List(x => x.Kullanici.Id == ku.Id));
                }
                
                SifirBegenilenArabalar ba = new SifirBegenilenArabalar();
                Arabalar aa = a.Find(x => x.Id == id.Value);
             //   KiralikArabalar kk = kiralikmanager.Find(x => x.Id == id.Value);
                if (Session["logink"] != null && aa!=null )
               {
                    //ba = begenilenler.Find(x => x.Araba == aa && x.Kullanici == ku);
                    //if (ba == null)
                    //{
                        ba.Kullanici = ku;
                        ba.Araba = aa;
                        //   ba.begenildimi = 1;//begenildimiyi gerekirse sil biryerde kullanmadım şuanlık....
                        begenilenler.Insert(ba);
                    //}


               }
                
                return View(begenilenler.List(x=>x.Kullanici.Id==ku.Id));


            }
            return View();
        }
        public ActionResult BegenilenArabaDetay(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SifirBegenilenArabalar arabalar = begenilenler.Find(x => x.Id == id.Value);

            if (arabalar == null)
            {
                return HttpNotFound();
            }

            return View(arabalar);
        }
        [HttpPost]
        public ActionResult BegenilenArabaDetay(int id)
        {
            SifirBegenilenArabalar araba = begenilenler.Find(x => x.Id == id);
            begenilenler.Delete(araba);
            return RedirectToAction("BegenilenAraba");

           
        }
        //--Araba sATIŞ VE KİRALAMANIN İşlemlerinin başladığı yer
        public ActionResult PersonelIslem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arabalar arabalar = a.Find(x => x.Id == id.Value);
            TempData["araba"] = arabalar.Id;
            if (arabalar == null)
            {
                return HttpNotFound();
            }

            return View(arabalar);
           
        }
        public ActionResult PesonelIslemKullanicisi()
        {
            return View();

        }

        [HttpPost]
        public ActionResult PesonelIslemKullanicisi(SifirSatilanAraba model)
        {         

                int idsi = Convert.ToInt32(TempData["araba"]);
                YeniArabalar car = yenia.Find(x => x.Arabaid.Id == idsi);
            Yoneticiler y = Session["loginy"] as Yoneticiler;
                Kullanicilar ku = kma.Find(x => x.Tc == model.SatilanKullanici.Tc);
            if (ku == null)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Kullanıcı Tc'si hatalı böyle bir kullanıcı bulunamadı..",
                    RedirectingUrl = "/Home/PesonelIslemKullanicisi"
                };
                return View("Error", notifyonj);
            }
            SifirSatilanAraba yenisat = new SifirSatilanAraba();
                yenisat.SifirArabalar = car;
                yenisat.SatilanKullanici = ku;
                yenisat.SatilmaTarih = DateTime.Now;
                yenisat.SatilmaUcret = model.SatilmaUcret;
                yenisat.SatanPersonel = y.Adi+" "+y.Adres;
                sifirsatilan.Insert(yenisat);
                try
                {
                    Arabalar aa = a.Find(x => x.Id == car.Arabaid.Id);
                    aa.IslemYapabilme = false;
                    a.Update(aa);

                    SifirSatilmayanArabalar satilma = satilmayanaraba.Find(x => x.SatilmayanAraba.Id == car.Id);
                    satilmayanaraba.Delete(satilma);
                    car.Durum = OtoGaleri_Entities.IkincielEnums.Durum.Satildi;
                    yenia.Update(car);

                Gelirler gelir = new Gelirler();
                gelir.Araba = aa;
                gelir.ArabaninDurumu = aa.Durum;
                gelir.Kullanici = ku;
                gelir.Tarih = DateTime.Now;
                gelir.Personel = yenisat.SatanPersonel; //sifir satılmış personelden çektik ilerde deişiklik yapabilirim...
                gelir.Fiyat = yenisat.SatilmaUcret;
                gelirmanager.Insert(gelir);

                OkViewModel notifyobj = new OkViewModel()
                    {

                        Title = "Güncelleme Başarılı",
                        RedirectingUrl = "/Home/Index",

                    };
                    notifyobj.Items.Add("Satış işleminiz başarılı bir şekilde gerçekleşmiştir.");
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
      
        public ActionResult PersonelSifirSatilanArabalar()
        {
            int sifirkazanilan = sifirsatilan.List().Sum(x => x.SatilmaUcret);
            TempData["sifirkazanilan"] = sifirkazanilan;
            return View(sifirsatilan.List());
        }
        public ActionResult PersonelSifirSatilanArabaDetay(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SifirSatilanAraba arabalar = sifirsatilan.Find(x => x.Id == id.Value);

            if (arabalar == null)
            {
                return HttpNotFound();
            }

            return View(arabalar);
        }
        //--Sıfır arabalarının işlemlerinin bittiği yer

        //--Kiralık arabaların işlemlerinin başladığı yer
        public ActionResult PesonelIslemKullanicisikiralik()
        {
            return View();

        }
        [HttpPost]
        public ActionResult PesonelIslemKullanicisikiralik(KirayaVerilmisArabalar model)
        {
            if (model.KiradanAlamaTarih >= DateTime.Today && model.KiralamaTarih>=DateTime.Today)
            {//olmadı yine dene......
                int idsi = Convert.ToInt32(TempData["araba"]);
                KiralikArabalar car = kiralikmanager.Find(x => x.Arabaid.Id == idsi);
                Kullanicilar ku = kma.Find(x => x.Tc == model.HangiKullanici.Tc);
                Ortak123 ortakk = Session["loginy"] as Yoneticiler;
                Ortak123 ortakk1 = Session["loginp"] as Personeller;
                Ortak123 ortakkkisi;
                if (ortakk == null)
                {
                    ortakkkisi = ortakk1;
                }else
                {
                    ortakkkisi = ortakk;
                }
                if (ku == null)
                {
                    ErrorViewModel notifyonj = new ErrorViewModel()
                    {
                        Title = "Kullanıcı Tc'si hatalı böyle bir kullanıcı bulunamadı..",
                        RedirectingUrl = "/Home/PesonelIslemKullanicisikiralik"
                    };
                    return View("Error", notifyonj);
                }
                DateTime verilentarih = model.KiralamaTarih;
                DateTime almatarih = model.KiradanAlamaTarih;
                TimeSpan sonuc = almatarih - verilentarih;
                int sonuc1 = Convert.ToInt32(sonuc.Days);

                KirayaVerilmisArabalar kayit = new KirayaVerilmisArabalar();
                kayit.HangiKullanici = ku;
                kayit.KiralikAraba = car;
                kayit.KiralayanPersonel = ortakkkisi.Adi+" "+ortakkkisi.Soyadi;
                kayit.KiralamaTarih = model.KiralamaTarih;
                kayit.KiradanAlamaTarih = model.KiradanAlamaTarih;
                kayit.AlinacakUcret = sonuc1 * model.AlinacakUcret;
                kirayaverilmismanager.Insert(kayit);
                try
                {
                    Arabalar aa = a.Find(x => x.Id == car.Arabaid.Id);
                    aa.IslemYapabilme = false;
                    a.Update(aa);

                    KiralikBekleyen kiralikverilen = kiralik_bekleyen.Find(x => x.KiralikAraba.Arabaid.Id == car.Arabaid.Id);
                    kiralik_bekleyen.Delete(kiralikverilen);
                    car.Durum = OtoGaleri_Entities.IkincielEnums.DurumKiralandi.Kiralandi;
                    kiralikmanager.Update(car);

                    Gelirler gelir = new Gelirler();
                    gelir.Araba = aa;
                    gelir.ArabaninDurumu = aa.Durum;
                    gelir.Kullanici = ku;
                    gelir.Tarih = DateTime.Now;
                    gelir.Personel = kayit.KiralayanPersonel; //kiralık satılmış personelden çektik ilerde deişiklik yapabilirim...
                    gelir.Fiyat = kayit.AlinacakUcret;
                    gelirmanager.Insert(gelir);


                    OkViewModel notifyobj = new OkViewModel()
                    {

                        Title = "Güncelleme Başarılı",
                        RedirectingUrl = "/Home/Index",

                    };
                    notifyobj.Items.Add("Satış işleminiz başarılı bir şekilde gerçekleşmiştir.");
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
            else
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Lütfen Tarih Seçimi Bugünden Küçük Olmasın.Güvenlik Açısından Anasayfaya Yönlendiriliyorsunuz."
                    
                };
                return View("Error", notifyonj);
            }
         
            //return View();
        }
        public ActionResult PersonelKiralananArabalar()
        {
            
            
            int kiradankazanilan = kirayaverilmismanager.List(x => x.IslemAktiflik == false).Sum(x => x.AlinacakUcret);
            TempData["kiradankazanilan"] = kiradankazanilan;
            return View(kirayaverilmismanager.List());
        }

        [HttpPost]
        public ActionResult KiralikArabaTeslimAl(KirayaVerilmisArabalar model)
        {
            KirayaVerilmisArabalar kiradaki = kirayaverilmismanager.Find(x => x.Id == model.Id);
           
            Arabalar aa = a.Find(x => x.Id == kiradaki.KiralikAraba.Arabaid.Id);
            aa.IslemYapabilme = true;
            a.Update(aa);
            KiralikArabalar kiralikverilen = kiralikmanager.Find(x => x.Arabaid.Id == kiradaki.KiralikAraba.Arabaid.Id);
            KiralikBekleyen kiralikbekleyen = new KiralikBekleyen();
            kiralikbekleyen.KiralikAraba = kiralikverilen;
            kiralik_bekleyen.Insert(kiralikbekleyen);
            kiradaki.IslemAktiflik = false;
            kirayaverilmismanager.Update(kiradaki);
            try
            {
              
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Güncelleme Başarılı",
                    RedirectingUrl = "/Home/Index",

                };
                notifyobj.Items.Add("Satış işleminiz başarılı bir şekilde gerçekleşmiştir.");
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

        public ActionResult KiralikArabaTeslimAl(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KirayaVerilmisArabalar arabalar = kirayaverilmismanager.Find(x => x.Id == id.Value);

            if (arabalar == null)
            {
                return HttpNotFound();
            }
           
                return View(arabalar); //burada kaldım hata alıyorum....
            
            
        }

        public ActionResult KiralikArabaGunUzat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KirayaVerilmisArabalar kirayaverilmis = kirayaverilmismanager.Find(x => x.Id == id.Value);

            if (kirayaverilmis == null)
            {
                return HttpNotFound();
            }

            return View(kirayaverilmis);

        }

        [HttpPost]
        public ActionResult KiralikArabaGunUzat(KirayaVerilmisArabalar model)
        {
            if (model.KiradanAlamaTarih > DateTime.Today )
            {
                ModelState.Remove("HangiKullanici");
                ModelState.Remove("KiralayanPersonel");
                ModelState.Remove("KiralikAraba");
                ModelState.Remove("KiralamaTarih");
                if (ModelState.IsValid)
                {
                    KirayaVerilmisArabalar kiradaki = kirayaverilmismanager.Find(x => x.Id == model.Id);
                    Arabalar aa = a.Find(x => x.Id == kiradaki.KiralikAraba.Arabaid.Id);
                    DateTime tariheski = kiradaki.KiradanAlamaTarih;

                    kiradaki.KiradanAlamaTarih = model.KiradanAlamaTarih;



                    DateTime tarihyeni = model.KiradanAlamaTarih;
                    TimeSpan sonuc = tarihyeni - tariheski;
                    int sonuc1 = Convert.ToInt32(sonuc.Days);
                    kiradaki.AlinacakUcret = kiradaki.AlinacakUcret + aa.Fiyat * sonuc1;
                    kirayaverilmismanager.Update(kiradaki);

                    Gelirler gelir = gelirmanager.Find(x => x.Araba.Id == aa.Id);
                    gelir.Fiyat = kiradaki.AlinacakUcret;
                    gelirmanager.Update(gelir);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Lütfen Tarih Seçimi Bugünden Küçük Olmasın.Güvenlik Açısından Anasayfaya Yönlendiriliyorsunuz."

                };
                return View("Error", notifyonj);
            }
            return View();
        }
       //--Kiralık Arabalar İşlemleri Sonu
        
        //-----Ikinci El Arabalar İşlemleri Başlangıcı
        public ActionResult PesonelIslemKullanicisiIkinciEl()
        {

            return View();
        }
        [HttpPost]
        public ActionResult PesonelIslemKullanicisiIkinciEl(IkinciElSatilmisArabalar model)
        {
            int idsi = Convert.ToInt32(TempData["araba"]);
            IkinciEl car = ikincielarabalar.Find(x => x.Arabaid.Id == idsi);
            Kullanicilar ku = kma.Find(x => x.Tc == model.SatilanKullanici.Tc);
            Ortak123 ortakk = Session["loginy"] as Yoneticiler;
            Ortak123 ortakk1 = Session["loginp"] as Personeller;
            Ortak123 ortakkkisi;
            if (ortakk == null)
            {
                ortakkkisi = ortakk1;
            }
            else
            {
                ortakkkisi = ortakk;
            }
            if (ku == null)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Kullanıcı Tc'si hatalı böyle bir kullanıcı bulunamadı..",
                    RedirectingUrl = "/Home/PesonelIslemKullanicisiIkinciEl"
                };
                return View("Error", notifyonj);
            }
            IkinciElSatilmisArabalar ikincielsatilmis = new IkinciElSatilmisArabalar();
            ikincielsatilmis.SatilanKullanici = ku;
            ikincielsatilmis.IkinciElArabalar = car;
            ikincielsatilmis.SatilanUcret = model.SatilanUcret;
            ikincielsatilmis.SatanPersonel =ortakkkisi.Adi+" "+ortakkkisi.Soyadi;
            ikincielsatilmis.SatilmaTarih = DateTime.Now;
            ikincielsatilmisarabalarmanager.Insert(ikincielsatilmis);
            try
            {
              
                Arabalar aa = a.Find(x => x.Id == car.Arabaid.Id);
                aa.IslemYapabilme = false;
                a.Update(aa);

                Gelirler gelir = new Gelirler();
                gelir.Araba = aa;
                gelir.ArabaninDurumu = aa.Durum;
                gelir.Kullanici = ku;
                gelir.Tarih = DateTime.Now;
                gelir.Personel = ikincielsatilmis.SatanPersonel; //ikinci el satılmış personelden çektik ilerde deişiklik yapabilirim...
                gelir.Fiyat = ikincielsatilmis.SatilanUcret;
                gelirmanager.Insert(gelir);

                IkinciElSatilmayanArabalar satilmayan = ikincielsatilmayanarabamanager.Find(x => x.IkinciEller.Arabaid.Id == car.Arabaid.Id);
                ikincielsatilmayanarabamanager.Delete(satilmayan);
                car.Durum = OtoGaleri_Entities.IkincielEnums.Durum.Satildi;
                ikincielarabalar.Update(car);


                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Güncelleme Başarılı",
                    RedirectingUrl = "/Home/Index",

                };
                notifyobj.Items.Add("Satış işleminiz başarılı bir şekilde gerçekleşmiştir.");
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
        public ActionResult PersonelSatilanArabalar()
        {
            int ikincielkazanilan = ikincielsatilmisarabalarmanager.List().Sum(x => x.SatilanUcret);
            TempData["ikincielkazanilan"] = ikincielkazanilan;
            return View(ikincielsatilmisarabalarmanager.List());
        }
      
       
        public ActionResult PersonelIkinciElSatilanArabaDetay(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IkinciElSatilmisArabalar arabalar = ikincielsatilmisarabalarmanager.Find(x => x.Id == id.Value);

            if (arabalar == null)
            {
                return HttpNotFound();
            }

            return View(arabalar);
        }
        //-----------Ikinci El Arabalar İşlemleri Sonu
        public ActionResult GelirlerAraba()
        {
           int kiralik= gelirmanager.List(x=>x.ArabaninDurumu==OtoGaleri_Entities.ArabalarEnums.Durum2.Kiralik).Sum(x=>x.Fiyat);
            int ikinciel = gelirmanager.List(x => x.ArabaninDurumu == OtoGaleri_Entities.ArabalarEnums.Durum2.IkinciEl).Sum(x => x.Fiyat);
            int sifir = gelirmanager.List(x => x.ArabaninDurumu == OtoGaleri_Entities.ArabalarEnums.Durum2.Sifir).Sum(x => x.Fiyat);
            int toplam = kiralik + ikinciel + sifir;
            //burda kaldım


            TempData["VeriKiralik"] = kiralik;
            TempData["VeriSifir"] = sifir;
            TempData["VeriIkinci"] = ikinciel;
            TempData["VeriToplam"] = toplam;
            return View(gelirmanager.List().OrderByDescending(x => x.Tarih));
        }
      
        public ActionResult Create()
        {
            return View();
        }

          [HttpPost]
        [ValidateAntiForgeryToken]                      //http yok
        public ActionResult Create(Arabalar arabalar, HttpPostedFileBase ProfileImage)
        {
            
            ModelState.Remove("Resim1");
            ModelState.Remove("Resim2");
            ModelState.Remove("Resim3");
            ModelState.Remove("EklenmeTarihi");
            ModelState.Remove("IlanTarihi");
            ModelState.Remove("ArabayiEkleyen");
            if (ModelState.IsValid)
            {
                Arabalar aaaa = new Arabalar();
                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{arabalar.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    arabalar.Resim1 = filename;
                }
                Ortak123 ortakk = Session["loginy"] as Yoneticiler;
                int sayi =Convert.ToInt32(arabalar.Resim2);
                arabalar.Resim2 = null;
                arabalar.ArabayiEkleyen = ortakk.Adi + " " + ortakk.Soyadi;
                a.Insert(arabalar);
                if (arabalar.Durum == OtoGaleri_Entities.ArabalarEnums.Durum2.Sifir)
                {
                    YeniArabalar yeniaraba = new YeniArabalar();
                    yeniaraba.Arabaid = arabalar;
                    yeniaraba.Durum = OtoGaleri_Entities.IkincielEnums.Durum.Bekliyor;
                    yenia.Insert(yeniaraba);
                    SifirSatilmayanArabalar satilmayan_araba = new SifirSatilmayanArabalar();
                    satilmayan_araba.SatilmayanAraba = yeniaraba;
                    satilmayanaraba.Insert(satilmayan_araba);
                }
                if (arabalar.Durum == OtoGaleri_Entities.ArabalarEnums.Durum2.Kiralik)
                {
                    KiralikArabalar kiralikaraba = new KiralikArabalar();
                    kiralikaraba.Arabaid = arabalar;
                    kiralikaraba.Durum = OtoGaleri_Entities.IkincielEnums.DurumKiralandi.Bekliyor;
                    kiralikaraba.GunlukUcret = arabalar.Fiyat;
                    kiralikmanager.Insert(kiralikaraba);
                    KiralikBekleyen kiralikbekleyen = new KiralikBekleyen();
                    kiralikbekleyen.KiralikAraba = kiralikaraba;
                    kiralik_bekleyen.Insert(kiralikbekleyen);
                }
                if (arabalar.Durum == OtoGaleri_Entities.ArabalarEnums.Durum2.IkinciEl)
                {
                    IkinciEl ikinciel = new IkinciEl();
                    ikinciel.Arabaid = arabalar;
                    ikinciel.Durum = OtoGaleri_Entities.IkincielEnums.Durum.Bekliyor;
                    ikinciel.Km = sayi;
                    //ikinci elarabada arabanın kaç km oldugunu belirtmiyoruz.belirtmek lazm......
                    ikincielarabalar.Insert(ikinciel);
                    IkinciElSatilmayanArabalar ikincielsatilmamis = new IkinciElSatilmayanArabalar();
                    ikincielsatilmamis.IkinciEller = ikinciel;
                    ikincielsatilmayanarabamanager.Insert(ikincielsatilmamis);

                }

                return RedirectToAction("Index","Home");
            }

            return View(arabalar);
        }

      

        // GET: Arabalar/Edit/5
        public ActionResult Edit(int? id)//int? id
        {       //id
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
                                            //Id==id
            Arabalar arabalar = a.Find(x=>x.Id==id);
            if (arabalar.Durum == OtoGaleri_Entities.ArabalarEnums.Durum2.IkinciEl)
            {
                IkinciEl ikinciel = ikincielarabalar.Find(x => x.Arabaid.Id == arabalar.Id);
                int a = ikinciel.Km;
                TempData["kmm"] = a;
            }
            if (arabalar == null)
            {
                return HttpNotFound();
            }
            return View(arabalar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Arabalar user, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ArabayiEkleyen");
            ModelState.Remove("EklenmeTarihi");
          //  ModelState.Remove("Resim1");
            ModelState.Remove("Resim2");
            ModelState.Remove("Resim3");
            ModelState.Remove("Durum");
           ModelState.Remove("IlanTarihi");
            if (ModelState.IsValid)
            {

                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{user.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    user.Resim1 = filename;
                }
                Ortak123 ortakk = Session["loginy"] as Yoneticiler;
                user.ArabayiEkleyen = ortakk.Adi + " " + ortakk.Soyadi;
                BusinessLayerResult<Arabalar> res = a.UpdateProfileK(user);
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errornotifyObj = new ErrorViewModel()
                    {
                        RedirectingUrl = "/Arabalar/Index",
                        Title = "Profil Güncellenemedi.",
                        Items = res.Errors
                    };

                    return View("Error", errornotifyObj);
                }
               // Session["logink"] = res.Result;
                //CurrentSession.Set<Kullanicilar>("login", res.Result);//profil güncellendiği için sesion güncellendi
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Güncelleme Başarılı",
                    RedirectingUrl = "/Home/Index",

                };
                notifyobj.Items.Add("Güncelleme işleminiz başarılı bir şekilde gerçekleşmiştir.");

                return View("Ok", notifyobj);

            }
                //if (ModelState.IsValid)
                //{
                //    Arabalar yo = a.Find(x => x.Id == arabalar.Id);
                //    yo.Aciklama = arabalar.Aciklama;
                //    yo.ArabayiEkleyen = arabalar.ArabayiEkleyen;
                //    yo.Cekis = arabalar.Cekis;
                //    yo.Durum = arabalar.Durum;
                //    yo.EklenmeTarihi = arabalar.EklenmeTarihi;
                //    yo.Fiyat = arabalar.Fiyat;
                //    yo.Garanti = arabalar.Garanti;
                //    yo.IlanTarihi = arabalar.IlanTarihi;
                //    yo.Kasatipi = arabalar.Kasatipi;
                //    yo.Marka = arabalar.Marka;
                //    yo.Model = arabalar.Model;
                //    yo.MotorGucu = arabalar.MotorGucu;
                //    yo.MotorHacmi = arabalar.MotorHacmi;
                //    yo.Renk = arabalar.Renk;
                //    yo.Resim1 = arabalar.Resim1;
                //    yo.Resim2 = arabalar.Resim2;
                //    yo.Resim3 = arabalar.Resim3;
                //    yo.Vites = arabalar.Vites;
                //    yo.Yakit = arabalar.Yakit;
                //    yo.Yil = arabalar.Yil;

                //    a.Update(yo);
                //    return RedirectToAction("Index");
                //}
                //return View(arabalar);
                return View(user);
        }

        // GET: Arabalar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arabalar arabalar = a.Find(x=>x.Id==id.Value);
            if (arabalar == null)
            {
                return HttpNotFound();
            }
            return View(arabalar);
        }

        // POST: Arabalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Arabalar araba = a.Find(x => x.Id == id);
            a.Delete(araba);
            return RedirectToAction("Index","Home");
        }
        
        public ActionResult KullaniciKiraladigim()
        {
            Kullanicilar ku = Session["logink"] as Kullanicilar;
            int kirayaverdigimpara = kirayaverilmismanager.List(x => x.IslemAktiflik == false && x.HangiKullanici.Id ==ku.Id ).Sum(x => x.AlinacakUcret);
            TempData["kirayaverdigimpara"] = kirayaverdigimpara;
            return View(kirayaverilmismanager.List(x=>x.HangiKullanici.Id==ku.Id));
        }

        public ActionResult KullaniciIkinciEl()
        {
            Kullanicilar ku = Session["logink"] as Kullanicilar;
            int ikincielverdigimpara = ikincielsatilmisarabalarmanager.List(x =>x.SatilanKullanici.Id==ku.Id ).Sum(x => x.SatilanUcret);
            TempData["ikincielverdigimpara"] = ikincielverdigimpara;
            return View(ikincielsatilmisarabalarmanager.List(x => x.SatilanKullanici.Id == ku.Id));
        }
        public ActionResult KullaniciSifir()
        {
            Kullanicilar ku = Session["logink"] as Kullanicilar;
            int sifirverdigimpara = sifirsatilan.List(x => x.SatilanKullanici.Id == ku.Id).Sum(x => x.SatilmaUcret);
            TempData["sifirverdigimpara"] = sifirverdigimpara;
            return View(sifirsatilan.List(x => x.SatilanKullanici.Id == ku.Id));
        }
        public ActionResult BosDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arabalar arabalar = a.Find(x => x.Id == id.Value);
            if (arabalar.Durum == OtoGaleri_Entities.ArabalarEnums.Durum2.IkinciEl)
            {
                IkinciEl ikinciel = ikincielarabalar.Find(x => x.Arabaid.Id == arabalar.Id);
                int a = ikinciel.Km;
                TempData["km"] = a;
            }
            if (arabalar == null)
            {
                return HttpNotFound();
            }

            return View(arabalar);
        }


    }
}
