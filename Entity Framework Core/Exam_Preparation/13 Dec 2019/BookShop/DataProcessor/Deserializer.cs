using BookShop.Data.Models;
using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ImportDto;
using XmlFacade;

namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var bookDTOs = XmlConverter.Deserializer<BookImportModel>(xmlString, "Books");

            var result = new StringBuilder();
            var books = new List<Book>();

            foreach (var bookDTO in bookDTOs)
            {
                if (!IsValid(bookDTO))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime date;
                bool isDateValid = DateTime.TryParseExact(bookDTO.PublishedOn, "MM/dd/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (!isDateValid)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var newBook = new Book()
                {
                    Name = bookDTO.Name,
                    Genre = Enum.Parse<Genre>(bookDTO.Genre),
                    Price = bookDTO.Price,
                    Pages = bookDTO.Pages,
                    PublishedOn = date
                };

                books.Add(newBook);
                result.AppendLine(String.Format(SuccessfullyImportedBook,
                    newBook.Name, newBook.Price));
            }
            context.Books.AddRange(books);
            context.SaveChanges();
            return result.ToString().Trim();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var authorsDTOs = JsonConvert.DeserializeObject<List<AuthorImportModel>>(jsonString);
            var result = new StringBuilder();

            foreach (var authorDTO in authorsDTOs)
            {
                if (!IsValid(authorDTO))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }
                
                var authorBooks = new List<AuthorBook>();

                var newAuthor = new Author()
                {
                    FirstName = authorDTO.FirstName,
                    LastName = authorDTO.LastName,
                    Email = authorDTO.Email,
                    Phone = authorDTO.Phone,
                };

                foreach (var currentBook in authorDTO.Books)
                {
                    var book = context.Books.FirstOrDefault(x => x.Id == currentBook.Id);

                    if (book == null)
                    {
                        continue;
                    }

                    var newAuthorBook = new AuthorBook()
                    {
                        Book = book,
                        Author = newAuthor
                    };

                    authorBooks.Add(newAuthorBook);
                }

                if (!authorBooks.Any())
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                newAuthor.AuthorsBooks = authorBooks;
                context.Authors.Add(newAuthor);
                context.SaveChanges();
                result.AppendLine(String.Format(SuccessfullyImportedAuthor,
                    newAuthor.FirstName + " " + newAuthor.LastName,
                    newAuthor.AuthorsBooks.Count));
            }

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}