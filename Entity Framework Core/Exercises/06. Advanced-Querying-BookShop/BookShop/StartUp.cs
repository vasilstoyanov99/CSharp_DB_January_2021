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
            Console.WriteLine(CountCopiesByAuthor(db));
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var copiesByAuthors = context
                .Authors
                .Select(x => new
                {
                    AuthorFullName = x.FirstName + " " + x.LastName,
                    CopiesCount = x.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(x => x.CopiesCount)
                .ToList();

            var result = new StringBuilder();

            foreach (var author in copiesByAuthors)
            {
                result.AppendLine($"{author.AuthorFullName} - {author.CopiesCount}");
            }

            return result.ToString().Trim();
        }
    }
}
