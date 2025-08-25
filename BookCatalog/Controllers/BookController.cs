using BookCatalog.Business.Interfaces;
using BookCatalog.DataAccess.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
//[Authorize]
public class BookController(IBookManager bookManager) : ControllerBase
{
    private readonly IBookManager _bookManager = bookManager;

    // GET /api/books?author=xxx&genre=yyy&year=2020&sortBy=price&page=1&pageSize=5
    [HttpGet]
    public ActionResult<List<BookDto>> GetBooks(
        [FromQuery] string? author,
        [FromQuery] string? genre,
        [FromQuery] int? year,
        [FromQuery] string? sortBy,
        [FromQuery] bool ascending = true,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var books = _bookManager.GetBooks(author, genre, year, sortBy, ascending, page, pageSize);
        Console.WriteLine(books.Count());
        return Ok(books);
    }

    // GET /api/books/{id}
    [HttpGet("{id}")]
    public ActionResult<BookDto> GetBookById(int id)
    {
        var book = _bookManager.GetBookById(id);
        //if (book == null) return NotFound(new { message = "Book not found" });
        return Ok(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult AddBook([FromBody] BookDto dto)
    {
        _bookManager.AddBook(dto);
        return CreatedAtAction(nameof(GetBookById), new { id = dto.Id }, dto);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateBook([FromBody] BookDto dto)
    {
        _bookManager.UpdateBook(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteBook(int id)
    {
        if (id < 0) return NotFound(new { message = "Id should be greater than 0!" });
        _bookManager.DeleteBook(id);
        return NoContent();
    }

    [HttpGet()]
    public ActionResult<List<BookDto>> SearchBooks([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest(new { error = "Query parameter is required" });

        var results = _bookManager.SearchBooks(query);
        return Ok(results);
    }

}
