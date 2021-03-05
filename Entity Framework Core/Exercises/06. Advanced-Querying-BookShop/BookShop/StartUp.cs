using System;
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
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
            string ageRestriction = Console.ReadLine();
            Console.WriteLine(GetBooksByAgeRestriction(db, ageRestriction));
        }

        public static string GetBooksByAgeRestriction
            (BookShopContext context, string command)
        {
            AgeRestriction ageRestrictionEnum = Enum
                .Parse<AgeRestriction>(command, true);

            var sortedTitles = context
                .Books
                .Where(x => x.AgeRestriction == ageRestrictionEnum)
                .Select(x => new
                {
                    x.Title
                })
                .OrderBy(x => x.Title)
                .ToList();

            var result = new StringBuilder();

            foreach (var book in sortedTitles)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().Trim();
        }
    }
}
