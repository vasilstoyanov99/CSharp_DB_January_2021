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
            Console.WriteLine(GetBookTitlesContaining(db, arg.ToLower()));
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var sortedBooks = context
                .Books
                .Where(x => x.Title.ToLower().Contains(input))
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToList();

            string result = String.Join(Environment.NewLine, sortedBooks);
            return result.Trim();
        }
    }
}
