using hw7_database;
using hw7_database.Data;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
var connectionString = configuration["ConnectionString"];

var dbInitializer = new DatabaseInitializer(
    new SqliteConnectionFactory(connectionString));
await dbInitializer.InitializeAsync();


var bookController = new BookController(
    new SqliteConnectionFactory(connectionString));

var run = true;
while (run)
{
    Console.WriteLine("1. List all books");
    Console.WriteLine("2. Add a book");
    Console.WriteLine("3. Delete a book");
    Console.WriteLine("4. Exit");
    Console.WriteLine();
    Console.Write("Enter your choice: ");
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            var books = await bookController.ListAllBooksAsync();
            View.ListBooks(books);
            Console.WriteLine();
            break;
        case "2":
            var newBook = CreateBook();
            await bookController.AddBookAsync(newBook);
            break;
        case "3":
            View.ListBooks(await bookController.ListAllBooksAsync());
            Console.Write("Enter the book id to delete: ");
            var id = int.Parse(Console.ReadLine()!);
            if (!await bookController.DeleteBookByIdAsync(id))
            {
                Console.WriteLine("Book with id {0} not found", id);
                break;
            }
            Console.WriteLine("Book with id {0} deleted", id);
            break;
        case "4":
            Console.WriteLine("Exiting...");
            run = false;
            break;
    }
}

static Book CreateBook()
{
    Console.Write("Enter the title: ");
    var title = Console.ReadLine()!;
    Console.Write("Enter the author: ");
    var author = Console.ReadLine()!;
    Console.Write("Enter the page count: ");
    var pageCount = int.Parse(Console.ReadLine()!);
    Console.Write("Enter the publisher: ");
    var publisher = Console.ReadLine()!;
    return new Book
    {
        Title = title,
        Author = author,
        PageCount = pageCount,
        Publisher = publisher
    };
}




