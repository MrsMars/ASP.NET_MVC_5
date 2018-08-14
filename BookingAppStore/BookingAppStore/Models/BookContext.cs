using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookingAppStore.Models
{
    public class BookContext : DbContext
    {
        // for communication with db
        public DbSet<Book> Books { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }

    // initialize our db first parameters
    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        // DropCreateDatabaseAlways - recreates this db all time we start our app

        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book { Name = "Sherlock Holmes", Author = "Arhur Conan Doyle", Price = 220 });
            db.Books.Add(new Book { Name = "A Tale of Two Cities", Author = "Charles Dickens", Price = 180 });
            db.Books.Add(new Book { Name = "Man's Search for Meaning", Author = "Viktor E Frankl", Price = 150 });

            base.Seed(db);
        }
    }
}