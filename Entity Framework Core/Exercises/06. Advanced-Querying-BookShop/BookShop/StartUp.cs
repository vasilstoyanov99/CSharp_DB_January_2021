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
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine(GetBooksNotReleasedIn(db, year));
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var sortedBooks = context
                .Books
                .Where(x => x.ReleaseDate.HasValue
                            && x.ReleaseDate.Value.Year != year)
                .OrderBy(x => x.BookId)
                .Select(x => new
                {
                    x.Title
                })
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
