using Delegates;

// Delegates & Events!

void WelcomeWorood()
{
    Console.WriteLine("Welcome Worood!");
}
void WelcomeAhmad()
{
    Console.WriteLine("Welcome Ahmad!");
}
void Welcome(string name)
{
    Console.WriteLine($"Welcome {name}!");
}
void GoodBye(string name)
{
    Console.WriteLine($"GoodBye {name}!");
}

List<Book> books =
        [
            new () { Name = "C# in Depth", Price = 45.99 },
            new () { Name = "The Pragmatic Programmer", Price = 42.00 },
            new () { Name = "Design Patterns", Price = 50.00 },
            new () { Name = "Clean Code", Price = 39.50 },
            new () { Name = "Refactoring", Price = 44.95 },
            new () { Name = "Head First Design Patterns", Price = 38.75 },
            new () { Name = "You Don’t Know JS", Price = 29.99 },
            new () { Name = "Pro ASP.NET Core", Price = 59.99 },
            new () { Name = "Introduction to Algorithms", Price = 70.00 },
            new () { Name = "Effective C#", Price = 36.00 }
        ];

int CountBooksPriceLess30(List<Book> books)
{
    int count = 0;
    foreach (Book book in books)
    {
        if (book.Price < 30)
            count++;
    }
    Console.WriteLine(count);
    return count;
}

int CountBooksPriceLess50(List<Book> books)
{
    int count = 0;
    foreach (Book book in books)
    {
        if (book.Price < 50)
            count++;
    }
    Console.WriteLine(count);
    return count;
}

int CountBooksPriceLess(List<Book> books, int maxValue)
{
    int count = 0;
    foreach (Book book in books)
    {
        if (book.Price < maxValue)
            count++;
    }
    Console.WriteLine(count);
    return count;
}
int CountBooksPrices(List<Book> books, int minValue)
{
    int count = 0;
    foreach (Book book in books)
    {
        if (book.Price > minValue)
            count++;
    }
    Console.WriteLine(count);
    return count;
}

int CountBooksStartWithD(List<Book> books)
{
    int count = 0;
    foreach (Book book in books)
    {
        if (book.Name.StartsWith('D'))
            count++;
    }
    Console.WriteLine(count);
    return count;
}

bool IsLess30(Book book)
{
    return book.Price < 30;
}
//CountBooksPriceLess30(books);
//CountBooksPriceLess50(books);
//CountBooksPriceLess(books, 60);
//CountBooksStartWithD(books);

#region Delegates
int CountBooks(List<Book> books, MyDelegate condition)
{
    int count = 0;
    foreach (Book book in books)
    {
        if (condition(book))
            count++;
    }
    Console.WriteLine(count);
    return count;
}

CountBooks(books, IsLess30);
CountBooks(books, b => b.Name.StartsWith('D'));

delegate bool MyDelegate(Book book);

//Action<int, int> printSum = (a, b) => Console.WriteLine(a + b);
//Func<int, int, int> sum = (a, b) => a + b;
//Predicate<int> isEven = (a) => a % 2 == 0;

#endregion

#region Events

//var stock = new Stock { Name = "Amazon", Price = 20 };
//Console.WriteLine($"{stock.Name}: ${stock.Price}");
//stock.OnPriceChanged += Stock_OnPriceChanged;

//stock.ChangeStockPrice(30);
//stock.ChangeStockPrice(10);


//void Stock_OnPriceChanged(Stock stock, double oldPrice)
//{
//if (stock.Price > oldPrice)
//Console.ForegroundColor = ConsoleColor.Green;
//else if (stock.Price < oldPrice)
//Console.ForegroundColor = ConsoleColor.Red;

//Console.WriteLine($"{stock.Name}: ${stock.Price}");
//Console.ForegroundColor = ConsoleColor.White;
//}

//public delegate void StockPriceChangeHandler(Stock stock, double oldPrice);

//public class Stock
//{
//    public string Name { get; set; }
//    public double Price { get; set; }

//    public event StockPriceChangeHandler OnPriceChanged;

//    public void ChangeStockPrice(double newPrice)
//    {
//        double oldPrice = this.Price;
//        this.Price = newPrice;
//        if (OnPriceChanged != null)
//        {
//            OnPriceChanged(this, oldPrice);
//        }
//    }
//}

#endregion