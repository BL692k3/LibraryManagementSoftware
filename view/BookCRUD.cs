using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public class BookCRUD
    {
        User CurrentUser;
        public void BookCrud(User currentUser)
        {
            CurrentUser = currentUser;
            Menu menu = new Menu();
            BookController bookController = new BookController();
            UserController userController = new UserController();
            int choice = 0;
            if (userController.IsAdmin(currentUser))
            {
                while (choice != 4)
                {
                    Display();
                    choice = menu.GetInput();
                    switch (choice)
                    {
                        case 1:
                            Console.Clear(); // Clear the console screen
                            Console.WriteLine("Enter book title:");
                            string title = Console.ReadLine();

                            Console.WriteLine("Enter book author:");
                            string author = Console.ReadLine();

                            Console.WriteLine("Enter book quantity:");
                            int quantity = 0;
                            bool isQuantityValid = false;
                            while (!isQuantityValid)
                            {
                                string quantityInput = Console.ReadLine();

                                if (int.TryParse(quantityInput, out quantity))
                                {
                                    isQuantityValid = true;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid integer for quantity.");
                                }
                            }

                            Console.WriteLine("Enter book description:");
                            string description = Console.ReadLine();

                            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
                            {
                                Console.WriteLine("Title and author cannot be blank. Book not added.");
                            }
                            else
                            {
                                Book book = new Book(0, title, author, description, quantity);
                                bookController.AddBook(book);
                                Console.WriteLine("Book added successfully.");
                            }

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Enter the id of the book you want to update:");
                            int id = 0;
                            bool isIdValid = false;
                            while (!isIdValid)
                            {
                                string idInput = Console.ReadLine();

                                if (int.TryParse(idInput, out id) && id > 0)
                                {
                                    isIdValid = true;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid integer greater than 0 for id.");
                                }
                            }

                            Book bookToUpdate = bookController.GetBooks().Find(b => b.Id == id);
                            if (bookToUpdate != null)
                            {
                                Console.WriteLine($"Book found: Id({bookToUpdate.Id}) {bookToUpdate.Title} by {bookToUpdate.Author} ({bookToUpdate.Quantity} available)");
                                Console.WriteLine("Please enter the updated book information:");

                                Console.WriteLine("Enter book title:");
                                string updatedTitle = Console.ReadLine();
                                if (!string.IsNullOrEmpty(updatedTitle))
                                {
                                    bookToUpdate.Title = updatedTitle;
                                }

                                Console.WriteLine("Enter book author:");
                                string updatedAuthor = Console.ReadLine();
                                if (!string.IsNullOrEmpty(updatedAuthor))
                                {
                                    bookToUpdate.Author = updatedAuthor;
                                }

                                Console.WriteLine("Enter book quantity:");
                                int updatedQuantity = 0;
                                bool isUpdatedQuantityValid = false;
                                while (!isUpdatedQuantityValid)
                                {
                                    string quantityInput = Console.ReadLine();

                                    if (int.TryParse(quantityInput, out updatedQuantity))
                                    {
                                        isUpdatedQuantityValid = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter a valid integer for quantity.");
                                    }
                                }
                                bookToUpdate.Quantity = updatedQuantity;

                                Console.WriteLine("Enter book description:");
                                string updatedDescription = Console.ReadLine();
                                if (!string.IsNullOrEmpty(updatedDescription))
                                {
                                    bookToUpdate.Desc = updatedDescription;
                                }

                                bookController.UpdateBook(bookToUpdate);
                                Console.WriteLine("Book updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Book with id {id} not found.");
                            }

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("Enter the id of the book you want to delete:");
                            int deleteId = 0;
                            bool isDeleteIdValid = false;
                            while (!isDeleteIdValid)
                            {
                                string deleteIdInput = Console.ReadLine();

                                if (int.TryParse(deleteIdInput, out deleteId) && deleteId > 0)
                                {
                                    isDeleteIdValid = true;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid integer greater than 0 for id.");
                                }
                            }

                            Book bookToDelete = bookController.GetBooks().Find(b => b.Id == deleteId);
                            if (bookToDelete != null)
                            {
                                Console.WriteLine($"Are you sure you want to delete the book with id {deleteId}? (Y/N)");
                                string confirmationInput = Console.ReadLine();

                                if (confirmationInput.ToLower() == "y")
                                {
                                    bookController.RemoveBook(bookToDelete);
                                    Console.WriteLine("Book deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Deletion cancelled.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Book with id {deleteId} not found.");
                            }

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine("Going back to main Menu.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            else
            {
                Display();
                choice = menu.GetInput();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Going back to main Menu.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.Please try again.");
                        break;
                }
            }
        }
        public void Display()
        {
            BookController bookController = new BookController();
            UserController userController = new UserController();
            List<Book> books = bookController.GetBooks();
            foreach (Book book in books)
            {
                Console.WriteLine($"Id({book.Id}) {book.Title} by {book.Author} ({book.Quantity} available)");
            }
            if (userController.IsAdmin(CurrentUser))
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Add a book");
                Console.WriteLine("2. Update a book");
                Console.WriteLine("3. Delete a book");
                Console.WriteLine("4. Quit");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Quit");
                Console.WriteLine();
            }
        }
    }
}