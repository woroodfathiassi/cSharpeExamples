using BookCatalog.DataAccess.Models;

namespace BookCatalog.DataAccess.Repositories;

public interface ICsvBookRepository
{
    List<Book> GetBooks();
    void AddBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(int id);
}
