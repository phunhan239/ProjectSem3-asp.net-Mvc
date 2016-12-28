using FinalShopOnline.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalShopOnline.Controllers
{
    public class NiceFoodController : Controller
    {
        // GET: NiceFood
        public ActionResult Index(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var niceFoodInfor = db.NiceFoods.ToList();
                int pageSize = 6;
                int pageNumber = (page ?? 1);
              
                return View(niceFoodInfor.ToPagedList(pageNumber, pageSize));
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

                NiceFood nicefood = db.NiceFoods.FirstOrDefault(x => x.Id == id);

                if (nicefood == null)
                {
                    return HttpNotFound();
                }
                return View(nicefood);
            }
        }
    }
}