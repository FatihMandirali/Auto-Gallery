using OtoGaleri.ViewModels;
using OtoGaleri_BusinessLayer;
using OtoGaleri_BusinessLayer.Result;
using OtoGaleri_Common.Helpers;
using OtoGaleri_DataAccessLayer.Entity_Framework;
using OtoGaleri_Entities.Tablolar;
using OtoGaleri_Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OtoGaleri.Controllers
{
    public class HomeController : Controller
    {
     //Fatih Mandıralı
        KullaniciManager k = new KullaniciManager();
        PersonelManager p = new PersonelManager();
        YoneticiManager y = new YoneticiManager();
        ArabalarManager am = new ArabalarManager();
        MesajlasmaManager m_manager = new MesajlasmaManager();
        SifirBegenilenArabalarManager sb_maneger = new SifirBegenilenArabalarManager();
        //IkinciElSatilmisArabalarManager ikincielsatilmamisarabamanager = new IkinciElSatilmisArabalarManager();
        //SifirSatilmayanArabalarManager sifirsatilmayanarabalarmanager = new SifirSatilmayanArabalarManager();
        //KiralikBekleyenManager kiralikbekletenmanager = new KiralikBekleyenManager();
      Models.ViewModels model = new Models.ViewModels();
        // GET: Home
        public ActionResult Index(int? id)
        {
            
            model.araba = am.List().OrderByDescending(x => x.EklenmeTarihi).ToList();
            model.kullanici=k.List().OrderByDescending(x => x.KayitTarih).ToList();
            return View(model);      
        }
    
        public ActionResult Category(int? id)
        {
          string ad;
            if (id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            //YeniArabalarManager yenia = new YeniArabalarManager();
            //if (id == 0)
            //{
            //    return View("Index", yenia.List().ToList());
            //}   //burda arabaları farklı tablolara ayırmıştım ilerde devam edebilirim...
         //   ArabalarManager am = new ArabalarManager();
           
            if (id == 0)
            {
                ad = "Sifir";
                model.araba = am.List(x => x.Durum.ToString() == ad.ToString()).OrderByDescending(x => x.EklenmeTarihi).ToList();
                model.kullanici = k.List().OrderByDescending(x => x.KayitTarih).ToList();
                return View("Index",model);
            }
            else if (id == 1)
            {
                ad = "IkinciEl";
                model.araba = am.List(x => x.Durum.ToString() == ad.ToString()).OrderByDescending(x => x.EklenmeTarihi).ToList();
                model.kullanici = k.List().OrderByDescending(x => x.KayitTarih).ToList();
                return View("Index", model);
            }
            else if (id == 2)
            {
                ad = "Kiralik";
                model.araba = am.List(x => x.Durum.ToString() == ad.ToString()).OrderByDescending(x => x.EklenmeTarihi).ToList();
                model.kullanici = k.List().OrderByDescending(x => x.KayitTarih).ToList();
                return View("Index", model);
            }
            else
                return View("Index");
        }
        public ActionResult About()
        {

            return View();
        }

        //-----------
        public ActionResult LoginKullanici() {


            return View();
       }
        [HttpPost]
        public ActionResult LoginKullanici(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kullanicilar> res = k.LoginUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                Session["logink"] = res.Result;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //-----------------
        public ActionResult LoginPersonel()
        {


            return View();
        }
        [HttpPost]
        public ActionResult LoginPersonel(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Personeller> res = p.LoginPersonel(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                Session["loginp"] = res.Result;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //--------------
        public ActionResult LoginYonetici()
        {


            return View();
        }
        [HttpPost]
        public ActionResult LoginYonetici(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Yoneticiler> res = y.LoginYonetici(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                Session["loginy"] = res.Result;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //--------Loginler bitişi



        public ActionResult Register()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kullanicilar> res = k.RegisterUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title="Kayıt Başarılı",
                    RedirectingUrl="/Home/LoginKullanici",
                    
                };
                notifyobj.Items.Add("Lütfen E-Posta adresinize gönderilen link ile hesabınızı onaylatın ve sonra sisteminize giriş yapın.");

                return View("Ok",notifyobj);
            }

            return View(model);
        }

        //---------------------
        public ActionResult ShowProfileK()
        {
            Kullanicilar kullanici = Session["logink"] as Kullanicilar;
            k = new KullaniciManager();
            BusinessLayerResult<Kullanicilar> res = k.GetUserById(kullanici.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                   Items = res.Errors

                };
                TempData["errors"] = res.Errors;
                return View("Error", notifyonj);
            }
           
            return View(res.Result);
        }
        public ActionResult EditProfileK()
        {
            Kullanicilar kullanici = Session["logink"] as Kullanicilar;
            k = new KullaniciManager();
            BusinessLayerResult<Kullanicilar> res = k.GetUserById(kullanici.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors

                };
                TempData["errors"] = res.Errors;
                return View("Error", notifyonj);
            }

            return View(res.Result);
            
        }

        [HttpPost]
        public ActionResult EditProfileK(Kullanicilar user,HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("KimKayitEtti");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{user.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    user.Resim = filename;
                }
                BusinessLayerResult<Kullanicilar> res = k.UpdateProfileK(user);
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errornotifyObj = new ErrorViewModel()
                    {
                        RedirectingUrl = "/Home/EditProfileK",
                        Title = "Profil Güncellenemedi.",
                        Items = res.Errors
                    };

                    return View("Error", errornotifyObj);
                }
                Session["logink"] = res.Result;
                //CurrentSession.Set<Kullanicilar>("login", res.Result);//profil güncellendiği için sesion güncellendi
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Güncelleme Başarılı",
                    RedirectingUrl = "/Home/ShowProfileK",

                };
                notifyobj.Items.Add("Güncelleme işleminiz başarılı bir şekilde gerçekleşmiştir.");

                return View("Ok", notifyobj);
            }
            return View(user);

        }

        public ActionResult DeleteProfileK()
        {
            Kullanicilar kk = Session["logink"] as Kullanicilar;
            BusinessLayerResult<Kullanicilar> res = k.RemoveUserByIdK(kk.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel errornotifyObj = new ErrorViewModel()
                {
                    RedirectingUrl = "/Home/ShowProfileK",
                    Title = "Profil Silinemedi.",
                    Items = res.Errors
                };

                return View("Error", errornotifyObj);
            }
            Session.Clear();
            return RedirectToAction("Index");
            
        }
        //----------------------------
        public ActionResult ShowProfileY()
        {
            Yoneticiler yonetici = Session["loginy"] as Yoneticiler;
            y = new YoneticiManager();
            BusinessLayerResult<Yoneticiler> res = y.GetUserById(yonetici.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors

                };
                TempData["errors"] = res.Errors;
                return View("Error", notifyonj);
            }

            return View(res.Result);
        }
        public ActionResult EditProfileY()
        {
            Yoneticiler yonetici = Session["loginy"] as Yoneticiler;
            y = new YoneticiManager();
            BusinessLayerResult<Yoneticiler> res = y.GetUserById(yonetici.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors

                };
                TempData["errors"] = res.Errors;
                return View("Error", notifyonj);
            }

            return View(res.Result);

        }
        [HttpPost]
        public ActionResult EditProfileY(Yoneticiler user, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("KimKayitEtti");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{user.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    user.Resim = filename;
                }
                BusinessLayerResult<Yoneticiler> res = y.UpdateProfileY(user);
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errornotifyObj = new ErrorViewModel()
                    {
                        RedirectingUrl = "/Home/EditProfileY",
                        Title = "Profil Güncellenemedi.",
                        Items = res.Errors
                    };

                    return View("Error", errornotifyObj);
                }
                Session["loginy"] = res.Result;
                //CurrentSession.Set<Kullanicilar>("login", res.Result);//profil güncellendiği için sesion güncellendi
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Güncelleme Başarılı",
                    RedirectingUrl = "/Home/ShowProfileY",

                };
                notifyobj.Items.Add("Güncelleme işleminiz başarılı bir şekilde gerçekleşmiştir.");

                return View("Ok", notifyobj);
            }
            return View(user);

        }
        public ActionResult DeleteProfileY()
        {
            Yoneticiler yy = Session["loginy"] as Yoneticiler;
            BusinessLayerResult<Yoneticiler> res = y.RemoveUserByIdY(yy.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel errornotifyObj = new ErrorViewModel()
                {
                    RedirectingUrl = "/Home/ShowProfileY",
                    Title = "Profil Silinemedi.",
                    Items = res.Errors
                };

                return View("Error", errornotifyObj);
            }
            Session.Clear();
            return RedirectToAction("Index");

        }
        //-------------

        public ActionResult ShowProfileP()
        {
            Personeller personel = Session["loginp"] as Personeller;
            p = new PersonelManager();
            BusinessLayerResult<Personeller> res = p.GetUserById(personel.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors

                };
                TempData["errors"] = res.Errors;
                return View("Error", notifyonj);
            }

            return View(res.Result);
        }
        public ActionResult EditProfileP()
        {
            Personeller personel = Session["loginp"] as Personeller;
            p = new PersonelManager();
            BusinessLayerResult<Personeller> res =p.GetUserById(personel.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel notifyonj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu.",
                    Items = res.Errors

                };
                TempData["errors"] = res.Errors;
                return View("Error", notifyonj);
            }

            return View(res.Result);

        }
        [HttpPost]
        public ActionResult EditProfileP(Personeller user, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("KimKayitEtti");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" || ProfileImage.ContentType == "image/jpg" || ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{user.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    user.Resim = filename;
                }
                BusinessLayerResult<Personeller> res = p.UpdateProfileP(user);
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errornotifyObj = new ErrorViewModel()
                    {
                        RedirectingUrl = "/Home/EditProfileP",
                        Title = "Profil Güncellenemedi.",
                        Items = res.Errors
                    };

                    return View("Error", errornotifyObj);
                }
                Session["loginp"] = res.Result;
                //CurrentSession.Set<Kullanicilar>("login", res.Result);//profil güncellendiği için sesion güncellendi
                OkViewModel notifyobj = new OkViewModel()
                {

                    Title = "Güncelleme Başarılı",
                    RedirectingUrl = "/Home/ShowProfileP",

                };
                notifyobj.Items.Add("Güncelleme işleminiz başarılı bir şekilde gerçekleşmiştir.");

                return View("Ok", notifyobj);
            }
            return View(user);

        }
        public ActionResult DeleteProfileP()
        {
            Personeller pp = Session["loginp"] as Personeller;
            BusinessLayerResult<Personeller> res = p.RemoveUserByIdP(pp.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel errornotifyObj = new ErrorViewModel()
                {
                    RedirectingUrl = "/Home/ShowProfileP",
                    Title = "Profil Silinemedi.",
                    Items = res.Errors
                };

                return View("Error", errornotifyObj);
            }
            Session.Clear();
            return RedirectToAction("Index");

        }
        //----------------
        public ActionResult UserActivate(Guid id)
        {

            k = new KullaniciManager();
            BusinessLayerResult<Kullanicilar> res = k.ActivateUser(id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel notifyonj = new ErrorViewModel() {
                    Title = "Geçersiz İşlem.",
                    Items = res.Errors 

                };
                 TempData["errors"] = res.Errors;
                return View("Error", notifyonj);
            }

            OkViewModel oknotifyObj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/LoginKullanici"
            };
            oknotifyObj.Items.Add("Hesabınınz aktifleştirildi");

            return View("Ok", oknotifyObj);
        }

        [HttpPost]
        public JsonResult MesajYolla(Mesajlasma m)
        {
            Kullanicilar k = Session["logink"] as Kullanicilar;
            Mesajlasma mm = new Mesajlasma();
            mm.Mesaj = m.Mesaj;
            mm.GondermeTarihi = DateTime.Now;
            mm.GonderenKullanici = k;
         //   mm.AlanKullanici = "sdf";

            m_manager.Insert(mm);
            return Json("Mesajınınz başarıyla gönderildi.", JsonRequestBehavior.AllowGet);
        }

        public ActionResult MesajKutusuK()
        {
            Kullanicilar k = Session["logink"] as Kullanicilar;
            return View(m_manager.List(x=>x.GonderenKullanici.Id==k.Id && x.Ksil!=true).OrderByDescending(x => x.GondermeTarihi));
        }

        public ActionResult MesajDetay(int? id)
        {
            Mesajlasma mm = m_manager.Find(x => x.Id == id);
            mm.Okundumu = true;
            mm.Kokudumu = true;
            m_manager.Update(mm);
            return View(/*m_manager.Find(x=>x.Id==id)*/mm);
        }
       
        public ActionResult MesajKutusuY()
        {
            Models.YoneticiMesajViewModel model = new Models.YoneticiMesajViewModel();
            model.mesaj = m_manager.List(x=>x.Ysil != false).OrderByDescending(x => x.GondermeTarihi).ToList();
            model.kullanici = k.List().OrderByDescending(x => x.KayitTarih).ToList();
            return View(model);
        }
        public ActionResult MesajYanitla(int? id)
        {
            Mesajlasma mm = m_manager.Find(x => x.Id == id);          
            mm.Okundumu = true;         
            m_manager.Update(mm);
            return View(m_manager.Find(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult MesajYanitla(Mesajlasma m)
        {
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
            Mesajlasma mm = m_manager.Find(x => x.Id == m.Id);
            mm.Yanit = m.Yanit;
            mm.Okundumu = true;
            mm.AlanKullanici = ortakkkisi.Adi + " " + ortakkkisi.Soyadi;
            mm.Kokudumu = false;
            m_manager.Update(mm);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult MesajSil(int id)
        {
            Mesajlasma m = m_manager.Find(x => x.Id == id);
            m.Ysil = false;
            m.Okundumu = true;
            m_manager.Update(m);
            return Json("Mesajınınz başarıyla silindi.", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MesajSilK(int id)
        {
            Mesajlasma m = m_manager.Find(x => x.Id == id);
            m.Ksil = true;
            m.Kokudumu = true;
            m_manager.Update(m);
            return Json("Mesajınınz başarıyla silindi.", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");

        }

        int kullaniciid;
             public ActionResult YetkiliMsj(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                kullaniciid =Convert.ToInt32(id);
                TempData["gönderilenkullanici"] = kullaniciid;
            }
            
            return View();

        }
        [HttpPost]
        public JsonResult YetkiliMesaj(Mesajlasma mesaj)
        {
            if (mesaj.Yanit == null) {
                return Json("Mesaj Gönderilemedi lütfen boş yer bırakmyın.", JsonRequestBehavior.AllowGet);
            } else
            {
            Ortak123 o = Session["loginy"] as Ortak123;
            int kullaniciid1 =Convert.ToInt32(TempData["gönderilenkullanici"]);
            Kullanicilar kk = k.Find(x => x.Id == kullaniciid1);

            Mesajlasma m = new Mesajlasma();
            m.Yanit = mesaj.Yanit;
            m.GondermeTarihi = DateTime.Now;
            m.GonderenKullanici =kk ;
            m.AlanKullanici = o.Adi + " " + o.Soyadi;
            m.Okundumu = true;
            m.Kokudumu = false;
            m_manager.Insert(m);
            return Json("Mesajınınz başarıyla gönderildi.", JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult KullaniciCevap(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
           // Mesajlasma m = m_manager.Find(x => x.Id == id);
            return View(m_manager.Find(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult KullaniciCevap(Mesajlasma mesaj)
        {
            Mesajlasma m = m_manager.Find(x => x.Id == mesaj.Id);
            m.Mesaj = mesaj.Yanit;
            m.Okundumu = false;
            m.Kokudumu = true;
            m_manager.Update(m);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ArabaBegen(int id)
        {
            Kullanicilar k = Session["logink"] as Kullanicilar;
            Arabalar a = am.Find(x => x.Id == id);
            if (sb_maneger.Find(x => x.Kullanici.Id == k.Id && x.Araba.Id == a.Id) != null)
            {
                return Json("Bu araba zaten beğenildi.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                SifirBegenilenArabalar m = new SifirBegenilenArabalar();
                m.Araba = a;
                m.Kullanici = k;
                sb_maneger.Insert(m);
                return Json("Araba beğenildi.", JsonRequestBehavior.AllowGet);
            }
        }
        int kullaniciidsi;
        public ActionResult YetkiliMail(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                kullaniciidsi = Convert.ToInt32(id);
                TempData["gönderilenkullanicii"] = kullaniciidsi;
            }
            return View();

        }

        [HttpPost]
        public JsonResult YetkiliMaili(string baslik,string icerik)
        {
            if (baslik == null || icerik == null)
            {
               return Json("Mail Gönderilemedi lütfen boş yer bırakmyın.", JsonRequestBehavior.AllowGet);
            }
            else
            {           
                int kullaniciid1 = Convert.ToInt32(TempData["gönderilenkullanicii"]);
                Kullanicilar kk = k.Find(x => x.Id == kullaniciid1);
                MailHelper.SendMail(icerik, kk.Eposta, baslik, true);
                return Json("Mail Gönderildi.", JsonRequestBehavior.AllowGet);
            }
        }



        }
}