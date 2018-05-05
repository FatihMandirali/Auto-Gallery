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
    public class YonetimController : Controller
    {

       private YoneticiManager y = new YoneticiManager();
        // GET: Yonetim
        public ActionResult Index()
        {
            return View(y.List());
        }

        // GET: Yonetim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yoneticiler yoneticiler = y.Find(x=>x.Id==id.Value);
            if (yoneticiler == null)
            {
                return HttpNotFound();
            }
            return View(yoneticiler);
        }

        // GET: Yonetim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yonetim/Create
         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Yoneticiler yoneticiler)
        {
            Ortak123 ortakk = Session["loginy"] as Yoneticiler;
            ModelState.Remove("KimKayitEtti");
            ModelState.Remove("KayitTarih");
            ModelState.Remove("IsActive");
            if (ModelState.IsValid)
            {
                yoneticiler.KimKayitEtti = ortakk.Adi + " " + ortakk.Soyadi;
                BusinessLayerResult<Yoneticiler> res = y.Insert(yoneticiler);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(yoneticiler);
                }
                //y.Insert(yoneticiler);
                return RedirectToAction("Index");
            }

            return View(yoneticiler);
        }

        // GET: Yonetim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yoneticiler yoneticiler = y.Find(x => x.Id == id.Value);
            if (yoneticiler == null)
            {
                return HttpNotFound();
            }
            return View(yoneticiler);
        }

        // POST: Yonetim/Edit/5
          [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Yoneticiler yoneticiler)
        {
            ModelState.Remove("KimKayitEtti");
            ModelState.Remove("KayitTarih");
            ModelState.Remove("IsActive");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Yoneticiler> res = y.Update(yoneticiler);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(yoneticiler);
                }
                /* Yoneticiler yo = y.Find(x => x.Id == yoneticiler.Id);
                 yo.Adi = yoneticiler.Adi;
                 yo.Soyadi = yoneticiler.Soyadi;
                 yo.Tc = yoneticiler.Tc;
                 yo.DogumTarih = yoneticiler.DogumTarih;
                 yo.Eposta = yoneticiler.Eposta;
                 yo.IsActive = yoneticiler.IsActive;
                 yo.KullaniciAdi = yoneticiler.KullaniciAdi;
                 yo.Sifre = yoneticiler.Sifre;
                 yo.Adres = yoneticiler.Adres;
                 yo.Telefon = yoneticiler.Telefon;

                 y.Update(yo);//incelenecek*///bu yöntemi kullanmadık çünkü kullanıcı eklediğimiz için aynı isimleri sorguladık.
                return RedirectToAction("Index");
            }
            return View(yoneticiler);
        }

        // GET: Yonetim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yoneticiler yoneticiler = y.Find(x => x.Id == id.Value);
            if (yoneticiler == null)
            {
                return HttpNotFound();
            }
            return View(yoneticiler);
        }

        // POST: Yonetim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yoneticiler yoneticiler = y.Find(x => x.Id == id);
            y.Delete(yoneticiler);
            return RedirectToAction("Index");
        }

       
    }
}
