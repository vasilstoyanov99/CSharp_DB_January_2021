using System;
using System.Collections.Generic;
using System.Linq;

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
            string categories = Console.ReadLine();
            Console.WriteLine(GetBooksByCategory(db, categories));
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var listOfCategories = input
                .ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var sortedTitles = new List<string>();

            foreach (var category in listOfCategories)
            {
                var titles = context
                    .Books
                    .Where(x => x
                        .BookCategories
                        .Any(b => 
                            b.Category.Name.ToLower().Contains(category)))
                    .Select(x => x.Title)
                    .ToList();
                sortedTitles.AddRange(titles);
            }

            string result = String.Join(Environment.NewLine, 
                sortedTitles.OrderBy(x => x));
            return result.Trim();
        }
    }
}
