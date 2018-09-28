﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IGameRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IGameRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int gameId, string returnUrl)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if(game != null) { cart.AddItem(game, 1); }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int gameId, string returnUrl)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if(game != null) { cart.RemoveLine(game); }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart) { return PartialView(cart); }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0) { ModelState.AddModelError("", "Sorry, your cart is empty!"); }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();

                ViewBag.Name = shippingDetails.Name;

                return View("Completed");
            }
            else { return View(shippingDetails); }
        }
    }
}