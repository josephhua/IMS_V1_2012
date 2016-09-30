using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
namespace IMS_V1.Controllers
{
    public class ItemAttributeController : Controller
    {
        private IMS_V1Entities db = new IMS_V1Entities();

        //
        // GET: /ItemAttribute/

        public ActionResult Index(int id)
        {
            var itemattribute = db.ItemAttributes.Include(i => i.Attribute_Lookup.AttributeType).Include(i => i.Item).Where(i => i.Item_Id == id);
            ViewBag.Itm_num = db.Items.Where(i => i.Item_id == id).Select(i => i.Itm_Num).Single();
            ViewBag.ItemDescription = db.Items.Where(i => i.Item_id == id).Select(i => i.Item_Description).Single();
            ViewBag.SFItemDescription = db.Items.Where(i => i.Item_id == id).Select(i => i.SF_Item_Description).Single();
            ViewBag.Printed = db.Items.Where(i => i.Item_id == id).Select(i => i.Printed).Single();
            ViewBag.Item_Id = id;
            ViewBag.ReadyForApproval = db.Items.Where(i => i.Item_id == id).Select(i => i.ReadyForApproval).Single();
            ViewBag.FastTrack = db.Items.Where(i => i.Item_id == id).Select(i => i.FastTrack).Single();
            ViewBag.Company99 = db.Items.Where(i => i.Item_id == id).Select(i => i.Company99).Single();
            ViewBag.Company99_List = new SelectList(
                                new List<SelectListItem>
                                {
                                    //new SelectListItem { Text = "", Value = "",Selected = (ViewBag.Company99 == "")},
                                    new SelectListItem { Text = "99", Value = "99",Selected = (ViewBag.Company99 == "99")},
                                }, "Value", "Text");
                            
            GetRemainingAttributeTypes(id);
            GetRemainingAttributeTypesCount(id);
            GetCategoryClass(id);
            if (ViewBag.ItemDescription != null) 
            { 
                int idl = ViewBag.ItemDescription.Length;
                if (idl > 31)
                {
                    ViewBag.AplusDesc1 = ViewBag.ItemDescription.Substring(0, 31);
                    ViewBag.APlusDesc2 = ViewBag.ItemDescription.Substring(31, idl - 31);
                }
                else
                {
                    ViewBag.AplusDesc1 = ViewBag.ItemDescription;
                    ViewBag.APlusDesc2 = "";
                }
            }
            return View(itemattribute.ToList());
        }

        [HttpPost]
        public ActionResult SaveDescription(int id)
        {

            string nd = Request.Form["txtItemDescription"];
            if (nd != null)
            {
                var result1 = db.Database.ExecuteSqlCommand("UpdateItemDescription @Item_Id,@NewDescription,@UserId", new SqlParameter("@Item_Id", id),
                                                                                            new SqlParameter("@NewDescription", nd),
                                                                                            new SqlParameter("@UserId", int.Parse(Session.Contents["UserId"].ToString())));
            }

            //string sfnd = Request.Form["txtSFItemDescription"];
            //if (sfnd != null)
            //{
            //    var resultSFItemDescription = db.Database.ExecuteSqlCommand("UpdateSFItemDescription @Item_Id,@NewSFDescription,@UserId", new SqlParameter("@Item_Id", id),
            //                                                                                new SqlParameter("@NewSFDescription", sfnd),
            //                                                                                new SqlParameter("@UserId", int.Parse(Session.Contents["UserId"].ToString())));
            //}
            var company99 = Request.Form["Company99"];
            string c99 = "";
            if (company99 != null)
            {
                c99 = company99;
            }

            string srfa = "";
            string sft = "";
            var rfa = Request.Form["chkReadyForApproval"];
            //string chkspecialorder;
            if (rfa == "on")
            {
                srfa = "Y";
            }
            else
            {
                srfa = "N";
            }

            var ft = Request.Form["chkFastTrack"];
            //string chkspecialorder;
            if (ft == "on")
            {
                sft = "Y";
            }
            else
            {
                sft = "N";
            }


            var result2 = db.Database.ExecuteSqlCommand("UpdateItemRFA_FT @Item_Id,@ReadyForApproval,@FastTrack,@UserId,@Company99", new SqlParameter("@Item_Id", id),
                                                                                        new SqlParameter("@ReadyForApproval", srfa),
                                                                                        new SqlParameter("@FastTrack", sft),
                                                                                        new SqlParameter("@UserId", int.Parse(Session.Contents["UserId"].ToString())),
                                                                                        new SqlParameter("@Company99",c99));
            
            
            return RedirectToAction("Index", new { id = id });

        }
        [HttpPost]
        public ActionResult SaveRFA_FastTrack(int id)
        {

            string srfa = "";
            string sft = "";
            var rfa = Request.Form["chkReadyForApproval"];
            //string chkspecialorder;
            if (rfa == "on")
            {
                srfa = "Y";
            }
            else
            {
                srfa = "N";
            }

            var ft = Request.Form["chkFastTrack"];
            //string chkspecialorder;
            if (ft == "on")
            {
                sft = "Y";
            }
            else
            {
                sft = "N";
            }


            var result1 = db.Database.ExecuteSqlCommand("UpdateItemRFA_FT @Item_Id,@ReadyForApproval,@FastTrack,@UserId", new SqlParameter("@Item_Id", id),
                                                                                        new SqlParameter("@ReadyForApproval", srfa),
                                                                                        new SqlParameter("@FastTrack", sft),
                                                                                        new SqlParameter("@UserId", int.Parse(Session.Contents["UserId"].ToString())));


            //if (item.ReadyForApproval == "Y" && item.ApprovedBy != null)
            //{
            //    dbItems.Approved = item.ReadyForApproval;
            //    dbItems.ApprovedBy = int.Parse(Session.Contents["UserId"].ToString());
            //    dbItems.ApprovedDate = DateTime.Now;
            //}
            //if (item.FastTrack == "Y" && item.FastTrackBy != null)
            //{
            //    dbItems.FastTrack = item.FastTrack;
            //    dbItems.FastTrackBy = int.Parse(Session.Contents["UserId"].ToString());
            //    dbItems.FastTrackDate = DateTime.Now;
            //}
            //if (dbItems.Approved == "Y" && dbItems.Itm_Num == "")
            //{
            //    var result1 = db.Database.ExecuteSqlCommand("UpdateItemsWithItm_Num @Item_id,@CategoryClass_Id", new SqlParameter("@Item_id", Item_Id),
            //                                                                         new SqlParameter("@CategoryClass_Id", Category_Id));
            //}


            return RedirectToAction("Index", new { id = id });

        }

        //
        // GET: /ItemAttribute/Details/5

        public ActionResult Details(int id = 0)
        {
            ItemAttribute itemattribute = db.ItemAttributes.Find(id);
            if (itemattribute == null)
            {
                return HttpNotFound();
            }
            return View(itemattribute);
        }

        //
        // GET: /ItemAttribute/Create

        public ActionResult Create(int item_id)
        {
            var AttributeType = db.AttributeTypes.Select(at => new
            {
                AttributeType_Id = at.AttributeType_Id,
                Description = at.AttributeType1
            }).OrderBy(al => al.Description)
                                                    .ToList();
            ViewBag.AttributeType = new SelectList(AttributeType, "AttributeType_Id", "Description");
            ViewBag.Itm_num = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Itm_Num).Single();
            ViewBag.ItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Item_Description).Single();
            ViewBag.SFItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.SF_Item_Description).Single();
            ViewBag.Printed = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Printed).Single();
            //ViewBag.AttributeLookup = new SelectList(db.Attribute_Lookup, "AttributeLookup_Id", "WebsiteAttributeValue");
            ViewBag.Item_Id = item_id;
            var AttributeLookup = db.Attribute_Lookup.Select(al => new
            {
                AttributeLookup_Id = al.AttributeLookup_Id,
                Description = al.WebsiteAttributeValue,
                Active = al.Active
            }).Where(al => al.Active == true)
            .OrderBy(al => al.Description)
                                                    .ToList();
            ViewBag.AttributeLookup = new SelectList(AttributeLookup, "AttributeLookup_Id", "Description");

            return View();
        }

        //
        // POST: /ItemAttribute/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ItemAttribute_Id")]ItemAttribute itemattribute)
        {
            if (ModelState.IsValid)
            {
                int usertypeid = int.Parse(Session.Contents["UserTypeId"].ToString());
                db.ItemAttributes.Add(itemattribute);
                itemattribute.AddedBy = usertypeid;
                itemattribute.AddedDate = DateTime.Now;
                string at = Request.Form["AttributeType_Id"];
                if (at == "" || at == null)
                {
                    at = "0";
                }
                int atid = int.Parse(at);
                if ((itemattribute.AttributeLookup_Id == 0 || itemattribute.AttributeLookup_Id == null) && atid != 0)
                {
                    int alid = db.Attribute_Lookup.Where(al => (al.AttributeType_Id == atid) && (al.WebsiteAttributeValue == "Not Selected")).Select(al => al.AttributeLookup_Id).Single();
                    itemattribute.AttributeLookup_Id = alid;
                }
                if (atid != 0)
                {
                    db.SaveChanges();
                }
                int item_id = itemattribute.Item_Id.Value;//db.ItemAttributes.Where(ia => ia.ItemAttribute_Id == id).Select(ia => ia.Item_Id).Single();
                ViewBag.Itm_num = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Itm_Num).Single();
                ViewBag.ItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Item_Description).Single();
                ViewBag.SFItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.SF_Item_Description).Single();

                var dbItems = db.Items.FirstOrDefault(i => i.Item_id == itemattribute.Item_Id);
                if (dbItems == null)
                {
                    return HttpNotFound();
                }

                GetCategoryClass(item_id);
                if (ViewBag.CategoryClass == "Y") 
                {
                    GetItemDescription(item_id);
                }
                

                return RedirectToAction("Index", new { id = itemattribute.Item_Id });
            }

            return View(itemattribute);
        }

        //
        // GET: /ItemAttribute/Edit/5

        public ActionResult Edit(string attributeName, int id = 0)
        {
            ItemAttribute itemattribute = db.ItemAttributes.Find(id);
            if (itemattribute == null)
            {
                return HttpNotFound();
            }
            int item_id = itemattribute.Item_Id.Value;
            int AttributeType_Id = db.Attribute_Lookup.Where(al => al.AttributeLookup_Id == itemattribute.AttributeLookup_Id.Value).Select(al => al.AttributeType_Id).Single();
            ViewBag.AttributeLookup = new SelectList(db.Attribute_Lookup.Where(al => al.AttributeType_Id == AttributeType_Id && al.Active == true).OrderBy(al => al.WebsiteAttributeValue), "AttributeLookup_Id", "WebsiteAttributeValue", itemattribute.AttributeLookup_Id);
            ViewBag.APlusValue = db.Attribute_Lookup.Where(al => al.AttributeLookup_Id == itemattribute.AttributeLookup_Id).Select(al => al.APlusAttributeValue).FirstOrDefault();
            ViewBag.DefaultValue = db.Attribute_Lookup.Where(al => al.AttributeLookup_Id == itemattribute.AttributeLookup_Id).Select(al => al.DefaultActualValue).FirstOrDefault();
            ViewBag.Printed = db.Items.Where(i => i.Item_id == id).Select(i => i.Printed).FirstOrDefault();
            ViewBag.AttributeType = attributeName;
            ViewBag.AttributeType_Id = AttributeType_Id;
            ViewBag.Itm_num = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Itm_Num).Single();
            ViewBag.ItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Item_Description).Single();
            ViewBag.SFItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.SF_Item_Description).Single();
            return View(itemattribute);
        }

        //
        // POST: /ItemAttribute/Edit/5

        [HttpPost]
        public ActionResult Edit(ItemAttribute itemattribute)
        {
            if (ModelState.IsValid)
            {

                var dbItemAttributes = db.ItemAttributes.FirstOrDefault(p => p.ItemAttribute_Id == itemattribute.ItemAttribute_Id);
                if (dbItemAttributes == null)
                {
                    return HttpNotFound();
                }
                //dbItemAttributes.AttributeLookup_Id = itemattribute.AttributeLookup_Id;
                dbItemAttributes.ActualAttributeValue = itemattribute.ActualAttributeValue;
                int usertypeid = int.Parse(Session.Contents["UserTypeId"].ToString());
                dbItemAttributes.ModifiedBy = usertypeid;
                dbItemAttributes.ModifiedDate = DateTime.Now;
                var PrintedDate = itemattribute.PrintedDate;
                if (PrintedDate != null)
                {
                    if (DateTime.Now > PrintedDate)
                    {
                        dbItemAttributes.PrintedDate = null;
                    }
                }


                if (itemattribute.AttributeLookup_Id == 0 || itemattribute.AttributeLookup_Id == null)
                {
                    int atid = int.Parse(Request.Form["hdnAttributeTypeId"]);

                    int alid = db.Attribute_Lookup.Where(al => (al.AttributeType_Id == atid) && (al.WebsiteAttributeValue == "Not Selected")).Select(al => al.AttributeLookup_Id).Single();
                    itemattribute.AttributeLookup_Id = alid;
                    dbItemAttributes.AttributeLookup_Id = itemattribute.AttributeLookup_Id;
                }
                else 
                { 
                    dbItemAttributes.AttributeLookup_Id = itemattribute.AttributeLookup_Id;
                }
                db.SaveChanges();
                int item_id = itemattribute.Item_Id.Value;//db.ItemAttributes.Where(ia => ia.ItemAttribute_Id == id).Select(ia => ia.Item_Id).Single();
                ViewBag.Itm_num = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Itm_Num).Single();
                ViewBag.ItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Item_Description).Single();
                ViewBag.SFItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.SF_Item_Description).Single();

                GetCategoryClass(item_id);
                if (ViewBag.CategoryClass == "Y")
                {
                    GetItemDescription(item_id);
                }

                return RedirectToAction("Index", new { id = itemattribute.Item_Id });
            }
            return View(itemattribute);
        }

        //
        // GET: /ItemAttribute/Delete/5

        public ActionResult Delete(string attributeName, int id = 0)
        {
            ItemAttribute itemattribute = db.ItemAttributes.Find(id);
            if (itemattribute == null)
            {
                return HttpNotFound();
            }
            int item_id = itemattribute.Item_Id.Value;
            ViewBag.WebsiteAttributeValue = db.Attribute_Lookup.Where(al => al.AttributeLookup_Id == itemattribute.AttributeLookup_Id).Select(al => al.WebsiteAttributeValue).FirstOrDefault();
            ViewBag.APlusAttributeValue = db.Attribute_Lookup.Where(al => al.AttributeLookup_Id == itemattribute.AttributeLookup_Id).Select(al => al.APlusAttributeValue).FirstOrDefault();
            ViewBag.Printed = db.Items.Where(i => i.Item_id == id).Select(i => i.Printed).FirstOrDefault();
            ViewBag.DefaultAttributeValue = db.Attribute_Lookup.Where(al => al.AttributeLookup_Id == itemattribute.AttributeLookup_Id).Select(al => al.DefaultActualValue).FirstOrDefault();
            ViewBag.ActualAttributeValue = itemattribute.ActualAttributeValue;
            ViewBag.AttributeType = attributeName;
            ViewBag.Itm_num = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Itm_Num).Single();
            ViewBag.ItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.Item_Description).Single();
            ViewBag.SFItemDescription = db.Items.Where(i => i.Item_id == item_id).Select(i => i.SF_Item_Description).Single();
            
            return View(itemattribute);
        }

        //
        // POST: /ItemAttribute/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemAttribute itemattribute = db.ItemAttributes.Find(id);
            int item_id = itemattribute.Item_Id.Value;//db.ItemAttributes.Where(ia => ia.ItemAttribute_Id == id).Select(ia => ia.Item_Id).Single();
            db.ItemAttributes.Remove(itemattribute);
            db.SaveChanges();

            GetCategoryClass(item_id);
            if (ViewBag.CategoryClass == "Y")
            {
                GetItemDescription(item_id);
            }


            return RedirectToAction("Index", new { id = item_id });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void GetItemDescription(int Itemid)
        {
            var result1 = db.Database.ExecuteSqlCommand("GetItemDescription @itemid, @Description", new SqlParameter("@itemid", Itemid),
                                                                        new SqlParameter("@Description", "1"));
            var result2 = db.Database.ExecuteSqlCommand("GetItemDescription @itemid, @Description", new SqlParameter("@itemid", Itemid),
                                                                        new SqlParameter("@Description", "2"));
            var resultw = db.Database.ExecuteSqlCommand("GetItemDescription @itemid, @Description", new SqlParameter("@itemid", Itemid),
                                                                        new SqlParameter("@Description", "W"));
        }


        public void GetCategoryClass(int Itemid)
        {
            var categoryClassid = db.Items.Where(i => i.Item_id == Itemid).Select(i => i.CategoryClass_Id).Single();
            if (categoryClassid == 1 || categoryClassid == 2 || categoryClassid == 3 )//|| categoryClassid == 9) Optics removed until description truely defined.
            {
                ViewBag.CategoryClass = "Y";
            }
            else
            {
                ViewBag.CategoryClass = "N";
            }
        }



        public void GetRemainingAttributeTypes(int Itemid)
        {

            var results1 = db.GetRemainingAttributeTypes(Itemid);
            ViewBag.RemainingAttributeTypes = results1.ToList();
        }
        public void GetRemainingAttributeTypesCount(int Itemid)
        {
            var results1 = db.GetRemainingAttributeTypes(Itemid).Where(r => r.Required == true);

            ViewBag.AllRequired = results1.Count();
        }

        private IList<Attribute_Lookup> GetAttributeLookup(int attributetypeid)
        {
            return (from al in db.Attribute_Lookup
                    where al.AttributeType_Id == attributetypeid && al.Active == true
                    orderby al.WebsiteAttributeValue
                    select al).ToList();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadAttributeInfo(int Aid)
        {
            var Ainfo = (from a in db.Attribute_Lookup
                         where a.AttributeLookup_Id == Aid
                         select new { a.APlusAttributeValue, a.DefaultActualValue,a.AttributeLookup_Id }).SingleOrDefault();
            return Json(Ainfo, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadAttributeLookup(int attributetypeid)
        {

            var AttributeLookupList = this.GetAttributeLookup(attributetypeid);
            var AttributeLookupData = AttributeLookupList.Select(al => new
            {
                Text = al.WebsiteAttributeValue,
                Value = al.AttributeLookup_Id,
                Active = al.Active,
            }).Where(al => al.Active == true);
            return Json(AttributeLookupData, JsonRequestBehavior.AllowGet);

        }

   
   }
}