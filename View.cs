using hw7_database;

namespace hw7_database;

public static class View
{
    public static void ListBooks(IEnumerable<Book> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id} {book.Title} {book.Author} {book.PageCount} {book.Publisher}");
        }
    }
}