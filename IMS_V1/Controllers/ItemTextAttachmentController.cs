using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS_V1.Controllers
{
    public class ItemTextAttachmentController : Controller
    {
        private IMS_V1Entities db = new IMS_V1Entities();

        //
        // GET: /ItemTextAttachment/

        public ActionResult Index(int itemid)
        {
            //var itemtextattachments = db.ItemTextAttachments.Include(i => i.Item).Select(i=>i.Text).ToString();
            GetExistingLongDescriptions(itemid);

            ViewBag.Itm_num = db.Items.Where(i => i.Item_id == itemid).Select(i => i.Itm_Num).Single();

           ViewBag.ItemDescription = db.Items.Where(i => i.Item_id == itemid).Select(i => i.Item_Description).Single();
            //ViewBag.ItemDescription = db.Items.Where(i => i.Item_id == id).Select(i => i.Item_Description).Single();
            //byte[] binaryString = (byte[])db.Items.Where(i => i.Item_id == id).Select(i => i.Item_Description).Single();

            // if the original encoding was ASCII
            //string x = Encoding.ASCII.GetString(binaryString);
            ViewBag.Item_Id = itemid;
            return View();
        
        }

        //
        // GET: /ItemTextAttachment/Create

        public ActionResult Create(int itemid,string itmnum,string itemdescription)
        {
            ViewBag.Item_id = itemid;
            ViewBag.Itm_Num = itmnum;
            ViewBag.Item_Description = itemdescription;
            return View();
        }

        //
        // POST: /ItemTextAttachment/Create

        [HttpPost]
        public ActionResult Create(ItemTextAttachment itemtextattachment)
        {
            string txa = Request.Form["txaLongDescription"];
            int itemid = Convert.ToInt32(Request.Form["hdnItem_id"]);
            int userid = int.Parse(Session.Contents["UserId"].ToString());
            try
            {
                AddLongDescription(itemid, txa,userid);
                return RedirectToAction("Index", new { itemid = itemid });
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.  Try, again later please.");
            }
           
            return View();

        }

        //
        // GET: /ItemTextAttachment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            GetEditLongDescription(id);
            foreach (var ld in ViewBag.EditLongDescription)
            {
                ViewBag.ItemTextAttachment_Id = ld.ItemTextAttachment_Id;
                ViewBag.Item_id = ld.item_id;
                ViewBag.Itm_Num = ld.itm_num;
                ViewBag.Item_Description = ld.Item_Description;
                ViewBag.LongDescription = ld.LongDescription;
            }
            return View();
        }

        //
        // POST: /ItemTextAttachment/Edit/5

        [HttpPost]
        public ActionResult Edit(ItemTextAttachment itemtextattachment)
        {
            int id = Convert.ToInt32(Request.Form["hdnItemTextAttachment_Id"]);
            string txa = Request.Form["txaLongDescription"];
            string itemid = Request.Form["hdnItem_id"];

            try
            {
                UpdateLongDescription(id, txa);
                return RedirectToAction("Index", new { itemid = itemid });
            } catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.  Try, again later please." );
            }
    
            GetEditLongDescription(id);
            foreach (var ld in ViewBag.EditLongDescription)
            {
                ViewBag.Item_id = ld.item_id;
                ViewBag.Itm_Num = ld.itm_num;
                ViewBag.Item_Description = ld.Item_Description;
                ViewBag.LongDescription = ld.LongDescription;
            }
            return View();
        }

        //
        // GET: /ItemTextAttachment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            GetEditLongDescription(id);
            foreach (var ld in ViewBag.EditLongDescription)
            {
                ViewBag.ItemTextAttachment_Id = ld.ItemTextAttachment_Id;
                ViewBag.Item_id = ld.item_id;
                ViewBag.Itm_Num = ld.itm_num;
                ViewBag.Item_Description = ld.Item_Description;
                ViewBag.LongDescription = ld.LongDescription;
            }
            return View();
            
        }

        //
        // POST: /ItemTextAttachment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemTextAttachment itemtextattachment = db.ItemTextAttachments.Find(id);
            db.ItemTextAttachments.Remove(itemtextattachment);
            db.SaveChanges();
            string txa = Request.Form["txaLongDescription"];
            string itemid = Request.Form["hdnItem_id"];

            return RedirectToAction("Index", new { itemid = itemid });

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void GetExistingLongDescriptions(int Itemid)
        {

            var results2 = db.GetExistingLongDescriptions(Itemid);
            ViewBag.LongDescriptions = results2.ToList();
        }

        public void GetEditLongDescription(int id)
        {

            var results3 = db.GetLongDescription(id);
            ViewBag.EditLongDescription = results3.ToList();

        }

        public void UpdateLongDescription(int id,string LongDescription)
        {

            var results3 = db.UpdateLongDescription(id,LongDescription);

        }

        public void AddLongDescription(int id,string LongDescription,int userid)
        {

            var results3 = db.AddLongDescription(id,LongDescription,userid);

        }

    }
}