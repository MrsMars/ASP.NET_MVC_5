using BookingAppStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {

        // for communication with db
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            // get our book list
            var books = db.Books;

            // transfer in views
            //ViewBag.Books = books;

            return View(books);
        }
        public ActionResult BookIndex()
        {
            var books = db.Books;
            return View(books);
        }

        // async method
        public async Task<ActionResult> BookList()
        {
            IEnumerable<Book> books = await db.Books.ToListAsync();
            ViewBag.Books = books;
            return View("Index");
        }


        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            //add info about purchase in db
            db.Purchases.Add(purchase);
            db.SaveChanges();

            return "Thank you, " + purchase.Person + ", for your purchase";
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}