using Dapper;
using hw7_database;
using hw7_database.Data;

namespace hw7_database;

public class BookController
{
    private readonly IDbConnectionFactory _connectionFactory;

    public BookController(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Book>> ListAllBooksAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<Book>("SELECT * FROM Book");
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync(
            @"INSERT INTO Book
            (Title, Author, PageCount, Publisher) 
            VALUES (@Title, @Author, @PageCount, @Publisher)", book);

        return result > 0;
    }

    public async Task<bool> DeleteBookByIdAsync(int id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"DELETE FROM Book
            WHERE Id = @Id", new { Id = id });

        return result > 0;
    }
}
