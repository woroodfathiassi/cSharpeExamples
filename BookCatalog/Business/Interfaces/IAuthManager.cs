using BookCatalog.DataAccess.Dtos;

namespace BookCatalog.Business.Interfaces;

public interface IAuthManager
{
    string Login(LoginRequestDto userRequest);
}
