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
    public class HizmetlilerController : BaseController
    {
        private HizmetlilerManager h = new HizmetlilerManager();

        // GET: Hizmetliler
        public ActionResult Index()
        {
            return View(h.List());
        }

        // GET: Hizmetliler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetliler hizmetliler = h.Find(x=>x.Id==id.Value);
            if (hizmetliler == null)
            {
                return HttpNotFound();
            }
            return View(hizmetliler);
        }

        // GET: Hizmetliler/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hizmetliler hizmetliler)
        {
            Ortak123 ortakk = Session["loginy"] as Yoneticiler;
            ModelState.Remove("EkleyenPersonel");
            ModelState.Remove("EklenmeTarihi");
            if (ModelState.IsValid)
            {
                hizmetliler.EkleyenPersonel = ortakk.Adi + " " + ortakk.Soyadi;
                BusinessLayerResult<Hizmetliler> res = h.Insert(hizmetliler);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(hizmetliler);
                }
                return RedirectToAction("Index","Home");
            }

            return View(hizmetliler);
        }

        // GET: Hizmetliler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetliler hizmetliler = h.Find(x => x.Id == id.Value);
            if (hizmetliler == null)
            {
                return HttpNotFound();
            }
            return View(hizmetliler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hizmetliler hizmetliler)
        {
            ModelState.Remove("EkleyenPersonel");
            ModelState.Remove("EklenmeTarihi");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Hizmetliler> res = h.Update(hizmetliler);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(hizmetliler);
                }
                return RedirectToAction("Index","Home");
            }
            return View(hizmetliler);
        }

        // GET: Hizmetliler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetliler hizmetliler = h.Find(x => x.Id == id.Value);
            if (hizmetliler == null)
            {
                return HttpNotFound();
            }
            return View(hizmetliler);
        }

        // POST: Hizmetliler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hizmetliler hizmetli = h.Find(x => x.Id == id);
            h.Delete(hizmetli);
            return RedirectToAction("Index","Home");
        }

      
    }
}
