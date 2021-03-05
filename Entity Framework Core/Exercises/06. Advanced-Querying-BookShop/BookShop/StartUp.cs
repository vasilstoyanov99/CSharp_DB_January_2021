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
            string date = Console.ReadLine();
            Console.WriteLine(GetBooksReleasedBefore(db, date));
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "dd-MM-yyyy",
                CultureInfo.InvariantCulture);

            var sortedBooks = context
                .Books
                .ToList()
                .Where(x => x.ReleaseDate.HasValue &&
                            x
                                .ReleaseDate
                                .Value
                                .Date < dateTime)
                .OrderByDescending(x => x.ReleaseDate)
                .Select(x => new
                {
                    x.Title,
                    x.EditionType,
                    x.Price
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var book in sortedBooks)
            {
                result.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return result.ToString().Trim();
        }
    }
}
