using BookShop.DataProcessor.ExportDto;
using XmlFacade;

namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var sortedAuthors = context
                .Authors
                .ToList()
                .Select(a => new
                {
                    AuthorName = a.FirstName + " " + a.LastName,
                    Books = a.AuthorsBooks
                        .OrderByDescending(b => b.Book.Price)
                        .Select(b => new
                        {
                            BookName = b.Book.Name,
                            BookPrice = b.Book.Price.ToString("F2")
                        })
                        .ToList()
                })
                .OrderByDescending(a => a.Books.Count)
                .ThenBy(a => a.AuthorName)
                .ToList();

            var jsonResult = JsonConvert.SerializeObject(sortedAuthors, Formatting.Indented);
            return jsonResult;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var sortedBooks = context
                .Books
                .ToList()
                .Where(b => b.PublishedOn < date && b.Genre.ToString() == "Science")
                .OrderByDescending(b => b.Pages)
                .ThenBy(b => b.PublishedOn)
                .Select(b => new OldestBooksExportModel()
                {
                    Name = b.Name,
                    Date = b.PublishedOn.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    Pages = b.Pages.ToString()
                })
                .Take(10)
                .ToList();

            var jsonResult = XmlConverter.Serialize(sortedBooks, "Books");
            return jsonResult;
        }
    }
}