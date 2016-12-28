using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalShopOnline.Models;
using System.IO;

namespace FinalShopOnline.Controllers
{
    public class ManageNicheInformationsController : Controller
    {
        private ShopOnlineDb db = new ShopOnlineDb();

        // GET: ManageNicheInformations
        public ActionResult Index()
        {
            return View(db.NicheInformations.ToList());
        }

        // GET: ManageNicheInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NicheInformation nicheInformation = db.NicheInformations.Find(id);
            if (nicheInformation == null)
            {
                return HttpNotFound();
            }
            return View(nicheInformation);
        }

        // GET: ManageNicheInformations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageNicheInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImageUrl,Title,HeadContent,CreateDate,BlogContent")] NicheInformation nicheInformation)
        {
            if (ModelState.IsValid)
            {
                nicheInformation.CreateDate = System.DateTime.Now;
                db.NicheInformations.Add(nicheInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nicheInformation);
        }

        // GET: ManageNicheInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NicheInformation nicheInformation = db.NicheInformations.Find(id);
            if (nicheInformation == null)
            {
                return HttpNotFound();
            }
            return View(nicheInformation);
        }

        // POST: ManageNicheInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImageUrl,Title,HeadContent,CreateDate,BlogContent")] NicheInformation nicheInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nicheInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nicheInformation);
        }

        // GET: ManageNicheInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NicheInformation nicheInformation = db.NicheInformations.Find(id);
            if (nicheInformation == null)
            {
                return HttpNotFound();
            }
            return View(nicheInformation);
        }

        // POST: ManageNicheInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NicheInformation nicheInformation = db.NicheInformations.Find(id);
            db.NicheInformations.Remove(nicheInformation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region SUMMERNOTE UPLOAD IMAGES

        [HttpPost]
        public ActionResult SummernoteUploadImage()
        {
            foreach (var fileKey in Request.Files.AllKeys)
            {
                var file = Request.Files[fileKey];
                try
                {
                    var fileName = Path.GetFileName(file?.FileName);
                    if (fileName != null)
                    {
                        var path = Server.MapPath("~/Uploads/SummernoteImages");
                        if (System.IO.File.Exists(path) == false)
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }

                        path = Path.Combine(Server.MapPath("~/Uploads/SummernoteImages"), fileName);
                        file.SaveAs(path);
                        return Json(new { Url = Url.Content("~/Uploads/SummernoteImages/" + fileName) });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { Message = $"Error in saving file ({ex.Message})" });
                }
            }
            return Json(new { Message = "File saved" });
        }

        #endregion

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
                        var nicheInformation = db.NicheInformations.Find(id);
                        nicheInformation.ImageUrl = imageUrl;
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
