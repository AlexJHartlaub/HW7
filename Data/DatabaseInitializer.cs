using Dapper;
using hw7_database.Data;

namespace hw7_database.Data;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync(
            @"DROP TABLE IF EXISTS Book;");

        await connection.ExecuteAsync(
            @"CREATE TABLE IF NOT EXISTS Book(
                Id INTEGER PRIMARY KEY,
                Title TEXT NOT NULL,
                Author TEXT NOT NULL,
                PageCount TEXT NOT NULL,
                Publisher TEXT NOT NULL);"
            );

        await connection.ExecuteAsync(
            @"INSERT INTO Book(Title, Author, PageCount, Publisher)
            VALUES ('The Lord of the Rings', 'J.R.R. Tolkien', '1254', 'Allen & Unwin');
                
            INSERT INTO Book(Title, Author, PageCount, Publisher)
            VALUES ('The Hobbit', 'J.R.R. Tolkien', '455', 'Allen & Unwin');
            
            INSERT INTO Book(Title, Author, PageCount, Publisher)
            VALUES ('The Catcher in the Rye', 'J.D. Salinger', '874', 'Allen & Unwin');
            
            INSERT INTO Book(Title, Author, PageCount, Publisher)
            VALUES ('The Grapes of Wrath', 'John Steinbeck', '356', 'Allen & Unwin');
            
            INSERT INTO Book(Title, Author, PageCount, Publisher)
            VALUES ('The Great Gatsby', 'F. Scott Fitzgerald', '466', 'Allen & Unwin');
            ");
    }
}