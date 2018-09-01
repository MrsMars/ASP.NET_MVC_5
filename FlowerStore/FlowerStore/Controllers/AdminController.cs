using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerStore.Models;

namespace FlowerStore.Controllers
{
    public class AdminController : Controller
    {
        private FlowersDBEntities db = new FlowersDBEntities();

        // GET: Admin
        public ActionResult Index()
        {
            var flowers = (from flower in db.Flowers
                          select flower).ToList();

            return View(flowers);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            var flowerDetails = (from flower in db.Flowers
                                 where flower.FlowerId == id
                                 select flower).First();

            return View(flowerDetails);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
