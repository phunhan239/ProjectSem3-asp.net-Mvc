using FinalShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace FinalShopOnline.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.ToList();

                int pageSize = 9;
                int pageNumber = (page ?? 1);

                return View(products.ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Product
        public ActionResult ListStyleIndex(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.ToList();

                int pageSize = 9;
                int pageNumber = (page ?? 1);

                return View(products.ToPagedList(pageNumber, pageSize));
            }
        }

        // San pham khuyen mai
        // GET: Product
        public ActionResult SanphamkhuyenmaiGrid(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.Where(x => x.Status == "ACTIVE" && x.DisplayPosition == "SAN PHAM KHUYEN MAI").OrderBy(x => x.SortOrder).ToList();

                int pageSize = 9;
                int pageNumber = (page ?? 1);

                return View(products.ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Product
        public ActionResult SanphamkhuyenmaiList(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.Where(x => x.Status == "ACTIVE" && x.DisplayPosition == "SAN PHAM KHUYEN MAI").OrderBy(x => x.SortOrder).ToList();

                int pageSize = 9;
                int pageNumber = (page ?? 1);

                return View(products.ToPagedList(pageNumber, pageSize));
            }
        }

        // San pham noi bat
        // GET: Product
        public ActionResult SanphamnoibatGrid(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.Where(x => x.Status == "ACTIVE" && x.DisplayPosition == "SAN PHAM NOI BAT").OrderBy(x => x.SortOrder).ToList();

                int pageSize = 9;
                int pageNumber = (page ?? 1);

                return View(products.ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Product
        public ActionResult SanphamnoibatList(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.Where(x => x.Status == "ACTIVE" && x.DisplayPosition == "SAN PHAM NOI BAT").OrderBy(x => x.SortOrder).ToList();

                int pageSize = 6;
                int pageNumber = (page ?? 1);

                return View(products.ToPagedList(pageNumber, pageSize));
            }
        }

        // San pham 
        // GET: Product
        public ActionResult SanphambanchayGrid(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.Where(x => x.Status == "ACTIVE" && x.DisplayPosition == "SAN PHAM BAN CHAY").OrderBy(x => x.SortOrder).ToList();

                int pageSize = 9;
                int pageNumber = (page ?? 1);

                return View(products.ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Product
        public ActionResult SanphambanchayList(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.Where(x => x.Status == "ACTIVE" && x.DisplayPosition == "SAN PHAM BAN CHAY").OrderBy(x => x.SortOrder).ToList();

                int pageSize = 9;
                int pageNumber = (page ?? 1);

                return View(products.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult Details(int? id)
        {
            using (var db = new ShopOnlineDb())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Product product = db.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }

        }

        [HttpPost]
        public ActionResult Search(string SearchText)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.Where(x => x.Name.Contains(SearchText)).ToList();

                return View(products);
            }
        }

       

        public ActionResult DisplayByCategory(int categoryId, int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.Where(x => x.CategoryId == categoryId).ToList();

                int pageSize = 9;
                int pageNumber = (page ?? 1);

                return View("index", products.ToPagedList(pageNumber, pageSize));
                
            }
        }

        public ActionResult GetAll()
        {
            using (var db = new ShopOnlineDb())
            {
                var products = db.Products.ToList();
                return View("Index", products);
            }
        }

    }
}