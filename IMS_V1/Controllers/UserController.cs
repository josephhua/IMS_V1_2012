using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS_V1.Controllers
{
    public class UserController : Controller
    {
        private IMS_V1Entities db = new IMS_V1Entities();

        //
        // GET: /User/

        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.UserType);
            return View(users.ToList());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.UserType_Id = new SelectList(db.UserTypes, "UserType_id", "UserTypeDescription");
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "User_Id")]User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                int userid = int.Parse(Session.Contents["UserID"].ToString());
                user.ModifiedBy = userid;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserType_Id = new SelectList(db.UserTypes, "UserType_id", "UserTypeDescription", user.UserType_Id);
            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserType_Id = new SelectList(db.UserTypes, "UserType_id", "UserTypeDescription", user.UserType_Id);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                int userid = int.Parse(Session.Contents["UserID"].ToString());
                user.ModifiedBy = userid;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserType_Id = new SelectList(db.UserTypes, "UserType_id", "UserTypeDescription", user.UserType_Id);
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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