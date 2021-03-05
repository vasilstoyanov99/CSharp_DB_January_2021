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
            Console.WriteLine(GetMostRecentBooks(db));
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var recentBooks = context
                .Categories
                .Select(x => new
                {
                    x.Name,
                    Books = x
                        .CategoryBooks
                        .Where(a => a.Book.ReleaseDate.HasValue)
                        .Select(a => new
                        {
                            a.Book.Title,
                            a.Book.ReleaseDate
                        })
                        .OrderByDescending(a => a.ReleaseDate)
                        .Take(3)
                        .ToList()
                })
                .OrderBy(x => x.Name)
                .ToList();

            var result = new StringBuilder();

            foreach (var category in recentBooks)
            {
                result.AppendLine($"--{category.Name}");

                foreach (var book in category.Books)
                {
                    result.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return result.ToString().Trim();
        }
    }
}
