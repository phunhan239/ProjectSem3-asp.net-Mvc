﻿using System;
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
    public class ManageCategoriesController : Controller
    {
        private ShopOnlineDb db = new ShopOnlineDb();

        // GET: ManageCategories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: ManageCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: ManageCategories/Create
        // GET: ManageCategories/Create
        public ActionResult Create()
        {
            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường ParentId
            var categories = db.Categories.Where(x => x.ParentId == 0).ToList();

            var root = new Category();
            root.Id = 0;
            root.Name = "";

            categories.Add(root);

            ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường ParentId
            return View();
        }

        // POST: ManageCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ParentId,Name,SortOrder,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường ParentId
            var categories = db.Categories.Where(x => x.ParentId == 0).ToList();

            var root = new Category();
            root.Id = 0;
            root.Name = "";

            categories.Add(root);

            ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường ParentId
            return View(category);
        }

        // GET: ManageCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            return View(category);
        }

        // POST: ManageCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParentId,Name,SortOrder,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: ManageCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: ManageCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            var deleteItem = db.Products.Where(x => x.CategoryId == id).ToList().Count;
            if (deleteItem > 0)
            {
                return RedirectToAction("Index");
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
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
