using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DataAccess.Dtos;

public class LoginRequestDto
{
    [Required(ErrorMessage = "UserName is required.")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; set; }
}
