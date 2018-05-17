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
using OtoGaleri.Utils;

namespace OtoGaleri.Controllers
{
    public class PersonelController : BaseController
    {
        private PersonelManager p = new PersonelManager();

        // GET: Personel
        public ActionResult Index()
        {
            return View(p.List());
        }

        // GET: Personel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personeller personeller = p.Find(x=>x.Id==id.Value);
            if (personeller == null)
            {
                return HttpNotFound();
            }
            return View(personeller);
        }

        // GET: Personel/Create
        public ActionResult Create()
        {
            return View();
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Personeller personeller)
        {
            Ortak123 ortakk = Session["loginy"] as Yoneticiler;
         
            ModelState.Remove("KimKayitEtti");
            ModelState.Remove("KayitTarih");
            ModelState.Remove("IsActive");
            if (ModelState.IsValid)
            {
                personeller.KimKayitEtti = ortakk.Adi + " " + ortakk.Soyadi;
                BusinessLayerResult<Personeller> res = p.Insert(personeller);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(personeller);
                }
                return RedirectToAction("Index","Home");
            }

            return View(personeller);
        }

        // GET: Personel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personeller personeller = p.Find(x=>x.Id==id.Value);
            if (personeller == null)
            {
                return HttpNotFound();
            }
            return View(personeller);
        }

          [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Personeller personeller)
        {
            ModelState.Remove("KimKayitEtti");
            ModelState.Remove("KayitTarih");
            ModelState.Remove("IsActive");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Personeller> res = p.Update(personeller);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(personeller);
                }
                return RedirectToAction("Index","Home");
            }
            return View(personeller);
        }

        // GET: Personel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personeller personeller = p.Find(x => x.Id == id.Value);
            if (personeller == null)
            {
                return HttpNotFound();
            }
            return View(personeller);
        }

        // POST: Personel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personeller yoneticiler = p.Find(x => x.Id == id);
            p.Delete(yoneticiler);
            return RedirectToAction("Index","Home");
        }

      
    }
}
