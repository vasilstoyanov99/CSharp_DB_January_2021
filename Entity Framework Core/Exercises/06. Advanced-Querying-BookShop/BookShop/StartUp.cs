using System;
using System.Globalization;
using System.Linq;
using System.Text;

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
            string arg = Console.ReadLine();
            Console.WriteLine(GetBooksByAuthor(db, arg));
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var sortedBooks = context
                .Books
                .Where(x => x.Author
                    .LastName
                    .ToLower()
                    .StartsWith(input.ToLower()))
                .OrderBy(x => x.BookId)
                .Select(x => new
                {
                    x.Title,
                    AuthorFullName = x.Author.FirstName + " " + x.Author.LastName
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var book in sortedBooks)
            {
                result.AppendLine($"{book.Title} ({book.AuthorFullName})");
            }

            return result.ToString().Trim();
        }
    }
}
