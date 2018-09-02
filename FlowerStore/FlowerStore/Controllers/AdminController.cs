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
            Flower flower = new Flower();

            return View(flower);
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(Flower flower)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Flowers.Add(flower);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex);
            }
            return View(flower);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var flowerEdit = (from flower in db.Flowers
                              where flower.FlowerId == id
                              select flower).First();

            return View(flowerEdit);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var flowerEdit = (from flower in db.Flowers
                              where flower.FlowerId == id
                              select flower).First();

            try
            {
                // TODO: Add update logic here
                UpdateModel(flowerEdit);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(flowerEdit);
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            var flowerDelete = (from flower in db.Flowers
                              where flower.FlowerId == id
                              select flower).First();
            return View(flowerDelete);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var flowerDelete = (from flower in db.Flowers
                                where flower.FlowerId == id
                                select flower).First();
            try
            {
                // TODO: Add delete logic here
                db.Flowers.Remove(flowerDelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(flowerDelete);
            }
        }
    }
}
