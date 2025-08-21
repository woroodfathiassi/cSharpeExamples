using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DataAccess.Dtos;

public class BookDto
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }

    [Required, StringLength(200)]
    public required string Title { get; set; }

    [Required, StringLength(200)]
    public required string Author { get; set; }

    [Required, StringLength(100)]
    public required string Genre { get; set; }

    [Range(1, 9999)]
    public int PublishedYear { get; set; }

    [Range(0, 1_000_000)]
    public decimal Price { get; set; }
}
