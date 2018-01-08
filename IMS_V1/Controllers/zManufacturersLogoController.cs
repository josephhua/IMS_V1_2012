using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS_V1.Controllers
{
    public class zManufacturersLogoController : Controller
    {
        private IMS_V1Entities db = new IMS_V1Entities();

        //
        // GET: /zManufacturersLogo/

        public ActionResult Index()
        {
            return View(db.zManufacturersLogoes.ToList().OrderBy(z => z.APlusVendorName));
        }

        //
        // GET: /zManufacturersLogo/Details/5

        public ActionResult Details(int id = 0)
        {
            zManufacturersLogo zmanufacturerslogo = db.zManufacturersLogoes.Find(id);
            if (zmanufacturerslogo == null)
            {
                return HttpNotFound();
            }
            return View(zmanufacturerslogo);
        }

        //
        // GET: /zManufacturersLogo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /zManufacturersLogo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ManufacturerLogo_Id")]zManufacturersLogo zmanufacturerslogo)
        {
            if (zmanufacturerslogo.Abbrev != null && zmanufacturerslogo.Abbrev.Trim().Length > 4)
                ModelState.AddModelError("Abbrev", "Abbrev is too long, it should be 3 for non-marine vendors, and 4 for marine vendors.");
            else if (zmanufacturerslogo.Abbrev != null && zmanufacturerslogo.Abbrev.Trim().Contains(" "))
                ModelState.AddModelError("Abbrev", "Abbrev can't contain spaces.");
            else if (zmanufacturerslogo.Abbrev != null && zmanufacturerslogo.Abbrev.Trim().Length < 3)
                ModelState.AddModelError("Abbrev", "Abbrev is too short, at least 3 letters.");
            if (ModelState.IsValid)
            {
                db.zManufacturersLogoes.Add(zmanufacturerslogo);
                int userid = int.Parse(Session.Contents["UserID"].ToString());
                zmanufacturerslogo.ModifiedBy = userid;
                zmanufacturerslogo.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zmanufacturerslogo);
        }

        //
        // GET: /zManufacturersLogo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            zManufacturersLogo zmanufacturerslogo = db.zManufacturersLogoes.Find(id);
            if (zmanufacturerslogo == null)
            {
                return HttpNotFound();
            }
            return View(zmanufacturerslogo);
        }

        //
        // POST: /zManufacturersLogo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(zManufacturersLogo zmanufacturerslogo)
        {
            if (zmanufacturerslogo.Abbrev != null && zmanufacturerslogo.Abbrev.Trim().Length > 4)
                ModelState.AddModelError("Abbrev", "Abbrev is too long, it should be 3 for non-marine vendors, and 4 for marine vendors.");
            else if (zmanufacturerslogo.Abbrev != null && zmanufacturerslogo.Abbrev.Trim().Contains(" "))
                ModelState.AddModelError("Abbrev", "Abbrev can't contain spaces.");
            else if (zmanufacturerslogo.Abbrev != null && zmanufacturerslogo.Abbrev.Trim().Length < 3)
                ModelState.AddModelError("Abbrev", "Abbrev is too short, at least 3 letters."); if (ModelState.IsValid)
            {
                db.Entry(zmanufacturerslogo).State = EntityState.Modified;
                int userid = int.Parse(Session.Contents["UserID"].ToString());
                zmanufacturerslogo.ModifiedBy = userid;
                zmanufacturerslogo.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zmanufacturerslogo);
        }

        //
        // GET: /zManufacturersLogo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            zManufacturersLogo zmanufacturerslogo = db.zManufacturersLogoes.Find(id);
            if (zmanufacturerslogo == null)
            {
                return HttpNotFound();
            }
            return View(zmanufacturerslogo);
        }

        //
        // POST: /zManufacturersLogo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            zManufacturersLogo zmanufacturerslogo = db.zManufacturersLogoes.Find(id);
            db.zManufacturersLogoes.Remove(zmanufacturerslogo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}