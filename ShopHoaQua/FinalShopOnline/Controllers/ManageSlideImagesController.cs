using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalShopOnline.Models;

namespace FinalShopOnline.Controllers
{
    public class ManageSlideImagesController : Controller
    {
        private ShopOnlineDb db = new ShopOnlineDb();

        // GET: ManageSlideImages
        public ActionResult Index()
        {
            return View(db.SlideImages.ToList());
        }

        // GET: ManageSlideImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return HttpNotFound();
            }
            return View(slideImage);
        }

        // GET: ManageSlideImages/Create
        public ActionResult Create()
        {
            // BEGIN: Tạo ra dropdownlist cho trường Status
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem { Text = "ACTIVE", Value = "ACTIVE" });
            statusList.Add(new SelectListItem { Text = "DEACTIVE", Value = "DEACTIVE" });

            ViewBag.Status = new SelectList(statusList, "Value", "Text");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường DisplayPosition
            List<SelectListItem> displayPositionList = new List<SelectListItem>();
            displayPositionList.Add(new SelectListItem { Text = "NON-DISPLAY", Value = "NON-DISPLAY" });
            displayPositionList.Add(new SelectListItem { Text = "SLIDE STYLE", Value = "SLIDE STYLE" });
            displayPositionList.Add(new SelectListItem { Text = "PRODUCT CERTIFICATION STYLE", Value = "PRODUCT CERTIFICATION STYLE" });

            ViewBag.DisplayPosition = new SelectList(displayPositionList, "Value", "Text");
            // END: Tạo ra dropdownlist cho trường DisplayPosition

            return View();
        }

        // POST: ManageSlideImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status,DisplayPosition,SortOrder,ImageUrl")] SlideImage slideImage)
        {
            if (ModelState.IsValid)
            {
                db.SlideImages.Add(slideImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem { Text = "ACTIVE", Value = "ACTIVE" });
            statusList.Add(new SelectListItem { Text = "DEACTIVE", Value = "DEACTIVE" });

            ViewBag.Status = new SelectList(statusList, "Value", "Text");

            List<SelectListItem> displayPositionList = new List<SelectListItem>();
            displayPositionList.Add(new SelectListItem { Text = "NON-DISPLAY", Value = "NON-DISPLAY" });
            displayPositionList.Add(new SelectListItem { Text = "SLIDE STYLE", Value = "SLIDE STYLE" });
            displayPositionList.Add(new SelectListItem { Text = "PRODUCT CERTIFICATION STYLE", Value = "PRODUCT CERTIFICATION STYLE" });

            ViewBag.DisplayPosition = new SelectList(displayPositionList, "Value", "Text");
            // END: Tạo ra dropdownlist cho trường DisplayPosition


            return View(slideImage);
        }

        // GET: ManageSlideImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return HttpNotFound();
            }

            // BEGIN: Tạo ra dropdownlist cho trường Status
            List<SelectListItem> statusEditList = new List<SelectListItem>();
            statusEditList.Add(new SelectListItem { Text = "ACTIVE", Value = "ACTIVE" });
            statusEditList.Add(new SelectListItem { Text = "DEACTIVE", Value = "DEACTIVE" });

            ViewBag.Status = new SelectList(statusEditList, "Value", "Text");

            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường DisplayPosition
            List<SelectListItem> displayPositionEditList = new List<SelectListItem>();
            displayPositionEditList.Add(new SelectListItem { Text = "NON-DISPLAY", Value = "NON-DISPLAY" });
            displayPositionEditList.Add(new SelectListItem { Text = "SLIDE STYLE", Value = "SLIDE STYLE" });
            displayPositionEditList.Add(new SelectListItem { Text = "PRODUCT CERTIFICATION STYLE", Value = "PRODUCT CERTIFICATION STYLE" });

            ViewBag.DisplayPosition = new SelectList(displayPositionEditList, "Value", "Text");
            // END: Tạo ra dropdownlist cho trường DisplayPosition

            return View(slideImage);
        }

        // POST: ManageSlideImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status,DisplayPosition,SortOrder,ImageUrl")] SlideImage slideImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slideImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slideImage);
        }

        // GET: ManageSlideImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return HttpNotFound();
            }
            return View(slideImage);
        }

        // POST: ManageSlideImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SlideImage slideImage = db.SlideImages.Find(id);
            db.SlideImages.Remove(slideImage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // UPLOAD IMAGE
        public ActionResult UploadImage(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(HttpPostedFileBase file, int id)
        {
            var defaultFolderToSaveFile = "~/Uploads/Images/" + id + "/";

            // BEGIN: Kiểm tra nếu chưa tồn tại thư mục trên thì tạo mới. 
            if (System.IO.Directory.Exists(Server.MapPath(defaultFolderToSaveFile)) == false)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(defaultFolderToSaveFile));
            }
            // END: Kiểm tra nếu chưa tồn tại thư mục trên thì tạo mới. 

            if (ModelState.IsValid)
            {
                // Kiểm tra nếu người dùng có chọn file
                if (file != null && file.ContentLength > 0)
                {
                    // Lấy tên file
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    if (fileName != null)
                    {
                        // Đường dẫn đầy đủ trên Server gồm path + filename
                        var path = System.IO.Path.Combine(Server.MapPath(defaultFolderToSaveFile), fileName);

                        var i = 1;
                        while (System.IO.File.Exists(path))
                        {
                            path = System.IO.Path.Combine(Server.MapPath(defaultFolderToSaveFile), i + "_" + fileName); // id + ".jpg"
                            i++;
                        }

                        // Upload file lên Server ở thư mục ~/Uploads/...
                        file.SaveAs(path);

                        // Lấy imageurl để lưu vào database, có định dạng "~/Uploads/Images/SlideImages/Id/ten_file.jpg"
                        var imageUrl = defaultFolderToSaveFile + fileName;
                        if (i > 1)
                        {
                            imageUrl = defaultFolderToSaveFile + (i - 1) + "_" + fileName;
                        }
                        // Lưu thông tin image url vào SlideImages
                        var slideImage = db.SlideImages.Find(id);
                        slideImage.ImageUrl = imageUrl;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
