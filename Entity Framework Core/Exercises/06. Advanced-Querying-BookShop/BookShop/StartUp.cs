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
            Console.WriteLine(GetGoldenBooks(db));
        }


        public static string GetGoldenBooks(BookShopContext context)
        {
            var sortedBooks = context
                .Books
                .Where(x => x.EditionType == EditionType.Gold
                            && x.Copies < 5000)
                .Select(x => new
                {
                    x.Title,
                    x.BookId
                })
                .OrderBy(x => x.BookId)
                .ToList();

            var result = new StringBuilder();

            foreach (var book in sortedBooks)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().Trim();
        }
    }
}
