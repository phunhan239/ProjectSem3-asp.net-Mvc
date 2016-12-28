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
    public class NicheInformationController : Controller
    {
        // GET: NiceFood
        public ActionResult Index(int? page)
        {
            using (var db = new ShopOnlineDb())
            {
                var nicheInformation = db.NicheInformations.ToList();
                int pageSize = 6;
                int pageNumber = (page ?? 1);

                return View(nicheInformation.ToPagedList(pageNumber, pageSize));
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

                NicheInformation nicheInformation = db.NicheInformations.FirstOrDefault(x => x.Id == id);

                if (nicheInformation == null)
                {
                    return HttpNotFound();
                }
                return View(nicheInformation);
            }
        }
    }
}