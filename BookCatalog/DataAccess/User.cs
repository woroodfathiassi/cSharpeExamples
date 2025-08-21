namespace BookCatalog.DataAccess;

public enum Roles{
    Admin,
    Customer
}
public class User
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
}
