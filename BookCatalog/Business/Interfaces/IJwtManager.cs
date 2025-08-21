using BookCatalog.DataAccess;

namespace BookCatalog.Business.Interfaces;

public interface IJwtManager
{
    string GenerateToken(User user);
}
