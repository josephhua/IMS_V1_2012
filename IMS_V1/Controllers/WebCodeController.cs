using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS_V1.Controllers
{
    public class WebCodeController : Controller
    {
        private IMS_V1Entities db = new IMS_V1Entities();

        //
        // GET: /WebCode/

        public ActionResult Index()
        {
            return View(db.WebCodes.ToList());
        }

        //
        // GET: /WebCode/Details/5

        public ActionResult Details(int id = 0)
        {
            WebCode webcode = db.WebCodes.Find(id);
            if (webcode == null)
            {
                return HttpNotFound();
            }
            return View(webcode);
        }

        //
        // GET: /WebCode/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WebCode/Create

        [HttpPost]
        public ActionResult Create(WebCode webcode)
        {
            if (ModelState.IsValid)
            {
                db.WebCodes.Add(webcode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webcode);
        }

        //
        // GET: /WebCode/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WebCode webcode = db.WebCodes.Find(id);
            if (webcode == null)
            {
                return HttpNotFound();
            }
            return View(webcode);
        }

        //
        // POST: /WebCode/Edit/5

        [HttpPost]
        public ActionResult Edit(WebCode webcode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webcode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webcode);
        }

        //
        // GET: /WebCode/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WebCode webcode = db.WebCodes.Find(id);
            if (webcode == null)
            {
                return HttpNotFound();
            }
            return View(webcode);
        }

        //
        // POST: /WebCode/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WebCode webcode = db.WebCodes.Find(id);
            db.WebCodes.Remove(webcode);
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