using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BookShop.Models.Enums;

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
            Console.WriteLine(GetBooksByPrice(db));
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var sortedBooks = context
                .Books
                .Where(x => x.Price > 40)
                .OrderByDescending(x => x.Price)
                .Select(x => new
                {
                    x.Title,
                    x.Price
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var book in sortedBooks)
            {
                result.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return result.ToString().Trim();
        }
    }
}
