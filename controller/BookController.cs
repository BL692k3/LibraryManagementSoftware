using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryManagementSoftware
{
    class BookController
    {
        private List<Book> books;

        public BookController()
        {
            books = new List<Book>();
            ReadBooksFromFile("books.txt");
        }

        public void AddBook(Book book)
        {
            // Find highest id value in list
            int highestId = 0;
            foreach (Book b in books)
            {
                if (b.Id > highestId)
                {
                    highestId = b.Id;
                }
            }

            // Assign new book an id that is one higher than the highest id in the list
            book.Id = highestId + 1;

            // Check if book with same id already exists in list
            Book existingBook = books.Find(b => b.Id == book.Id);
            if (existingBook != null)
            {
                // If book already exists, update the existing book's quantity
                existingBook.Quantity += book.Quantity;
            }
            else
            {
                // If book does not already exist, add the new book to the list
                books.Add(book);
            }

            // Write updated list of books to file
            WriteBooksToFile("books.txt");
        }

        public void RemoveBook(Book book)
        {
            books.Remove(book);
            WriteBooksToFile("books.txt");
        }

        public void UpdateBook(Book book)
        {
            // Find index of book with matching id in list
            int index = books.FindIndex(b => b.Id == book.Id);
            if (index != -1)
            {
                // Replace book at index with updated book
                books[index] = book;

                // Write updated list of books to file
                WriteBooksToFile("books.txt");
            }
        }

        public List<Book> GetBooks()
        {
            return books;
        }

        public static Book GetBookById(int id)
        {
            BookController bookController = new BookController();
            List<Book> books = bookController.GetBooks();

            Book book = books.FirstOrDefault(b => b.Id == id);

            return book;
        }

        private void ReadBooksFromFile(string filename)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\data", filename);
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(',');
                        int id = int.Parse(parts[0]);
                        string title = parts[1];
                        string author = parts[2];
                        string desc = parts[3];
                        int quantity = int.Parse(parts[4]);
                        Book book = new Book(id, title, author, desc, quantity);
                        books.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading books from file: {ex.Message}");
            }
        }

        private void WriteBooksToFile(string filename)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\data", filename);
                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (Book book in books)
                    {
                        string line = $"{book.Id},{book.Title},{book.Author},{book.Desc},{book.Quantity}";
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing books to file: {ex.Message}");
            }
        }
    }
}