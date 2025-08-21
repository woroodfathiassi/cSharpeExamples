using BookCatalog.DataAccess;

namespace BookCatalog.Data;

public class Users
{
    public static List<User> users = [
            new() {UserName = "admin", Password = "admin", Role=Roles.Admin},
            new() {UserName = "c1", Password = "c1", Role=Roles.Customer},
        ];
}
