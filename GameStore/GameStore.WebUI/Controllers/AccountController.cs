﻿using GameStore.UsersDomain.Entities;
using GameStore.UsersDomain.Concrete;
using GameStore.UsersDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GameStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;

                using (EFDbContext db = new EFDbContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
                }

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("List", "Game");
                }
                else { ModelState.AddModelError("", "User with this login isn't exist..."); }
            }
            return View(model);
        }



        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;

                using(EFDbContext db = new EFDbContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Name);
                }

                if (user == null)
                {
                    using (EFDbContext db = new EFDbContext())
                    {
                        db.Users.Add(new User { Email = model.Name, Password = model.Password });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    }

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("List", "Game");
                    }
                }
                else { ModelState.AddModelError("", "User with this login already exists..."); }
                }
            return View(model);
        }




    }
}