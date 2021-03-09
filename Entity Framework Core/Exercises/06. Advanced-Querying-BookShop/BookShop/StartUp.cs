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
            Console.WriteLine(RemoveBooks(db));
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var booksToRemove = context
                .Books
                .Where(x => x.Copies < 4200)
                .ToList();

            int countOfRemovedBooks = booksToRemove.Count;

            context.RemoveRange(booksToRemove);

            context.SaveChanges();

            return countOfRemovedBooks;
        }
    }
}
