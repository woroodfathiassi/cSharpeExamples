using BookCatalog.Business.Interfaces;
using BookCatalog.DataAccess;
using BookCatalog.DataAccess.Dtos;
using BookCatalog.Mappers;

namespace BookCatalog.Business.Managers;

public class BookManager(ICsvBookRepository csvBookRepository) : IBookManager
{
    private readonly ICsvBookRepository _repository = csvBookRepository;
    public List<BookDto> GetBooks(string? author = null, string? genre = null, int? year = null,
                              string? sortBy = null, bool ascending = true,
                              int page = 1, int pageSize = 10)
    {
        var query = _repository.GetBooks().AsQueryable();

        // filtering
        if (!string.IsNullOrWhiteSpace(author))
            query = query.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(genre))
            query = query.Where(b => b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));

        if (year.HasValue)
            query = query.Where(b => b.PublishedYear == year.Value);

        // sorting
        query = sortBy?.ToLower() switch
        {
            "price" => ascending ? query.OrderBy(b => b.Price) : query.OrderByDescending(b => b.Price),
            "year" => ascending ? query.OrderBy(b => b.PublishedYear) : query.OrderByDescending(b => b.PublishedYear),
            "title" => ascending ? query.OrderBy(b => b.Title) : query.OrderByDescending(b => b.Title),
            _ => query
        };

        // pagination
        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        return query.Select(BookMapper.ToDto).ToList();
    }

    public BookDto? GetBookById(int id)
    {
        var book = _repository.GetBooks().FirstOrDefault(b => b.Id == id);
        return book == null ? null : BookMapper.ToDto(book);
    }

    public void AddBook(BookDto dto)
    {
        var book = BookMapper.ToEntity(dto);
        _repository.AddBook(book);
    }

    public void UpdateBook(BookDto dto)
    {
        var book = BookMapper.ToEntity(dto);
        _repository.UpdateBook(book);
    }

    public void DeleteBook(int id)
    {
        _repository.DeleteBook(id);
    }
}
