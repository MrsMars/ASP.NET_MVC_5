﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerStore.Models;

namespace FlowerStore.Controllers
{
    public class HomeController : Controller
    {
        // conect to data in the model
        private FlowersDBEntities db = new FlowersDBEntities();

        // GET: Home
        public ActionResult Index()
        {
            // linq
            // result to arraylist
            var flowers = (from flower in db.Flowers
                           select flower).ToList();
            return View(flowers);
        }
    }
}