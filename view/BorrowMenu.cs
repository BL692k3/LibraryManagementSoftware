using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public class BorrowMenu
    {
        internal void borrowMenu(User currentUser)
        {
            Console.Clear();
            Menu menu = new Menu();
            UserController userController = new UserController();
            BorrowController borrowController = new BorrowController();
            BookController bookController = new BookController();
            int choice = 0;

            while (choice != 3)
            {
                while (true)
                {
                    display(currentUser);
                    Console.Write("Enter your choice: ");

                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice!");
                    }
                }

                switch (choice)
                {
                    case 1:
                        if (!userController.IsAdmin(currentUser))
                        {
                            Console.Clear();
                            List<Book> books = bookController.GetBooks();
                            foreach (Book book in books)
                            {
                                Console.WriteLine($"Id({book.Id}) Title: {book.Title}. Author: {book.Author}. Quantity: {book.Quantity}.");
                            }
                            Console.Write("Enter the book id to borrow: ");
                            int bookId = int.Parse(Console.ReadLine());
                            Book selectedBook = BookController.GetBookById(bookId);

                            if (selectedBook.Quantity <= 0)
                            {
                                Console.WriteLine("Sorry, the book is not available now.");
                            }
                            else
                            {
                                Member borrower = new Member(currentUser.Id, currentUser.Username, currentUser.Password, currentUser.Email);
                                if (borrower == null)
                                {
                                    Console.WriteLine("Invalid borrower id!");
                                }
                                else
                                {
                                    DateTime borrowDate = DateTime.Now;
                                    DateTime returnDate = borrowDate.AddDays(14);
                                    Borrow borrow = new Borrow(borrowController.GetBorrows().Count + 1, borrower, selectedBook, borrowDate, returnDate);
                                    borrowController.AddBorrow(borrow);
                                    Console.WriteLine($"Successfully borrowed {selectedBook.Title} by {selectedBook.Author}.");
                                }
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Only members are allowed to use this function");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case 2:
                        if (!userController.IsAdmin(currentUser))
                        {
                            Console.Clear();
                            List<Borrow> borrows = borrowController.GetBorrows().Where(b => b.Borrower.Id == currentUser.Id).ToList();
                            foreach (Borrow borrow in borrows)
                            {
                                Console.WriteLine($"Id({borrow.Id}) Book: {borrow.Book.Title}. Borrow date:{borrow.BorrowDate}. Return date: {borrow.ReturnDate}.");
                            }

                            Console.Write("Enter the borrow id to return: ");
                            int borrowId;

                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out borrowId))
                                {
                                    if (borrows.Any(b => b.Id == borrowId))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid borrow id! Please enter a valid borrow id.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input! Please enter a valid borrow id.");
                                }
                            }

                            Borrow selectedBorrow = borrows.FirstOrDefault(b => b.Id == borrowId);
                            selectedBorrow.ReturnDate = DateTime.Now;
                            borrowController.RemoveBorrow(selectedBorrow);
                            Console.WriteLine($"Successfully returned {selectedBorrow.Book.Title} by {selectedBorrow.Book.Author}.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Only members are allowed to use this function");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                    case 3:
                        Console.WriteLine("Quiting back to main menu");
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }

        public void display(User currentUser)
        {
            BorrowController borrowController = new BorrowController();
            List<Borrow> borrows = borrowController.GetBorrows().Where(b => b.Borrower.Id == currentUser.Id).ToList();
            foreach (Borrow borrow in borrows)
            {
                Console.WriteLine($"Id({borrow.Id}) Book: {borrow.Book.Title}. Borrow date:{borrow.BorrowDate}. Return date: {borrow.ReturnDate}.");
            }
            Console.WriteLine("-------------------------");
            Console.WriteLine("1. Borrow a book");
            Console.WriteLine("2. Return a book");
            Console.WriteLine("3. Quit");
            Console.WriteLine();
        }
    }
}
