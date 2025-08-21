using BookCatalog.Business.Interfaces;
using BookCatalog.DataAccess.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController(IAuthManager authManager) : ControllerBase
{
    private readonly IAuthManager _authManager = authManager;

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequestDto userRequest)
    {
        try
        {
            if (userRequest == null)
            {
                return BadRequest(new { message = "Login data must not be null." });
            }
            var token = _authManager.Login(userRequest);
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message }); ;
        }
        catch (Exception ex)
        {
            return StatusCode(500, new 
                        { 
                            message = "Server error", 
                            error = ex.Message 
                        });
        }
    }
}
