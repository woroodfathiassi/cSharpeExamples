using System.Globalization;

namespace BookCatalog.DataAccess
{
    public class CsvBookRepository : ICsvBookRepository
    {
        private string GetFilePath()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return config["FilePath"];
        }

        public List<Book> ReadBooksFromCsv(string filePath)
        {
            var books = new List<Book>();
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',', StringSplitOptions.TrimEntries);
                if (parts.Length != 6) continue;

                try
                {
                    books.Add(new Book
                    {
                        Id = int.Parse(parts[0]),
                        Title = parts[1],
                        Author = parts[2],
                        Genre = parts[3],
                        PublishedYear = int.Parse(parts[4]),
                        Price = decimal.Parse(parts[5], CultureInfo.InvariantCulture)
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line {i + 1}: {ex.Message}");
                }
            }
            return books;
        }

        private void WriteBooksToCsv(string filePath, List<Book> books)
        {
            var lines = new List<string>
            {
                "Id,Title,Author,Genre,PublishedYear,Price" 
            };

            lines.AddRange(books.Select(b =>
                $"{b.Id},{b.Title},{b.Author},{b.Genre},{b.PublishedYear},{b.Price.ToString(CultureInfo.InvariantCulture)}"));

            File.WriteAllLines(filePath, lines);
        }

        public List<Book> GetBooks()
        {
            string filePath = GetFilePath();
            return ReadBooksFromCsv(filePath);
        }

        public void AddBook(Book book)
        {
            var filePath = GetFilePath();
            var books = ReadBooksFromCsv(filePath);

            // Auto-increment ID if needed
            book.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;

            books.Add(book);
            WriteBooksToCsv(filePath, books);
        }

        public void UpdateBook(Book updatedBook)
        {
            var filePath = GetFilePath();
            var books = ReadBooksFromCsv(filePath);

            var index = books.FindIndex(b => b.Id == updatedBook.Id);
            if (index == -1) throw new Exception("Book not found");

            books[index] = updatedBook;
            WriteBooksToCsv(filePath, books);
        }

        public void DeleteBook(int id)
        {
            var filePath = GetFilePath();
            var books = ReadBooksFromCsv(filePath);

            var book = books.FirstOrDefault(b => b.Id == id) ?? throw new Exception("Book not found");

            books.Remove(book);
            WriteBooksToCsv(filePath, books);
        }
    }
}
