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
            Console.WriteLine(GetAuthorNamesEndingIn(db, arg));
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var sortedAuthors = context
                .Authors
                .Where(x => x.FirstName.EndsWith(input))
                .Select(x => new
                {
                    FullName = x.FirstName + " " + x.LastName
                })
                .OrderBy(x => x.FullName)
                .ToList();

            var result = new StringBuilder();

            foreach (var author in sortedAuthors)
            {
                result.AppendLine(author.FullName);
            }

            return result.ToString().Trim();
        }
    }
}
