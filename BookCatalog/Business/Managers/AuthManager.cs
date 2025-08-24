using BookCatalog.Business.Interfaces;
using BookCatalog.Data;
using BookCatalog.DataAccess.Dtos;
using BookCatalog.DataAccess.Models;

namespace BookCatalog.Business.Managers;

public class AuthManager(IJwtManager jwtService) : IAuthManager
{
    private readonly List<User> _usersRepo = Users.users;
    private readonly IJwtManager _jwtService = jwtService;

    public string Login(LoginRequestDto userRequest)
    {
        var user = _usersRepo.FirstOrDefault(
            u => 
                u.UserName == userRequest.UserName
                && u.Password == userRequest.Password
            ) ?? throw new UnauthorizedAccessException("Invalid credentials.");

        return _jwtService.GenerateToken(user);
    }
}
