using System;
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
            int arg = int.Parse(Console.ReadLine());
            Console.WriteLine(CountBooks(db, arg));
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var countOfBooks = context
                .Books
                .Where(x => x.Title.Length > lengthCheck)
                .ToList()
                .Count;

            return countOfBooks;
        }
    }
}
