namespace Delegates;

public class Book
{
    public string Name { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return $"Book Details:\n" +
               $"Name : {Name}, Price : {Price}";
    }
}
