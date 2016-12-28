using FinalShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalShopOnline.Controllers
{
    public class AboutShopController : Controller
    {
        // GET: AboutShop
        public ActionResult Index()
        {
            return View();
        }

        // Get about shop article
        public ActionResult IntroduceStore()
        {
            var db = new ShopOnlineDb();
            var aboutShopArticle = db.AboutShops.Where(x => x.Title.Contains("Giới thiệu cửa hàng")).ToList();
            return View(aboutShopArticle);
        }

        // GET chinh sach doi tra hang
        public ActionResult ChinhSachDoiTra()
        {
            var db = new ShopOnlineDb();
            var chinhsachdoitra = db.AboutShops.Where(x => x.Title.Contains("Chính sách đổi trả")).ToList();
            return View(chinhsachdoitra);
        }

        // GET chinh sach doi ưu đãi
        public ActionResult ChinhSachUuDai()
        {
            var db = new ShopOnlineDb();
            var chinhsachuudai = db.AboutShops.Where(x => x.Title.Contains("Chính sách ưu đãi")).ToList();
            return View(chinhsachuudai);
        }

        // GET huong dan mua hang
        public ActionResult HuongDanMuaHang()
        {
            var db = new ShopOnlineDb();
            var huongdanmuahang = db.AboutShops.Where(x => x.Title.Contains("Hướng dẫn mua hàng")).ToList();
            return View(huongdanmuahang);
        }

        // GET huong dan thanh toan
        public ActionResult HuongDanThanhToan()
        {
            var db = new ShopOnlineDb();
            var huongdanthanhtoan = db.AboutShops.Where(x => x.Title.Contains("Hướng dẫn thanh toán")).ToList();
            return View(huongdanthanhtoan);
        }
    }
}