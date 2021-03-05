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
            Console.WriteLine(GetTotalProfitByCategory(db));
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var profitsByCategory = context
                .Categories
                .Select(x => new
                {
                    TotalProfit = x
                        .CategoryBooks
                        .Sum(p
                            => p.Book.Copies * p.Book.Price),
                    Category = x.Name
                })
                .OrderByDescending(x => x.TotalProfit)
                .ThenBy(x => x.Category)
                .ToList();

            var result = new StringBuilder();

            foreach (var item in profitsByCategory)
            {
                result.AppendLine($"{item.Category} ${item.TotalProfit:f2}");
            }

            return result.ToString().Trim();
        }
    }
}
