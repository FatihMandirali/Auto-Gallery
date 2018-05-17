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

namespace OtoGaleri.Controllers
{
    public class KullaniciController : Controller
    {
        private KullaniciManager k = new KullaniciManager();

        // GET: Kullanici
        public ActionResult Index()
        {
            return View(k.List());
        }

        // GET: Kullanici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = k.Find(x=>x.Id==id.Value);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // GET: Kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kullanicilar kullanicilar)
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
            ModelState.Remove("KimKayitEtti");
            ModelState.Remove("KayitTarih");
            ModelState.Remove("IsActive");
            if (ModelState.IsValid)
            {
                kullanicilar.KimKayitEtti = ortakkkisi.Adi + " " + ortakkkisi.Soyadi;
                BusinessLayerResult<Kullanicilar> res = k.Insert(kullanicilar);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(kullanicilar);
                }
                return RedirectToAction("Index","Home");
            }

            return View(kullanicilar);
        }

        // GET: Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = k.Find(x => x.Id == id.Value);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kullanicilar kullanicilar)
        {
            ModelState.Remove("KimKayitEtti");
            ModelState.Remove("KayitTarih");
            ModelState.Remove("IsActive");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kullanicilar> res = k.Update(kullanicilar);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(kullanicilar);
                }
                return RedirectToAction("Index","Home");
            }
            return View(kullanicilar);
        }

        // GET: Kullanici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanicilar kullanicilar = k.Find(x => x.Id == id.Value);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // POST: Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kullanicilar kk = k.Find(x => x.Id == id);
            k.Delete(kk);
            return RedirectToAction("Index","Home");
        }

       
    }
}
