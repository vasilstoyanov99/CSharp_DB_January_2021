using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using BookShopContext db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
            IncreasePrices(db);
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var sortedBooks = context
                .Books
                .Where(x => x.ReleaseDate.Value.Year < 2010 && x.ReleaseDate.HasValue)
                .ToList();

            foreach (var book in sortedBooks)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }
    }
}
