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
        return Ok(books);
    }

    // GET /api/books/{id}
    [HttpGet("{id}")]
    public ActionResult<BookDto> GetBookById(int id)
    {
        var book = _bookManager.GetBookById(id);
        if (book == null) return NotFound(new { message = "Book not found" });
        return Ok(book);
    }
  
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult AddBook([FromBody] BookDto dto)
    {
        try
        {
            _bookManager.AddBook(dto);
            return Ok(new { message = "Book added successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error adding book", error = ex.Message });
        }
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateBook([FromBody] BookDto dto)
    {
        try
        {
            _bookManager.UpdateBook(dto);
            return Ok(new { message = "Book updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating book", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            _bookManager.DeleteBook(id);
            return Ok(new { message = "Book deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting book", error = ex.Message });
        }
    }
}
