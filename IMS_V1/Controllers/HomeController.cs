using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
namespace IMS_V1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Message = "Welcome to the Item Maintenance System.";
            ViewBag.Message = "Welcome " + Session.Contents["LogedUserFullName"] + " to the Item Management System.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacts";

            return View();
        }
        public ActionResult Login(int Login = 0, string ErrorMessage = "")
        {
            ViewBag.LoginError = ErrorMessage;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (IMS_V1Entities db = new IMS_V1Entities())
                {
                    var v = db.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session.Add("UserID", v.User_id);
                        Session.Add("UserTypeID", v.UserType_Id);
                        Session.Add("LogedUserFullName", v.FirstName.ToString() + " " + v.LastName.ToString());
                        Session.Add("Logout", "false");
                        //Session.Add("CreateAPlusImport", v.CreateAPlusImport_MarineShooting);
                        int usertypeid = int.Parse(Session.Contents["UserTypeId"].ToString());
                        if (usertypeid == 2)
                        {
                            return RedirectToAction("Index", "Item");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        Session.Add("LogedUserFullName", "");
                        Session.Add("Logout", "true");
                        return RedirectToAction("Login", new { Login = 1, ErrorMessage = "UserName or Password is incorrect.  Please try again." });
                    }
                }
            }
            return View(u);
        }


        public ActionResult Logout()
        {
            ViewBag.Message = "Good Bye " + Session.Contents["LogedUserFullname"] + ". You have been logged out.";
            Session.Add("Logout", "true");
            Session.Add("UserTypeID", null);
            Session.Add("UserID", null);
            Session.Abandon();
            return RedirectToAction("Login");
            //return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUpload(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            } 

            return View();
        }


    }
}
