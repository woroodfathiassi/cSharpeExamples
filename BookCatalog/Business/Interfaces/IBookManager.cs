using BookCatalog.DataAccess.Dtos;

namespace BookCatalog.Business.Interfaces;

public interface IBookManager
{
    List<BookDto> GetBooks(string? author = null, string? genre = null, int? year = null,
                           string? sortBy = null, bool ascending = true,
                           int page = 1, int pageSize = 10);

    BookDto? GetBookById(int id);
    void AddBook(BookDto dto);
    void UpdateBook(BookDto dto);
    void DeleteBook(int id);
    List<BookDto> SearchBooks(string query);
}
