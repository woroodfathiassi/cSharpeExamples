using BookCatalog.DataAccess.Models;

namespace BookCatalog.Business.Interfaces;

public interface IJwtManager
{
    string GenerateToken(User user);
}
