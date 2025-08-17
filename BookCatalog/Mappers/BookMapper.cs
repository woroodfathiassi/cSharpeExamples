using BookCatalog.DataAccess;

namespace BookCatalog.Mappers;

public class BookMapper
{
    public static BookDto ToDto(Book book)
    {
        return book == null
            ? throw new ArgumentNullException(nameof(book))
            : new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            PublishedYear = book.PublishedYear,
            Price = book.Price
        };
    }

    public static Book ToEntity(BookDto dto)
    {
        return dto == null
            ? throw new ArgumentNullException(nameof(dto))
            : new Book
        {
            Id = dto.Id,
            Title = dto.Title,
            Author = dto.Author,
            Genre = dto.Genre,
            PublishedYear = dto.PublishedYear,
            Price = dto.Price
        };
    }
}
