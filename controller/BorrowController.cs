using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public class BorrowController
    {
        private List<Borrow> _borrows;
        private string _borrowFilePath;

        public BorrowController()
        {
            _borrows = new List<Borrow>();
            _borrowFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\data", "Borrowed_books.txt");
            LoadBorrowsFromFile();
        }

        public List<Borrow> GetBorrows()
        {
            LoadBorrowsFromFile();
            return _borrows;
        }

        public void AddBorrow(Borrow borrow)
        {
            BookController bookController = new BookController();
            _borrows.Add(borrow);
            borrow.Book.Quantity--;
            bookController.UpdateBook(borrow.Book);
            SaveBorrowsToFile();
            LoadBorrowsFromFile();
        }

        public void RemoveBorrow(Borrow borrow)
        {
            BookController bookController = new BookController();
            _borrows.Remove(borrow);
            borrow.Book.Quantity++;
            bookController.UpdateBook(borrow.Book);
            SaveBorrowsToFile();
            LoadBorrowsFromFile();
        }

        private void LoadBorrowsFromFile()
        {
            _borrows.Clear();

            if (File.Exists(_borrowFilePath))
            {
                using (StreamReader reader = new StreamReader(_borrowFilePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = line.Split(',');

                        if (fields.Length == 5)
                        {
                            int id = int.Parse(fields[0]);
                            int borrowerId = int.Parse(fields[1]);
                            int bookId = int.Parse(fields[2]);
                            DateTime borrowDate = DateTime.Parse(fields[3]);
                            DateTime returnDate = DateTime.Parse(fields[4]);

                            Member borrower = UserController.GetUserById(borrowerId) as Member;
                            Book book = BookController.GetBookById(bookId);

                            if (borrower != null && book != null)
                            {
                                Borrow borrow = new Borrow(id, borrower, book, borrowDate, returnDate);
                                _borrows.Add(borrow);
                            }
                        }
                    }
                }
            }
        }

        private void SaveBorrowsToFile()
        {
            using (StreamWriter writer = new StreamWriter(_borrowFilePath))
            {
                foreach (Borrow borrow in _borrows)
                {
                    int id = borrow.Id;
                    int borrowerId = borrow.Borrower.Id;
                    int bookId = borrow.Book.Id;
                    DateTime borrowDate = borrow.BorrowDate;
                    DateTime returnDate = borrow.ReturnDate;

                    string line = $"{id},{borrowerId},{bookId},{borrowDate},{returnDate}";

                    writer.WriteLine(line);
                }
            }
        }
    }
}