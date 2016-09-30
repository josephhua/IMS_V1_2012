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
using PagedList;
using System.ComponentModel.DataAnnotations;


namespace IMS_V1.Controllers
{
    public class ItemController : Controller
    {
        private IMS_V1Entities db = new IMS_V1Entities();

        //
        // GET: /Item/

        public ActionResult Index(string currentFilter, string SearchString, int? page, string orderType) 
        {
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            int userid = int.Parse(Session.Contents["UserId"].ToString());
            int usertypeid = int.Parse(Session.Contents["UserTypeId"].ToString());
            string userfullname = Session.Contents["LogedUserFullName"].ToString();
            ViewBag.userid = userid;
            ViewBag.CurrentFilter = SearchString;
            var items = from r in
                            db.Items
                        select r;

            if (!String.IsNullOrEmpty(SearchString))
            {
                items = items.Where(r => r.Item_Description.ToUpper().Contains(SearchString.ToUpper()) || r.Itm_Num.ToUpper().Contains(SearchString.ToUpper()) || r.MFG_Number.ToUpper().Contains(SearchString.ToUpper()));
            }
//            else
//            {
//                items = items.Where(r => r.ReadyForApproval == "N");
//            }

            if (usertypeid != 1)
            {
                items = items.Where(r => r.CreatedBy == userid);
            }
            string OT = orderType;
            if (OT == "MR")
            {
                items = items.OrderByDescending(r => r.Item_id);
                ViewBag.OrderType = OT;
            }
            else
            {
                items = items.OrderBy(r => r.zManufacturersLogo.APlusVendorName).OrderBy(r => r.CategoryClass.CategoryName).OrderBy(r => r.Item_Description);// ascending;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);

            return View(items.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Item/Details/5

        public ActionResult Details(int id = 0)
        {

            return View();
        }

//        [HttpPost]
//        public ActionResult SaveFastTrack([Bind(Exclude = "Item_id")]Item item)
//        {

//            var PlanYN = item.Plan_YN;
//            var Haz = item.Haz;
//            int userid = int.Parse(Session.Contents["UserID"].ToString());
////            if (Request.Form["ddlVendor"])

////            item.ManufacturerLogo_Id = Request.Form["ddlVendor"];
////ddlVendor
////ddlCategory
////ddlSubClasses
////ddlFineLine
////MFG_Number = 99999
////UM_Id = 25
////VICost = 0.00
////MSRP = 0.00
////Level1 = 0.00
////Level2 = 0.00
////Level3 = 0.00
////JSCLevel5 = 0.00
////ddlWebcode 13
////ddlFreight = 16
////Plan_YN = "N"
////ABC_Id = 9
////STD=1
////MIN=1
//            db.Items.Add(item);
//            item.CreatedBy = userid;
//            item.CreatedDate = DateTime.Now;
//            var excl = Request.Form["chkExclusive"];
//            if (excl == "on")
//            {

//                item.Exclusive = "Y";
//            }
//            else
//            {
//                item.Exclusive = "N";
//            }

//            var alloc = Request.Form["chkAllocated"];
//            if (alloc == "on")
//            {
//                item.Allocated = "Y";
//            }
//            else
//            {
//                item.Allocated = "N";
//            }

//            var ds = Request.Form["chkDropShip"];
//            if (ds == "on")
//            {
//                item.DropShip = "Y";
//            }
//            else
//            {
//                item.DropShip = "N";
//            }

//            var pfw = Request.Form["chkPreventFromWeb"];
//            if (pfw == "on")
//            {
//                item.PreventFromWeb = "Y";
//            }
//            else
//            {
//                item.PreventFromWeb = "N";
//            }

//            var so = Request.Form["chkSpecialOrder"];
//            //string chkspecialorder;
//            if (so == "on")
//            {
//                item.SpecialOrder = "Y";
//            }
//            else
//            {
//                item.SpecialOrder = "N";
//            }

//            item.FastTrack = "Y";
//            item.ReadyForApproval = "Y";
//            item.Approved = "Y";
//            item.ApprovedBy = userid;
//            item.ApprovedDate = DateTime.Now;
//            item.FastTrackBy = userid;
//            item.FastTrackDate = DateTime.Now;
//                    db.SaveChanges();
//                    var Item_Id = item.Item_id;
//                    var CategoryClass_Id = item.CategoryClass_Id;
////                    GetCategoryClass(Item_Id);
//                    if (ViewBag.CategoryClass == "N")
//                    {
//                        string nd = item.Item_Description;
//                        var resultItemDescription = db.Database.ExecuteSqlCommand("UpdateItemDescription @Item_Id,@NewDescription,@UserId", new SqlParameter("@Item_Id", Item_Id),
//                                                                                                    new SqlParameter("@NewDescription", nd),
//                                                                                                    new SqlParameter("@UserId", int.Parse(Session.Contents["UserId"].ToString())));
//                    }
//                    if (item.WareHousesList != null)
//                    {
//                        foreach (int WareHouseId in item.WareHousesList)
//                        {
//                            //Stored procedure
//                            var result = db.Database.ExecuteSqlCommand("AddItemWareHouse @Item_Id,@WareHouse_Id", new SqlParameter("@Item_Id", Item_Id),
//                                                                                                new SqlParameter("@WareHouse_Id", WareHouseId));
//                        }
//                    }
//                    //Stored procedure
//                    if ((item.Approved == "Y" || item.FastTrack == "Y") && (item.Itm_Num == "" || item.Itm_Num == null))
//                    {
//                        var result1 = db.Database.ExecuteSqlCommand("UpdateItemsWithItm_Num @Item_id,@CategoryClass_Id", new SqlParameter("@Item_id", Item_Id),
//                                                                                            new SqlParameter("@CategoryClass_Id", CategoryClass_Id));
//                    }

//                    //Stored procedure
//                    var resultAttributes = db.Database.ExecuteSqlCommand("AddNewItemDefaultAttributes @Item_Id,@CategoryClass_Id,@User_Id", new SqlParameter("@Item_Id", Item_Id),
//                                                                                            new SqlParameter("@CategoryClass_Id", CategoryClass_Id),
//                                                                                            new SqlParameter("@User_Id", userid));
//                    return RedirectToAction("Index", "ItemAttribute", new { id = Item_Id });
////                }

////  If there is an error. 

//            //var VendorNameList = db.zManufacturersLogoes.Select(z => new
//            //{
//            //    ManufacturerLogo_Id = z.ManufacturerLogo_Id,
//            //    Description = z.APlusVendorName + "(" + z.VendorNumber + ")",
//            //    OB = z.APlusVendorName
//            //})
//            //                                        .ToList();
//            //ViewBag.VendorName = new SelectList(VendorNameList.OrderBy(ml => ml.OB), "ManufacturerLogo_Id", "Description");

//            //var ABC = db.ABC_Lookup.Select(a => new
//            //{
//            //    ABC_Id = a.ABC_Id,
//            //    Description = a.ABC_Code + " - " + a.ABC_Description
//            //})
//            //                                        .ToList();
//            //ViewBag.ABC_Lookup = new SelectList(ABC, "ABC_Id", "Description");
//            ////            ViewBag.ABC_Lookup = new SelectList(db.ABC_Lookup, "ABC_Id", "ABC_Description");
//            //var CategoryClass = db.CategoryClasses.Select(c => new
//            //{
//            //    CategoryClass_Id = c.CategoryClass_Id,
//            //    Description = c.Category_Id + " - " + c.CategoryName
//            //})
//            //                                        .ToList();
//            //ViewBag.CategoryClass = new SelectList(CategoryClass, "CategoryClass_Id", "Description");
//            //var SubClass = db.SubClasses.Select(s => new
//            //{
//            //    SubClassCode_Id = s.SubClassCode_Id,
//            //    Description = s.SubClass_Id + " - " + s.SubClassName
//            //})
//            //                                        .ToList();
//            //ViewBag.SubClass = new SelectList(SubClass, "SubClassCode_Id", "Description");
//            //var FineLineClass = db.FineLineClasses.Select(f => new
//            //{
//            //    FineLineCode_Id = f.FineLineCode_Id,
//            //    Description = f.FineLine_Id + " - " + f.FinelineName
//            //})
//            //                                        .ToList();
//            //ViewBag.FineLineClass = new SelectList(FineLineClass, "FineLineCode_Id", "Description");
//            //var CatWebCode = db.WebCodes.Select(w => new
//            //{
//            //    CatWebCode_Id = w.CatWebCode_Id,
//            //    Description = w.WebCode1 + " - " + w.WebCodeDescription
//            //})
//            //                                        .ToList();
//            //ViewBag.CatWebCode_Id = new SelectList(CatWebCode, "CatWebCode_Id", "Description");


//            ////ViewBag.CategoryClasses = db.CategoryClasses.ToList();
//            ////ViewBag.SubClasses = db.SubClasses.ToList();
//            ////ViewBag.FineLineClasses = db.FineLineClasses.ToList();

//            //var Freight = db.Freight_Lookup.Select(fl => new
//            //{
//            //    FreightLookup_Id = fl.Freight_Id,
//            //    Description = fl.Freight_APlusClass + " - " + fl.Freight_ItemClass
//            //})
//            //                            .ToList();
//            //ViewBag.Freight_Lookup = new SelectList(Freight, "FreightLookup_Id", "Description");
//            ////ViewBag.Freight_Lookup = new SelectList(db.Freight_Lookup, "Freight_Id", "Freight_ItemClass");
//            //ViewBag.UM_Lookup = new SelectList(db.UM_Lookup, "UM_Id", "UM_Description");
        //            //ViewBag.Plan = LoadYesNo(PlanYN);
//            //ViewBag.Hazardous = LoadHazardous(Haz);
//            //ViewBag.FFLType = new SelectList(db.FFLTypes, "FFLType_Id", "FFLType_Description");

//            //ViewBag.WareHousesList = LoadWarehouseList();



////            return View(item);
//        }


        //
        // GET: /Item/CopyNew

        public ActionResult CopyNew(int id = 0, int userid = 0)
        {

           var result2 = db.GetNewItem(id, userid);
           ViewBag.NewItemList = result2.ToList();
          
            foreach (var nii in ViewBag.NewItemList){
                ViewBag.NewItemId = nii;
            }

            Item item = db.Items.Find(ViewBag.NewItemId);
            if (item == null)
            {
                return HttpNotFound();
            }

            ViewBag.Itemid = item.Item_id;
            ViewBag.VendorName = new SelectList(db.zManufacturersLogoes.OrderBy(ml => ml.APlusVendorName), "ManufacturerLogo_Id", "APlusVendorName", item.ManufacturerLogo_Id);
            ViewBag.VendorNumber = db.zManufacturersLogoes.Where(vt => vt.ManufacturerLogo_Id == item.ManufacturerLogo_Id)
                                                       .Select(vt => vt.VendorNumber).FirstOrDefault();
            ViewBag.VendorAbbrev = db.zManufacturersLogoes.Where(vt => vt.ManufacturerLogo_Id == item.ManufacturerLogo_Id)
                                                       .Select(vt => vt.Abbrev).FirstOrDefault();


            var ABC = db.ABC_Lookup.Select(a => new
            {
                ABC_Id = a.ABC_Id,
                Description = a.ABC_Code + " - " + a.ABC_Description
            })
                                                    .ToList();
            ViewBag.ABC_Lookup = new SelectList(ABC, "ABC_Id", "Description", item.ABC_Id);
            //            ViewBag.ABC_Id = new SelectList(db.ABC_Lookup, "ABC_Id", "ABC_Code", item.ABC_Id);
            var CategoryClass = db.CategoryClasses.Select(c => new
            {
                CategoryClass_Id = c.CategoryClass_Id,
                Description = c.Category_Id + " - " + c.CategoryName
            })
                                                    .ToList();
            ViewBag.CategoryClass = new SelectList(CategoryClass, "CategoryClass_Id", "Description", item.CategoryClass_Id);
            var CategoryClassId = db.CategoryClasses.Where(c1 => c1.CategoryClass_Id == item.CategoryClass_Id)
                                    .Select(c1 => c1.Category_Id).FirstOrDefault();
            var SubClass = db.SubClasses.Where(s => s.Category_Id == CategoryClassId)
                .Select(s => new
                {
                    SubClassCode_Id = s.SubClassCode_Id,
                    Description = s.SubClass_Id + " - " + s.SubClassName
                })
                                                    .ToList();
            ViewBag.SubClass = new SelectList(SubClass, "SubClassCode_Id", "Description", item.SubClassCode_Id);
            var SubClassId = db.SubClasses.Where(s1 => s1.SubClassCode_Id == item.SubClassCode_Id)
                 .Select(s1 => s1.SubClass_Id).FirstOrDefault();
            var FineLineClass = db.FineLineClasses.Where(f => f.Category_Id == CategoryClassId && f.SubClass_id == SubClassId)
                .Select(f => new
                {
                    FineLineCode_Id = f.FineLineCode_Id,
                    Description = f.FineLine_Id + " - " + f.FinelineName

                })
                                                    .ToList();
            ViewBag.Item_Description = item.Item_Description;
            ViewBag.APlusDescription1 = item.APlusDescription1;
            ViewBag.APlusDescription2 = item.APlusDescription2;
            ViewBag.FineLineClass = new SelectList(FineLineClass, "FineLineCode_Id", "Description", item.FineLineCode_Id);
            var Freight = db.Freight_Lookup.Select(fl => new
            {
                FreightLookup_Id = fl.Freight_Id,
                Description = fl.Freight_APlusClass + " - " + fl.Freight_ItemClass
            })
                                                    .ToList();
            ViewBag.Freight_Lookup = new SelectList(Freight, "FreightLookup_Id", "Description", item.Freight_Id);
//            ViewBag.Freight_Id = new SelectList(db.Freight_Lookup, "Freight_Id", "Freight_APlusClass", item.Freight_Id);
            ViewBag.UM_Lookup = new SelectList(db.UM_Lookup, "UM_Id", "UM_Description",item.UM_Id);
            var CatWebCode = db.WebCodes.Select(w => new
            {
                CatWebCode_Id = w.CatWebCode_Id,
                Description = w.WebCode1 + " - " + w.WebCodeDescription
            })
                                                    .ToList();
            ViewBag.CatWebCode_Id = new SelectList(CatWebCode, "CatWebCode_Id", "Description",item.CatWebCode_Id);
            //            ViewBag.CatWebCode_Id = new SelectList(db.WebCodes, "CatWebCode_Id", "WebCode1", item.CatWebCode_Id);
            ViewBag.FFLType = new SelectList(db.FFLTypes, "FFLType_Id", "FFLType_Description", item.FFLType_Id);

            ViewBag.CreatedUser = db.Users.Where(usr => usr.User_id == item.CreatedBy)
                                                       .Select(usr => usr.FirstName + " " + usr.LastName).FirstOrDefault();
            GetExistingWareHousesList(item.Item_id);
            ViewBag.Plan = LoadYesNo(item.Plan_YN);
            ViewBag.WholeSale_MTP = LoadYesNo(item.WholeSaleMTP);
            ViewBag.Hazardous = LoadHazardous(item.Haz);
            GetRemainingAttributeTypesCount(id);

            return RedirectToAction("Edit", "Item", new { id = ViewBag.NewItemId });

        }
        //
        // GET: /Item/Create

        public ActionResult Create()
        {
            var ABC = db.ABC_Lookup.Select(a => new
            {
                ABC_Id = a.ABC_Id,
                Description = a.ABC_Code + " - " + a.ABC_Description
            })
                                                    .ToList();
            ViewBag.ABC_Lookup = new SelectList(ABC, "ABC_Id", "Description");
//            ViewBag.ABC_Lookup = new SelectList(db.ABC_Lookup, "ABC_Id", "ABC_Description");
            //ViewBag.CategoryClass = new SelectList(db.CategoryClasses, "CategoryClass_Id", "CategoryName");
            ViewBag.UserType = int.Parse(Session.Contents["UserTypeID"].ToString());
           string userfullname = Session.Contents["LogedUserFullName"].ToString();
           ViewBag.Buyer = userfullname;

            var CategoryClass = db.CategoryClasses.Select(c => new
            {
                CategoryClass_Id = c.CategoryClass_Id,
                Description = c.Category_Id + " - " + c.CategoryName
            })
                                                    .ToList();
            ViewBag.CategoryClass = new SelectList(CategoryClass, "CategoryClass_Id", "Description");
            var SubClass = db.SubClasses.Select(s => new
            {
                SubClassCode_Id = s.SubClassCode_Id,
                Description = s.SubClass_Id + " - " + s.SubClassName
            })
                                                    .ToList();
            ViewBag.SubClass = new SelectList(SubClass, "SubClassCode_Id", "Description");
            var FineLineClass = db.FineLineClasses.Select(f => new
            {
                FineLineCode_Id = f.FineLineCode_Id,
                Description = f.FineLine_Id + " - " + f.FinelineName
            })
                                                    .ToList();
            ViewBag.FineLineClass = new SelectList(FineLineClass, "FineLineCode_Id", "Description");


            var VendorNameList = db.zManufacturersLogoes.Select(z => new
            {
                ManufacturerLogo_Id = z.ManufacturerLogo_Id,
                Description = z.APlusVendorName + "(" + z.VendorNumber + ") ("+z.Abbrev+")",
                OB = z.APlusVendorName
            })
                                                    .ToList();
            ViewBag.VendorName = new SelectList(VendorNameList.OrderBy(ml => ml.OB), "ManufacturerLogo_Id", "Description");

            
            var Freight = db.Freight_Lookup.Select(fl => new
            {
                FreightLookup_Id = fl.Freight_Id,
                Description = fl.Freight_APlusClass + " - " + fl.Freight_ItemClass
            })
                                                  .ToList();
            ViewBag.Freight_Lookup = new SelectList(Freight, "FreightLookup_Id", "Description");
            //ViewBag.Freight_Lookup = new SelectList(db.Freight_Lookup, "Freight_Id", "Freight_ItemClass");
            ViewBag.UM_Lookup = new SelectList(db.UM_Lookup, "UM_Id", "UM_Description");

            var CatWebCode = db.WebCodes.Select(w => new
            {
                CatWebCode_Id = w.CatWebCode_Id,
                Description = w.WebCode1 + " - " + w.WebCodeDescription
            })
                                                    .ToList();
            ViewBag.CatWebCode_Id = new SelectList(CatWebCode, "CatWebCode_Id", "Description");
            
            //ViewBag.CatWebCode_Id = new SelectList(db.WebCodes, "CatWebCode_Id", "WebCodeDescription");
            ViewBag.WareHousesList = LoadWarehouseList();
            ViewBag.Plan = LoadYesNo("");
            ViewBag.WholeSale_MTP = LoadYesNo("");
            ViewBag.Hazardous = LoadHazardous("");

            ViewBag.FFLType = new SelectList(db.FFLTypes, "FFLType_Id", "FFLType_Description");


            //GetRemainingAttributeTypesCount(id);

            return View();
        }

        //
        // POST: /Item/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Item_id")]Item model)
        {
            var PlanYN = model.Plan_YN;
            var Haz = model.Haz;
            var WholeSaleMTP = model.WholeSaleMTP;
            if (int.Parse(Session.Contents["UserTypeID"].ToString()) != 6)
            {
                if (WholeSaleMTP == null)
                {
                    ModelState.AddModelError("WholeSaleMTP", "Please select a WholeSale MTP value.");
                }
                if (model.Level1 == null)
                {
                    ModelState.AddModelError("Level1", "Please enter a valid dollar amount.");
                }
                if (model.Level2 == null)
                {
                    ModelState.AddModelError("Level2", "Please enter a valid dollar amount.");
                }
                if (model.Level3 == null)
                {
                    ModelState.AddModelError("Level3", "Please enter a valid dollar amount.");
                }
                if (model.JSCLevel5 == null)
                {
                    ModelState.AddModelError("JSCLevel5", "Please enter a valid dollar amount.");
                }
                if (model.CatWebCode_Id == null)
                {
                    ModelState.AddModelError("CatWebCode_Id", "Please select a Web Code.");
                }
                if (model.Freight_Id == null)
                {
                    ModelState.AddModelError("Freight_Id", "Please select a Freight.");
                }
                if (model.ABC_Id == null)
                {
                    ModelState.AddModelError("ABC_Id", "Please select an ABC.");
                }
                if (model.STD == null)
                {
                    ModelState.AddModelError("STD", "Please enter a numeric value less than 99999.");
                }
                if (model.MIN == null)
                {
                    ModelState.AddModelError("MIN", "Please enter a numeric value less than 99999.");
                }
                if (model.VICost == null)
                {
                    ModelState.AddModelError("VICost", "Please enter a valid dollar amount.");
                }
            }
            else {
                model.WholeSaleMTP = null;
                model.Level1 = 0;
                model.Level2 = 0;
                model.Level3 = 0;
                model.JSCLevel5 = 0;
                model.CatWebCode_Id = 8; // Ellett/JSC WEB/Catalog
                model.Freight_Id = null;
                model.ABC_Id = null;
                model.STD = 1;
                model.MIN = "1";
                model.VICost = 1;
                model.MinAdvertisePrice = 0;

            }
            try
            {
                if (ModelState.IsValid)
                {

                    int userid = int.Parse(Session.Contents["UserID"].ToString());
                    db.Items.Add(model);
                    model.CreatedBy = userid;
                    model.CreatedDate = DateTime.Now;
                    var excl = Request.Form["chkExclusive"];
                    if (excl == "on")
                    {

                        model.Exclusive = "Y";
                    }
                    else
                    {
                        model.Exclusive = "N";
                    }

                    var alloc = Request.Form["chkAllocated"];
                    if (alloc == "on")
                    {
                        model.Allocated = "Y";
                    }
                    else
                    {
                        model.Allocated = "N";
                    }

                    var ds = Request.Form["chkDropShip"];
                    if (ds == "on")
                    {
                        model.DropShip = "Y";
                    }
                    else
                    {
                        model.DropShip = "N";
                    }

                    var pfw = Request.Form["chkPreventFromWeb"];
                    if (pfw == "on")
                    {
                        model.PreventFromWeb = "Y";
                    }
                    else
                    {
                        model.PreventFromWeb = "N";
                    }

                    var so = Request.Form["chkSpecialOrder"];
                    //string chkspecialorder;
                    if (so == "on")
                    {
                        model.SpecialOrder = "Y";
                    }
                    else
                    {
                        model.SpecialOrder = "N";
                    }

                    var rfa = Request.Form["chkReadyForApproval"];
                    //string chkspecialorder;
                    if (rfa == "on")
                    {
                        model.ReadyForApproval = "Y";
                    }
                    else
                    {
                        model.ReadyForApproval = "N";
                    }

                    var ft = Request.Form["chkFastTrack"];
                    //string chkspecialorder;
                    if (ft == "on")
                    {
                        model.FastTrack = "Y";
                    }
                    else
                    {
                        model.FastTrack = "N";
                    }

                    if (model.FastTrack == "Y") 
                    {
                        model.ReadyForApproval = "Y";
                        model.Approved = "Y";
                        model.ApprovedBy = userid;
                        model.ApprovedDate = DateTime.Now;
                        model.FastTrackBy = userid;
                        model.FastTrackDate = DateTime.Now;
                    }
                    db.SaveChanges();
                    var Item_Id = model.Item_id;
                    var CategoryClass_Id = model.CategoryClass_Id;
                    GetCategoryClass(Item_Id);
                    //if (ViewBag.CategoryClass == "N")
                    //{ 
                    string nd = model.Item_Description;
                    var resultItemDescription = db.Database.ExecuteSqlCommand("UpdateItemDescription @Item_Id,@NewDescription,@UserId", new SqlParameter("@Item_Id", Item_Id),
                                                                                                new SqlParameter("@NewDescription", nd),
                                                                                                new SqlParameter("@UserId", int.Parse(Session.Contents["UserId"].ToString())));
                    //}
                    if (model.WareHousesList != null)
                    {
                        foreach (int WareHouseId in model.WareHousesList)
                        {
                            //Stored procedure
                            var result = db.Database.ExecuteSqlCommand("AddItemWareHouse @Item_Id,@WareHouse_Id", new SqlParameter("@Item_Id", Item_Id),
                                                                                                new SqlParameter("@WareHouse_Id", WareHouseId));
                        }
                    }
                    //Stored procedure
                    if ((model.Approved == "Y" ||model.FastTrack =="Y") && (model.Itm_Num == "" || model.Itm_Num == null))
                    {
                        var result1 = db.Database.ExecuteSqlCommand("UpdateItemsWithItm_Num @Item_id,@CategoryClass_Id", new SqlParameter("@Item_id", Item_Id),
                                                                                            new SqlParameter("@CategoryClass_Id", CategoryClass_Id));
                    }

                    //Stored procedure
                    var resultAttributes = db.Database.ExecuteSqlCommand("AddNewItemDefaultAttributes @Item_Id,@CategoryClass_Id,@User_Id", new SqlParameter("@Item_Id", Item_Id),
                                                                                            new SqlParameter("@CategoryClass_Id", CategoryClass_Id),
                                                                                            new SqlParameter("@User_Id", userid));
                    return RedirectToAction("Index", "ItemAttribute", new { id = Item_Id });
                }
                else
                {
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            HttpContext.Response.Write(error);
                        }
                    }

                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.  Try, again later please." );
            }

            var VendorNameList = db.zManufacturersLogoes.Select(z => new
            {
                ManufacturerLogo_Id = z.ManufacturerLogo_Id,
                Description = z.APlusVendorName + "(" + z.VendorNumber + ") (" + z.Abbrev + ")",
                OB = z.APlusVendorName
            })
                                                    .ToList();
            ViewBag.VendorName = new SelectList(VendorNameList.OrderBy(ml => ml.OB), "ManufacturerLogo_Id", "Description");
            ViewBag.Buyer = Session.Contents["LogedUserFullName"].ToString(); 

            var ABC = db.ABC_Lookup.Select(a => new
            {
                ABC_Id = a.ABC_Id,
                Description = a.ABC_Code + " - " + a.ABC_Description
            })
                                                    .ToList();
            ViewBag.ABC_Lookup = new SelectList(ABC, "ABC_Id", "Description");
//            ViewBag.ABC_Lookup = new SelectList(db.ABC_Lookup, "ABC_Id", "ABC_Description");
            var CategoryClass = db.CategoryClasses.Select(c => new
            {
                CategoryClass_Id = c.CategoryClass_Id,
                Description = c.Category_Id + " - " + c.CategoryName
            })
                                                    .ToList();
            ViewBag.CategoryClass = new SelectList(CategoryClass, "CategoryClass_Id", "Description");
            var SubClass = db.SubClasses.Select(s => new
            {
                SubClassCode_Id = s.SubClassCode_Id,
                Description = s.SubClass_Id + " - " + s.SubClassName
            })
                                                    .ToList();
            ViewBag.SubClass = new SelectList(SubClass, "SubClassCode_Id", "Description");
            var FineLineClass = db.FineLineClasses.Select(f => new
            {
                FineLineCode_Id = f.FineLineCode_Id,
                Description = f.FineLine_Id + " - " + f.FinelineName
            })
                                                    .ToList();
            ViewBag.FineLineClass = new SelectList(FineLineClass, "FineLineCode_Id", "Description");
            var CatWebCode = db.WebCodes.Select(w => new
            {
                CatWebCode_Id = w.CatWebCode_Id,
                Description = w.WebCode1 + " - " + w.WebCodeDescription
            })
                                                    .ToList();
            ViewBag.CatWebCode_Id = new SelectList(CatWebCode, "CatWebCode_Id", "Description");

            
            //ViewBag.CategoryClasses = db.CategoryClasses.ToList();
            //ViewBag.SubClasses = db.SubClasses.ToList();
            //ViewBag.FineLineClasses = db.FineLineClasses.ToList();

            var Freight = db.Freight_Lookup.Select(fl => new
            {
                FreightLookup_Id = fl.Freight_Id,
                Description = fl.Freight_APlusClass + " - " + fl.Freight_ItemClass
            })
                                        .ToList();
            ViewBag.Freight_Lookup = new SelectList(Freight, "FreightLookup_Id", "Description");
            //ViewBag.Freight_Lookup = new SelectList(db.Freight_Lookup, "Freight_Id", "Freight_ItemClass");
            ViewBag.UM_Lookup = new SelectList(db.UM_Lookup, "UM_Id", "UM_Description");
            ViewBag.Plan = LoadYesNo(PlanYN);
            ViewBag.WholeSale_MTP = LoadYesNo(WholeSaleMTP);
            ViewBag.Hazardous = LoadHazardous(Haz);
            ViewBag.FFLType = new SelectList(db.FFLTypes, "FFLType_Id", "FFLType_Description");

            ViewBag.WareHousesList = LoadWarehouseList();



            return View(model);
        }

        //
        // GET: /Item/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserType = int.Parse(Session.Contents["UserTypeID"].ToString());
            ViewBag.Itemid = item.Item_id;
            var VendorNameList = db.zManufacturersLogoes.Select(z => new
            {
                ManufacturerLogo_Id = z.ManufacturerLogo_Id,
                Description = z.APlusVendorName + "(" + z.VendorNumber + ") (" + z.Abbrev + ")",
                OB = z.APlusVendorName
            })
                                                    .ToList();
            ViewBag.VendorName = new SelectList(VendorNameList.OrderBy(ml => ml.OB), "ManufacturerLogo_Id", "Description",item.ManufacturerLogo_Id);
            ViewBag.VendorNumber = db.zManufacturersLogoes.Where(vt => vt.ManufacturerLogo_Id == item.ManufacturerLogo_Id)
                                                       .Select(vt => vt.VendorNumber).FirstOrDefault();
            ViewBag.VendorAbbrev = db.zManufacturersLogoes.Where(vt => vt.ManufacturerLogo_Id == item.ManufacturerLogo_Id)
                                                       .Select(vt => vt.Abbrev).FirstOrDefault();


            var ABC = db.ABC_Lookup.Select(a => new
            {
                ABC_Id = a.ABC_Id,
                Description = a.ABC_Code + " - " + a.ABC_Description
            })
                                                    .ToList();
            ViewBag.ABC_Lookup = new SelectList(ABC, "ABC_Id", "Description", item.ABC_Id);
//            ViewBag.ABC_Id = new SelectList(db.ABC_Lookup, "ABC_Id", "ABC_Code", item.ABC_Id);
            var CategoryClass = db.CategoryClasses.Select(c => new
            {
                CategoryClass_Id = c.CategoryClass_Id,
                Description = c.Category_Id + " - " + c.CategoryName
            })
                                                    .ToList();
            ViewBag.CategoryClass = new SelectList(CategoryClass, "CategoryClass_Id", "Description", item.CategoryClass_Id);
            var CategoryClassId = db.CategoryClasses.Where(c1 => c1.CategoryClass_Id == item.CategoryClass_Id)
                                    .Select(c1 => c1.Category_Id).FirstOrDefault();
            ViewBag.CategoryClassDisplay = db.CategoryClasses.Where(cc => cc.CategoryClass_Id == item.CategoryClass_Id)
                                            .Select(cc => cc.Category_Id + "-" + cc.CategoryName).FirstOrDefault();
            var SubClass = db.SubClasses.Where(s => s.Category_Id == CategoryClassId)
                .Select(s => new
                {
                    SubClassCode_Id = s.SubClassCode_Id,
                    Description = s.SubClass_Id + " - " + s.SubClassName
                })
                                                    .ToList();
            ViewBag.SubClass = new SelectList(SubClass, "SubClassCode_Id", "Description", item.SubClassCode_Id);
            var SubClassId = db.SubClasses.Where(s1 => s1.SubClassCode_Id == item.SubClassCode_Id)
                 .Select(s1 => s1.SubClass_Id).FirstOrDefault();
            ViewBag.SubClassDisplay = db.SubClasses.Where(sc => sc.SubClassCode_Id == item.SubClassCode_Id)
                                            .Select(sc => sc.SubClass_Id + "-" + sc.SubClassName).FirstOrDefault();
            var FineLineClass = db.FineLineClasses.Where(f => f.Category_Id == CategoryClassId && f.SubClass_id == SubClassId)
                .Select(f => new
                {
                    FineLineCode_Id = f.FineLineCode_Id,
                    Description = f.FineLine_Id + " - " + f.FinelineName

                })
                                                    .ToList();
            ViewBag.FineLineClass = new SelectList(FineLineClass, "FineLineCode_Id", "Description", item.FineLineCode_Id);
            ViewBag.FineLineDisplay = db.FineLineClasses.Where(fl => fl.FineLineCode_Id == item.FineLineCode_Id)
                                            .Select(fl => fl.FineLine_Id + "-" + fl.FinelineName).FirstOrDefault();
            ViewBag.Item_Description = item.Item_Description;
            ViewBag.APlusDescription1 = item.APlusDescription1;
            ViewBag.APlusDescription2 = item.APlusDescription2;
            var Freight = db.Freight_Lookup.Select(fl => new
            {
                FreightLookup_Id = fl.Freight_Id,
                Description = fl.Freight_APlusClass + " - " + fl.Freight_ItemClass
            })
                                        .ToList();
            ViewBag.Freight_Lookup = new SelectList(Freight, "FreightLookup_Id", "Description", item.Freight_Id);
            //ViewBag.Freight_Id = new SelectList(db.Freight_Lookup, "Freight_Id", "Freight_APlusClass", item.Freight_Id);
            ViewBag.UM_Lookup = new SelectList(db.UM_Lookup, "UM_Id", "UM_Description",item.UM_Id);

            var CatWebCode = db.WebCodes.Select(w => new
            {
                CatWebCode_Id = w.CatWebCode_Id,
                Description = w.WebCode1 + " - " + w.WebCodeDescription
            })
                                                    .ToList();
            ViewBag.CatWebCode_Id = new SelectList(CatWebCode, "CatWebCode_Id", "Description",item.CatWebCode_Id);
            ViewBag.FFLType = new SelectList(db.FFLTypes, "FFLType_Id", "FFLType_Description", item.FFLType_Id);

            ViewBag.FFLTypeDisplay = db.FFLTypes.Where(ffl => ffl.FFLType_Id == item.FFLType_Id)
                                            .Select(ffl => ffl.FFLType_Code + "-" + ffl.FFLType_Description).FirstOrDefault();
            ViewBag.FFLLock = item.FFLLock;
            if (item.FFLLock == "Y") {
                ViewBag.FFLCaliberDisplay = item.FFLCaliber;
                ViewBag.FFLGaugeDisplay = item.FFLGauge;
                ViewBag.FFLModelDisplay = item.FFLModel;
                ViewBag.FFLMFGNameDisplay = item.FFLMFGName;
                ViewBag.FFLMFGImportNameDisplay = item.FFLMFGImportName;

            }
            ViewBag.CreatedUser = db.Users.Where(usr => usr.User_id == item.CreatedBy)
                                                       .Select(usr => usr.FirstName + " " + usr.LastName).FirstOrDefault();
            GetExistingWareHousesList(item.Item_id);
            ViewBag.Plan = LoadYesNo(item.Plan_YN);
            ViewBag.WholeSale_MTP = LoadYesNo(item.WholeSaleMTP);
            ViewBag.Hazardous = LoadHazardous(item.Haz);
            ViewBag.ReadyForApproval = item.ReadyForApproval;
            GetRemainingAttributeTypesCount(id);
            var msrp = Math.Round((((item.MSRP - item.VICost) / item.MSRP) * 100).Value,0);
            if (msrp != 0)
            {
                if (msrp.ToString().Substring(0, 1) == "-")
                {
                    ViewBag.Msrp_Percent = "";
                }
                else 
                {
                    ViewBag.Msrp_Percent = msrp.ToString();
                }
            }
            else
            {
                ViewBag.Msrp_Percent = "";
            }

            if (int.Parse(Session.Contents["UserTypeID"].ToString()) != 6)
            {
                var level1 = Math.Round((((item.Level1 - item.VICost) / item.Level1) * 100).Value, 0);
                if (level1 != 0)
                {
                    if (level1.ToString().Substring(0, 1) == "-")
                    {
                        ViewBag.Level1_Percent = "";
                    }
                    else
                    {
                        ViewBag.Level1_Percent = level1.ToString();
                    }
                }
                else
                {
                    ViewBag.Level1_Percent = "";
                }
                var level2 = Math.Round((((item.Level2 - item.VICost) / item.Level2) * 100).Value, 0);
                if (level2 != 0)
                {
                    if (level2.ToString().Substring(0, 1) == "-")
                    {
                        ViewBag.Level2_Percent = "";
                    }
                    else
                    {
                        ViewBag.Level2_Percent = level2.ToString();
                    }
                }
                else
                {
                    ViewBag.Level2_Percent = "";
                }
                var level3 = Math.Round((((item.Level3 - item.VICost) / item.Level3) * 100).Value, 0);
                if (level3 != 0)
                {
                    if (level3.ToString().Substring(0, 1) == "-")
                    {
                        ViewBag.Level3_Percent = "";
                    }
                    else
                    {
                        ViewBag.Level3_Percent = level3.ToString();
                    }
                }
                else
                {
                    ViewBag.Level3_Percent = "";
                }
                var level5 = Math.Round((((item.JSCLevel5 - item.VICost) / item.JSCLevel5) * 100).Value, 0);
                if (level5 != 0)
                {
                    if (level5.ToString().Substring(0, 1) == "-")
                    {
                        ViewBag.Level5_Percent = "";
                    }
                    else
                    {
                        ViewBag.Level5_Percent = level5.ToString();
                    }
                }
                else
                {
                    ViewBag.Level5_Percent = "";
                }
            }
            return View(item);
        }

        //
        // POST: /Item/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (int.Parse(Session.Contents["UserTypeID"].ToString()) != 6)
            {
                if (item.WholeSaleMTP == null)
                {
                    ModelState.AddModelError("WholeSaleMTP", "Please select a WholeSale MTP value.");
                }
                if (item.Level1 == null)
                {
                    ModelState.AddModelError("Level1", "Please enter a valid dollar amount.");
                }
                if (item.Level2 == null)
                {
                    ModelState.AddModelError("Level2", "Please enter a valid dollar amount.");
                }
                if (item.Level3 == null)
                {
                    ModelState.AddModelError("Level3", "Please enter a valid dollar amount.");
                }
                if (item.JSCLevel5 == null)
                {
                    ModelState.AddModelError("JSCLevel5", "Please enter a valid dollar amount.");
                }
                if (item.CatWebCode_Id == null)
                {
                    ModelState.AddModelError("CatWebCode_Id", "Please select a Web Code.");
                }
                if (item.Freight_Id == null)
                {
                    ModelState.AddModelError("Freight_Id", "Please select a Freight.");
                }
                if (item.ABC_Id == null)
                {
                    ModelState.AddModelError("ABC_Id", "Please select an ABC.");
                }
                if (item.STD == null)
                {
                    ModelState.AddModelError("STD", "Please enter a numeric value less than 99999.");
                }
                if (item.MIN == null)
                {
                    ModelState.AddModelError("MIN", "Please enter a numeric value less than 99999.");
                }
                if (item.VICost == null)
                {
                    ModelState.AddModelError("VICost", "Please enter a valid dollar amount.");
                }
            }
  


            if (ModelState.IsValid)
                {
                    var excl = Request.Form["chkExclusive"];
                    if (excl == "on")
                    {

                        item.Exclusive = "Y";
                    }
                    else
                    {
                        item.Exclusive = "N";
                    }

                    var alloc = Request.Form["chkAllocated"];
                    if (alloc == "on")
                    {
                        item.Allocated = "Y";
                    }
                    else
                    {
                        item.Allocated = "N";
                    }

                    var ds = Request.Form["chkDropShip"];
                    if (ds == "on")
                    {
                        item.DropShip = "Y";
                    }
                    else
                    {
                        item.DropShip = "N";
                    }

                    var pfw = Request.Form["chkPreventFromWeb"];
                    if (pfw == "on")
                    {
                        item.PreventFromWeb = "Y";
                    }
                    else
                    {
                        item.PreventFromWeb = "N";
                    }

                    var so = Request.Form["chkSpecialOrder"];
                    //string chkspecialorder;
                    if (so == "on")
                    {
                        item.SpecialOrder = "Y";
                    }
                    else
                    {
                        item.SpecialOrder = "N";
                    }

                    var rfa = Request.Form["chkReadyForApproval"];
                    //string chkspecialorder;
                    if (rfa == "on")
                    {
                        item.ReadyForApproval = "Y";
                    }
                    else
                    {
                        item.ReadyForApproval = "N";
                    }

                    var ft = Request.Form["chkFastTrack"];
                    //string chkspecialorder;
                    if (ft == "on")
                    {
                        item.FastTrack = "Y";
                    }
                    else
                    {
                        item.FastTrack = "N";
                    }

                    var dbItems = db.Items.FirstOrDefault(p => p.Item_id == item.Item_id);
//                    if (int.Parse(Session.Contents["UserTypeID"].ToString()) == 6)
//                    {
//                        Level1 = item.Level1;                
//                    }

                    if (dbItems == null)
                    {
                        return HttpNotFound();
                    }
                    dbItems.ManufacturerLogo_Id = item.ManufacturerLogo_Id;
                    dbItems.Item_Description = item.Item_Description;
                    dbItems.APlusDescription1 = item.APlusDescription1;
                    dbItems.APlusDescription2 = item.APlusDescription2;
                    dbItems.MFG_Number = item.MFG_Number;
                    dbItems.UM_Id = item.UM_Id;
                    dbItems.MSRP = item.MSRP;
                    if (int.Parse(Session.Contents["UserTypeID"].ToString()) != 6)
                    { 
                        dbItems.Level1 = item.Level1;
                        dbItems.Level2 = item.Level2;
                        dbItems.Level3 = item.Level3;
                        dbItems.JSCLevel5 = item.JSCLevel5;
                        dbItems.Qty_Break = item.Qty_Break;
                        dbItems.Qty_BreakPrice = item.Qty_BreakPrice;
                        dbItems.CatWebCode_Id = item.CatWebCode_Id;
                        dbItems.Freight_Id = item.Freight_Id;
                        dbItems.STD = item.STD;
                        dbItems.MIN = item.MIN;
                        dbItems.WholeSaleMTP = item.WholeSaleMTP;
                        dbItems.VICost = item.VICost;
                        dbItems.MinAdvertisePrice = item.MinAdvertisePrice;
                    }
                    dbItems.CategoryClass_Id = item.CategoryClass_Id;
                    dbItems.SubClassCode_Id = item.SubClassCode_Id;
                    dbItems.FineLineCode_Id = item.FineLineCode_Id;
                    dbItems.Plan_YN = item.Plan_YN;
                    dbItems.ABC_Id = item.ABC_Id;
                    dbItems.DS = item.DS;
                    dbItems.Haz = item.Haz;
                    dbItems.UPC = item.UPC;
                    dbItems.EDIUPC = item.EDIUPC;
                    dbItems.Buyer = item.Buyer;
                    dbItems.Exclusive = item.Exclusive;
                    dbItems.Allocated = item.Allocated;
                    dbItems.DropShip = item.DropShip;
                    dbItems.PreventFromWeb = item.PreventFromWeb;
                    dbItems.SpecialOrder = item.SpecialOrder;

//                    dbItems.CreatedBy = int.Parse(Session.Contents["UserID"].ToString());
//                    dbItems.CreatedDate = item.CreatedDate;
                    dbItems.ReadyForApproval = item.ReadyForApproval;
                    if (item.ReadyForApproval == "Y" && item.ApprovedBy == null)
                    {
                        dbItems.Approved = item.ReadyForApproval;
                        dbItems.ApprovedBy = int.Parse(Session.Contents["UserId"].ToString());
                        dbItems.ApprovedDate = DateTime.Now;
                    }
                    if (item.FastTrack == "Y" && item.FastTrackBy == null)
                    {
                        dbItems.FastTrack = item.FastTrack;
                        dbItems.FastTrackBy = int.Parse(Session.Contents["UserId"].ToString());
                        dbItems.FastTrackDate = DateTime.Now;
                    }
                    dbItems.FFLCaliber = item.FFLCaliber;
                    dbItems.FFLMFGImportName = item.FFLMFGImportName;
                    dbItems.FFLMFGName = item.FFLMFGName;
                    dbItems.FFLModel = item.FFLModel;
                    dbItems.FFLGauge = item.FFLGauge;
                    dbItems.FFLType_Id = item.FFLType_Id;
                    db.SaveChanges();

                    var CategoryClass_Id = item.CategoryClass_Id;

                    //db.Entry(item).State = EntityState.Modified;
                    //db.SaveChanges();
                    var Item_Id = item.Item_id;
                  
                    GetCategoryClass(Item_Id);
                    //if (ViewBag.CategoryClass == "N" )
                    //{
                        string nd = item.Item_Description;
                        var resultItemDescription = db.Database.ExecuteSqlCommand("UpdateItemDescription @Item_Id,@NewDescription,@UserId", new SqlParameter("@Item_Id", Item_Id),
                                                                                                    new SqlParameter("@NewDescription", nd),
                                                                                                    new SqlParameter("@UserId", int.Parse(Session.Contents["UserId"].ToString())));
                    //}

                    //Stored procedure
                    if ((dbItems.Approved == "Y" || dbItems.FastTrack =="Y" ) && (dbItems.Itm_Num == "" || dbItems.Itm_Num == null))
                    {
                       var result1 = db.Database.ExecuteSqlCommand("UpdateItemsWithItm_Num @Item_id,@CategoryClass_Id", new SqlParameter("@Item_id", Item_Id),
                                                                                            new SqlParameter("@CategoryClass_Id", CategoryClass_Id));
                    }


                    var DeleteItemWareHouses = db.Database.ExecuteSqlCommand("DeleteItemWareHouses @Item_Id", new SqlParameter("@Item_Id", Item_Id));
                    if (item.WareHousesList != null)
                    {
                        foreach (int WareHouseId in item.WareHousesList)
                        {
                            //Stored procedure
                            var result = db.Database.ExecuteSqlCommand("AddItemWareHouse @Item_Id,@WareHouse_Id", new SqlParameter("@Item_Id", Item_Id),
                                                                                                 new SqlParameter("@WareHouse_Id", WareHouseId));
                        }
                    }
                    return RedirectToAction("Index", "ItemAttribute", new { id = Item_Id });
                }
            //}
            //catch (DataException)
            //{
            //    ModelState.AddModelError("", "Unable to save changes.  Try, again later please.");
            //}

            ViewBag.Itemid = item.Item_id;
            var VendorNameList = db.zManufacturersLogoes.Select(z => new
            {
                ManufacturerLogo_Id = z.ManufacturerLogo_Id,
                Description = z.APlusVendorName + "(" + z.VendorNumber + ") (" + z.Abbrev + ")",
                OB = z.APlusVendorName
            })
                                                    .ToList();
            ViewBag.VendorName = new SelectList(VendorNameList.OrderBy(ml => ml.OB), "ManufacturerLogo_Id", "Description", item.ManufacturerLogo_Id);
            ViewBag.VendorNumber = db.zManufacturersLogoes.Where(vt => vt.ManufacturerLogo_Id == item.ManufacturerLogo_Id)
                                                        .Select(vt => vt.VendorNumber).FirstOrDefault();
            ViewBag.VendorAbbrev = db.zManufacturersLogoes.Where(vt => vt.ManufacturerLogo_Id == item.ManufacturerLogo_Id)
                                                        .Select(vt => vt.Abbrev).FirstOrDefault();

            var ABC = db.ABC_Lookup.Select(a => new
            {
                ABC_Id = a.ABC_Id,
                Description = a.ABC_Code + " - " + a.ABC_Description
            })
                                                    .ToList();
            ViewBag.ABC_Lookup = new SelectList(ABC, "ABC_Id", "Description", item.ABC_Id);
            //ViewBag.ABC_Lookup = new SelectList(db.ABC_Lookup, "ABC_Id", "ABC_Description", item.ABC_Id);
            var CategoryClass = db.CategoryClasses.Select(c => new
            {
                CategoryClass_Id = c.CategoryClass_Id,
                Description = c.Category_Id + " - " + c.CategoryName
            })
                                                    .ToList();
            ViewBag.CategoryClass = new SelectList(CategoryClass, "CategoryClass_Id", "Description", item.CategoryClass_Id);
            ViewBag.CategoryClassDisplay = db.CategoryClasses.Where(cc => cc.CategoryClass_Id == item.CategoryClass_Id)
                                            .Select(cc => cc.Category_Id + "-" + cc.CategoryName).FirstOrDefault();
            var SubClass = db.SubClasses.Select(s => new
            {
                SubClassCode_Id = s.SubClassCode_Id,
                Description = s.SubClass_Id + " - " + s.SubClassName
            })
                                                    .ToList();
            ViewBag.SubClass = new SelectList(SubClass, "SubClassCode_Id", "Description", item.SubClassCode_Id);
            ViewBag.SubClassDisplay = db.SubClasses.Where(sc => sc.SubClassCode_Id == item.SubClassCode_Id)
                                            .Select(sc => sc.SubClass_Id + "-" + sc.SubClassName).FirstOrDefault();
            var FineLineClass = db.FineLineClasses.Select(f => new
            {
                FineLineCode_Id = f.FineLineCode_Id,
                Description = f.FineLine_Id + " - " + f.FinelineName
            })
                                                    .ToList();
            ViewBag.FineLineClass = new SelectList(FineLineClass, "FineLineCode_Id", "Description", item.FineLineCode_Id);
            ViewBag.FineLineDisplay = db.FineLineClasses.Where(fl => fl.FineLineCode_Id == item.FineLineCode_Id)
                                            .Select(fl => fl.FineLine_Id + "-" + fl.FinelineName).FirstOrDefault();
            ViewBag.Freight_Lookup = new SelectList(db.Freight_Lookup, "Freight_Id", "Freight_ItemClass", item.Freight_Id);
            ViewBag.UM_Lookup = new SelectList(db.UM_Lookup, "UM_Id", "UM_Description", item.UM_Id);
            var CatWebCode = db.WebCodes.Select(w => new
            {
                CatWebCode_Id = w.CatWebCode_Id,
                Description = w.WebCode1 + " - " + w.WebCodeDescription
            })
                                                    .ToList();
            ViewBag.CatWebCode_Id = new SelectList(CatWebCode, "CatWebCode_Id", "Description",item.CatWebCode_Id);
            //ViewBag.CatWebCode_Id = new SelectList(db.WebCodes, "CatWebCode_Id", "WebCode1", item.CatWebCode_Id);
            ViewBag.FFLType = new SelectList(db.FFLTypes, "FFLType_Id", "FFLType_Description", item.FFLType_Id);
            ViewBag.Plan = LoadYesNo(item.Plan_YN);
            ViewBag.WholeSale_MTP = LoadYesNo(item.WholeSaleMTP);
            ViewBag.Hazardous = LoadHazardous(item.Haz);

            ViewBag.CreatedUser = db.Users.Where(usr => usr.User_id == item.CreatedBy)
                                                       .Select(usr => usr.FirstName + " " + usr.LastName).FirstOrDefault();


            GetExistingWareHousesList(item.Item_id);
            
            GetRemainingAttributeTypesCount(item.Item_id);



            return View(item);
        }

            
            
        //
        // GET: /Item/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            GetWarehouseList(item.Item_id);
            return View(item);
        }

        //
        // POST: /Item/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            var DeleteItemWareHouses = db.Database.ExecuteSqlCommand("DeleteItemWareHouses @Item_Id", new SqlParameter("@Item_Id", id));
            var DeleteItemAttributes = db.Database.ExecuteSqlCommand("DeleteItemAttributes @Item_Id", new SqlParameter("@Item_Id", id));
            
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private IList<SubClass> GetSubClasses(int categoryclassid)
        {
            return (from s in db.SubClasses
                    join c in db.CategoryClasses on s.Category_Id equals c.Category_Id
                    where c.CategoryClass_Id == categoryclassid
                    select s).ToList();
        }


        //        private IList<FineLineClass> GetFineLineClasses(string categoryid, string subclassid)
        private IList<FineLineClass> GetFineLineClasses(int categoryclassid, int subclasscodeid)
        {
            return (from f in db.FineLineClasses
                    join c in db.CategoryClasses on f.Category_Id equals c.Category_Id
                    join s in db.SubClasses on f.SubClass_id equals s.SubClass_Id 
                    where c.CategoryClass_Id == categoryclassid && s.SubClassCode_Id == subclasscodeid
                    select f).ToList();
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadSubClassesByCategoryId(int categoryclassid)
        {

            var SubClassList = this.GetSubClasses(categoryclassid);
            var SubClassData = SubClassList.Select(s => new
            {
                Text = s.SubClass_Id + " - " + s.SubClassName,
                Value = s.SubClassCode_Id,
            });
            return Json(SubClassData, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadFineLineClassesByCateogryIdSubClassId(int categoryclassid, int subclasscodeid)
        {
            var FineLineList = this.GetFineLineClasses(categoryclassid, subclasscodeid);
            var FineLineData = FineLineList.Select(f => new
            {
                Text = f.FineLine_Id + " - " + f.FinelineName,
                Value = f.FineLineCode_Id,
            });
            return Json(FineLineData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadVendorInfo(int vtid)
        {
            var vinfo = (from v in db.zManufacturersLogoes
                         where v.ManufacturerLogo_Id == vtid
                         select new { v.APlusVendorName, v.Abbrev, v.VendorNumber, v.ManufacturerLogo_Id, v.WebVendorName }).SingleOrDefault();
            return Json(vinfo, JsonRequestBehavior.AllowGet);
        }


        private MultiSelectList LoadWarehouseList(List<int> selectedValues = null)
        {
            var list = db.WareHouse_Lookup.Select(whl => new
            {
                Id = whl.WareHouse_id,
                Name = whl.WareHouseName
            }).ToList();

            return new MultiSelectList(list, "Id", "Name", selectedValues);
        }

        private void GetWarehouseList(int Itemid)
        {
            //var list = (from t in db.WareHouse_Lookup
            //            select new { Id = t.WareHouse_id, Name = t.WareHouseNumber + " - " + t.WareHouseName }).ToList();
            var list = (from whl in db.WareHouse_Lookup
                        join iwh in db.ItemWareHouses on whl.WareHouse_id equals iwh.WareHouse_id
                        where iwh.Item_id == Itemid
                        select whl).ToList();
            ViewBag.ExistingWareHousesList = list;
        }



        private List<SelectListItem> LoadHazardous(string defaultValue)
        {
            //            if (defaultValue != null)
            //            {
            //                defaultValue = defaultValue.Trim();
            //            }
            var Hazardous = new List<SelectListItem>
                                    {new SelectListItem { Text = "Yes", Value = "Y ", Selected = (defaultValue == "Y ")},
                                            new SelectListItem {Text = "No", Value  = "N ", Selected = (defaultValue == "N ")},
                                            new SelectListItem {Text = "CS", Value  = "CS", Selected = (defaultValue == "CS")},
                                            new SelectListItem {Text = "CC", Value = "CC", Selected = (defaultValue == "CC")}
                                        };
            return Hazardous.ToList();
        }

        private List<SelectListItem> LoadYesNo(string defaultValue)
        {
            //          if (defaultValue != null){
            //              defaultValue = defaultValue.Trim();
            //          }
            var YesNo = new List<SelectListItem>
                                    {new SelectListItem { Text = "Yes", Value = "Y", Selected = (defaultValue == "Y")},
                                            new SelectListItem {Text = "No", Value  = "N", Selected = (defaultValue == "N")}
                                        };
            return YesNo.ToList();
        }

        public void GetCategoryClass(int Itemid)
        {
            var categoryClassid = db.Items.Where(i => i.Item_id == Itemid).Select(i => i.CategoryClass_Id).Single();
            if (categoryClassid == 1 || categoryClassid == 2 || categoryClassid == 3 )//|| categoryClassid == 9) Optics Removed.
            {
                ViewBag.CategoryClass = "Y";
            }
            else
            {
                ViewBag.CategoryClass = "N";
            }
        }
        //private List<SelectListItem> LoadFFLType(string defaultValue)
        //{
        //    //          if (defaultValue != null){
        //    //              defaultValue = defaultValue.Trim();
        //    //          }
        //    var list = (from ffl in FFLType
        //                select new SelectListItem
        //                    {
        //                        Text = ffl.FFLType_Code,
        //                        Value = ffl.FFLType_Description

        //                    });
        //    return FFLType.ToList();     
        //}


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

        public void GetExistingWareHousesList(int Itemid)
        {

            var results1 = db.GetExistingWareHousesList(Itemid);
            ViewBag.WareHousesList = results1.ToList();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CalculatePercentage(decimal percent,decimal vicost)
        {
            var model = new
            {
                Cost = Math.Round(vicost / (1 - (percent / 100)), 2).ToString("#.00"),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CheckForMFG_Number(string mfgnumber,string vendornumber,int itemid)
        {
            var DupCheck = (from i in db.Items
                            join u in db.Users on i.CreatedBy equals u.User_id
                            where i.MFG_Number == mfgnumber && i.zManufacturersLogo.VendorNumber == vendornumber && i.Item_id != itemid
                         select new { i.Itm_Num,i.Item_Description,u.FirstName,u.LastName }).FirstOrDefault();
            if (DupCheck == null)
            {
                var DupCheck2 = (from i in db.APlusItems
                                 where i.MFG_Number == mfgnumber && i.VendorNumber == vendornumber
                                 select new { i.Itm_Num,i.Item_Desc1,i.Item_Desc2}).FirstOrDefault();
                if (DupCheck2 == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(DupCheck2, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(DupCheck, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CheckForMFG_NumberCreate(string mfgnumber, string vendornumber)
        {
            var DupCheck = (from i in db.Items
                            join u in db.Users on i.CreatedBy equals u.User_id
                            where i.MFG_Number == mfgnumber && i.zManufacturersLogo.VendorNumber == vendornumber
                            select new { i.Itm_Num, i.Item_Description, u.FirstName, u.LastName }).FirstOrDefault();
            if (DupCheck == null)
            {
                var DupCheck2 = (from i in db.APlusItems
                                 where i.MFG_Number == mfgnumber && i.VendorNumber == vendornumber
                                 select new { i.Itm_Num, i.Item_Desc1, i.Item_Desc2 }).FirstOrDefault();
                if (DupCheck2 == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(DupCheck2, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(DupCheck, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CheckForEDIUPC(string ediupc, int itemid)
        {
            var EDIUPCCheck = (from i in db.Items
                            join u in db.Users on i.CreatedBy equals u.User_id
                            where i.UPC == ediupc && i.Item_id != itemid
                            select new { i.Itm_Num, i.Item_Description, u.FirstName, u.LastName }).FirstOrDefault();
            if (EDIUPCCheck == null)
            {
                var EDIUPCCheck2 = (from i in db.APlusItems
                                 where i.UPC == ediupc
                                 select new { i.Itm_Num, i.Item_Desc1, i.Item_Desc2 }).FirstOrDefault();
                if (EDIUPCCheck2 == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(EDIUPCCheck2, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(EDIUPCCheck, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CheckForEDIUPCCreate(string ediupc)
        {
            var EDIUPCCheck = (from i in db.Items
                            join u in db.Users on i.CreatedBy equals u.User_id
                            where i.UPC == ediupc
                            select new { i.Itm_Num, i.Item_Description, u.FirstName, u.LastName }).FirstOrDefault();
            if (EDIUPCCheck == null)
            {
                var EDIUPCCheck2 = (from i in db.APlusItems
                                 where i.UPC == ediupc
                                 select new { i.Itm_Num, i.Item_Desc1, i.Item_Desc2 }).FirstOrDefault();
                if (EDIUPCCheck2 == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(EDIUPCCheck2, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(EDIUPCCheck, JsonRequestBehavior.AllowGet);
            }
        
        }

    }
}