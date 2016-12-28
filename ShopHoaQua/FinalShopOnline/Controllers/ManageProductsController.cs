using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalShopOnline.Models;
using System.IO;
using System;

namespace FinalShopOnline.Controllers
{
    public class ManageProductsController : Controller
    {
        private ShopOnlineDb db = new ShopOnlineDb();

        // GET: ManageProducts
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: ManageProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ManageProducts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");

            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường DisplayPosition
            var displayPositionItems = new[]
            {
                new { Id = "NON-DISPLAY", Name = "non-display" },
                new { Id = "SAN PHAM KHUYEN MAI", Name = "san pham khuyen mai" },
                new { Id = "SAN PHAM NOI BAT", Name = "san pham noi bat" },
                new { Id = "SAN PHAM BAN CHAY", Name = "san pham ban chay" },
            };

            ViewBag.DisplayPosition = new SelectList(displayPositionItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường DisplayPosition

            return View();
        }

        // POST: ManageProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,Code,Name,Price,Stock,Description,Notes,Discount,DisplayPosition,Status,SortOrder,Unit")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = System.DateTime.Now;
                product.ImageUrl = "#";
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");

            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường DisplayPosition
            var displayPositionItems = new[]
            {
                new { Id = "NON-DISPLAY", Name = "non-display" },
                new { Id = "SAN PHAM KHUYEN MAI", Name = "san pham khuyen mai" },
                new { Id = "SAN PHAM NOI BAT", Name = "san pham noi bat" },
                new { Id = "SAN PHAM BAN CHAY", Name = "san pham ban chay" },
            };

            ViewBag.DisplayPosition = new SelectList(displayPositionItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường DisplayPosition
            return View(product);
        }

        // GET: ManageProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);

            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường DisplayPosition
            var displayPositionItems = new[]
            {
                new { Id = "DEFAULT", Name = "Default" },
                new { Id = "SLIDE", Name = "slide" },
                new { Id = "SAN PHAM KHUYEN MAI", Name = "san pham khuyen mai" },
                new { Id = "SAN PHAM BAN CHAY", Name = "san pham ban chay" },
                new { Id = "SAN PHAM NOI BAT", Name = "san pham noi bat" }
            };

            ViewBag.DisplayPosition = new SelectList(displayPositionItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường DisplayPosition

            return View(product);
        }

        // POST: ManageProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Code,Name,Price,Stock,Description,Notes,Discount,DisplayPosition,Status,SortOrder,ImageUrl,Unit")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = System.DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);

            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường DisplayPosition
            var displayPositionItems = new[]
            {
                new { Id = "DEFAULT", Name = "Default" },
                new { Id = "SLIDE", Name = "slide" },
                new { Id = "SAN PHAM KHUYEN MAI", Name = "san pham khuyen mai" },
                new { Id = "SAN PHAM BAN CHAY", Name = "san pham ban chay" },
                new { Id = "SAN PHAM NOI BAT", Name = "san pham noi bat" }
            };

            ViewBag.DisplayPosition = new SelectList(displayPositionItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường DisplayPosition
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: ManageProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ManageProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id); 
            var orderItem = db.OrderDetails.Where(x => x.ProductId == id).ToList().Count;
            if (orderItem > 0)
            {
                return RedirectToAction("Index");
            }
            db.Products.Remove(product);
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

                        // Lấy imageurl để lưu vào database, có định dạng "~/Uploads/Products/Id/ten_file.jpg"
                        var imageUrl = defaultFolderToSaveFile + fileName;
                        if (i > 1)
                        {
                            imageUrl = defaultFolderToSaveFile + (i-1) + "_" + fileName;
                        }
                        // Lưu thông tin image url vào product
                        var product = db.Products.Find(id);
                        product.ImageUrl = imageUrl;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
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

        // UPLOAD PRODUCT DESCRIPTIONS
        public ActionResult UploadProductDescription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadProductDescription([Bind(Include = "Id,CategoryId,Code,Name,Price,Stock,Description,Notes,Discount,DisplayPosition,Status,SortOrder,ImageUrl,Unit")]Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = System.DateTime.Now;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Search(string SearchText)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.Include(p => p.Category).Where(x => x.Name.Contains(SearchText) || x.DisplayPosition.Contains(SearchText)).ToList();
                return View("Index", products);
            }
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
