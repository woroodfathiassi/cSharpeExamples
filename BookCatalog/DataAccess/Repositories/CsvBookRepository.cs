using BookCatalog.DataAccess.Models;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace BookCatalog.DataAccess.Repositories
{
    public class CsvBookRepository : ICsvBookRepository
    {
        private string GetFilePath()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return config["BooksFilePath"];
        }

        public List<Book> ReadBooksFromCsv(string filePath)
        {
            var books = new List<Book>();
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return books; // return empty list if file missing
            }

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++) // skip header
            {
                var parts = lines[i].Split(',', StringSplitOptions.TrimEntries);
                //if (parts.Length != 7) continue; // FIX: expect 7 columns (including description)

                try
                {
                    books.Add(new Book
                    {
                        Id = int.Parse(parts[0]),
                        Title = parts[1],
                        Author = parts[2],
                        Genre = parts[3],
                        PublishedYear = int.Parse(parts[4]),
                        Price = decimal.Parse(parts[5], CultureInfo.InvariantCulture),
                        Description = parts[6], // FIX: keep description
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line {i + 1}: {ex.Message}");
                }
            }
            Console.WriteLine("Worood : "+books.Count());
            return books;
        }

        private void WriteBooksToCsv(string filePath, List<Book> books)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var lines = new List<string>
            {
                // FIX: include Description column
                "Id,Title,Author,Genre,PublishedYear,Price,Description"
            };

            lines.AddRange(books.Select(b =>
                $"{b.Id},{b.Title},{b.Author},{b.Genre},{b.PublishedYear},{b.Price.ToString(CultureInfo.InvariantCulture)},{b.Description}"
            ));

            File.WriteAllLines(filePath, lines);
        }

        public List<Book> GetBooks()
        {
            string filePath = GetFilePath();
            Console.WriteLine(filePath);
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
