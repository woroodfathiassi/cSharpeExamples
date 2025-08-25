using BookCatalog.Business.Interfaces;
using BookCatalog.DataAccess.Dtos;
using BookCatalog.DataAccess.Repositories;
using BookCatalog.Mappers;

namespace BookCatalog.Business.Managers;

public class BookManager(ICsvBookRepository csvBookRepository, ILogger<BookManager> logger) : IBookManager
{
    private readonly ICsvBookRepository _repository = csvBookRepository;
    private readonly ILogger<BookManager> _logger = logger;
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
        return book == null ? throw new KeyNotFoundException("Book not found") : BookMapper.ToDto(book);
    }

    public void AddBook(BookDto dto)
    {
        try
        {
            var book = BookMapper.ToEntity(dto);
            _repository.AddBook(book);
            _logger.LogInformation("Book added: {Title}", book.Title);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding book: {Title}", dto.Title);
            throw; // Rethrow to let Controller handle response
        }
    }

    public void UpdateBook(BookDto dto)
    {
        try
        {
            var book = BookMapper.ToEntity(dto);
            _repository.UpdateBook(book);
            _logger.LogInformation("Book updated: {Title}, Id: {Id}", book.Title, book.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating book: {Title}", dto.Title);
            throw;
        }
    }

    public void DeleteBook(int id)
    {
        try
        {
            _repository.DeleteBook(id);
            _logger.LogInformation("Book deleted: Id {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting book: Id {Id}", id);
            throw;
        }
    }

    public List<BookDto> SearchBooks(string query)
    {
        var books = _repository.GetBooks();

        var results = books
            .Where(b =>
                (!string.IsNullOrEmpty(b.Title) &&
                 b.Title.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(b.Description) &&
                 b.Description.Contains(query, StringComparison.OrdinalIgnoreCase))
            )
            .Select(BookMapper.ToDto)
            .ToList();

        return results;
    }

}
