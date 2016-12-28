using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalShopOnline.Models;
using Microsoft.AspNet.Identity;
using System.Configuration;
using Common;

namespace FinalShopOnline.Controllers
{
    public class ShoppingCartController : Controller
    {

        public string GetCartNumbers()
        {
            if (Session["ShoppingCart"] != null)
            {
                var shoppingCart = (ShoppingCart)Session["ShoppingCart"];
                return shoppingCart.ShoppingCartItems.Count.ToString();
            }

            return "0";
        }

        public JsonResult GetShoppingCartItems()
        {
            // XE HÀNG
            var shoppingCart = new ShoppingCart();
            if (Session["ShoppingCart"] != null)
            {
                shoppingCart = (ShoppingCart)Session["ShoppingCart"];
            }
            return Json(shoppingCart.ShoppingCartItems.Select(x => new { x.Item.Id, x.Item.Name, x.Quantity, x.Item.Price, x.Item.Discount, x.Item.ImageUrl }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: ShoppingCart
        [HttpPost]
        public ActionResult AddToCart(int ProductId, int Quantity)
        {

            using (var db = new ShopOnlineDb())
            {

                // TÌM PRODUCT THEO ProductId
                var product = db.Products.Find(ProductId);
                if (product != null)
                {
                    // XE HÀNG
                    var shoppingCart = new ShoppingCart();
                    if (Session["ShoppingCart"] != null)
                    {
                        shoppingCart = (ShoppingCart)Session["ShoppingCart"];
                    }

                    shoppingCart.AddToCart(product, Quantity);

                    Session["ShoppingCart"] = shoppingCart;

                    return RedirectToAction("CheckOut", "ShoppingCart");
                }

                return HttpNotFound();
            }
        }

        public void UpdateCart(int ProductId, int Quantity)
        {

            using (var db = new ShopOnlineDb())
            {

                // TÌM PRODUCT THEO ProductId
                var product = db.Products.Find(ProductId);
                if (product != null)
                {
                    // XE HÀNG
                    var shoppingCart = new ShoppingCart();
                    if (Session["ShoppingCart"] != null)
                    {
                        shoppingCart = (ShoppingCart)Session["ShoppingCart"];
                    }

                    shoppingCart.UpdateCart(product, Quantity);

                    Session["ShoppingCart"] = shoppingCart;
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateShoppingCart(FormCollection collection)
        {
            for (var i = 1; i <= collection.AllKeys.Length / 2; i++)
            {
                var productId = Convert.ToInt32(collection["ProductId_" + i]);
                var quantity = Convert.ToInt32(collection["Quantity_" + i]);

                UpdateCart(productId, quantity);
            }
            return View("CheckOut");
        }

        [HttpPost]
        public ActionResult RemoveCart(int ProductId)
        {
            using (var db = new ShopOnlineDb())
            {
                // TÌM PRODUCT THEO ProductId
                var product = db.Products.Find(ProductId);
                if (product != null)
                {
                    // XE HÀNG
                    var shoppingCart = new ShoppingCart();
                    if (Session["ShoppingCart"] != null)
                    {
                        shoppingCart = (ShoppingCart)Session["ShoppingCart"];
                    }

                    shoppingCart.RemoveCart(product.Id);

                    Session["ShoppingCart"] = shoppingCart;
                }
            }

            return View("CheckOut");
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut([Bind(Include = "Code,Status,CreatedDate,ContactName,ContactAddress,ContactPhone,ContactEmail")] Order order)
        {
            using (var db = new ShopOnlineDb())
            {
                if (ModelState.IsValid)
                {
                    var shoppingCart = (ShoppingCart)Session["ShoppingCart"];
                    foreach (var item in shoppingCart.ShoppingCartItems)
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.ProductId = item.Item.Id;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Discount = item.Item.Discount;
                        if (item.Item.Discount > 0)
                        {
                            orderDetail.Price = item.Item.Price * (100 - item.Item.Discount) / 100;  
                        }
                        else
                        {
                            orderDetail.Price = item.Item.Price;
                        }
                        
                        order.OrderDetails.Add(orderDetail);
                    }

                    db.Orders.Add(order);
                    db.SaveChanges();
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Client/NewOrder.html"));

                    content = content.Replace("{{CustomerName}}", order.ContactName);
                    content = content.Replace("{{Address}}", order.ContactAddress);
                    content = content.Replace("{{Phone}}", order.ContactPhone);

                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                    new MailHelper().SendMail(toEmail,"Đơn đặt hàng mới từ XanhDiepLuc store", content);

                    Session["ShoppingCart"] = null;
                    return RedirectToAction("OrderSuccessNotification", "Home");
                }
                return View(order);
            }
        }

    }
}
