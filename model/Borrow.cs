using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibraryManagementSoftware
{
    public class Borrow
    {
        private int _id;
        private Member _borrower;
        private Book _book;
        private DateTime _borrowDate;
        private DateTime _returnDate;

        public Borrow(int id, Member borrower, Book book, DateTime borrowDate, DateTime returnDate)
        {
            _id = id;
            _borrower = borrower;
            _book = book;
            _borrowDate = borrowDate;
            _returnDate = returnDate;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Member Borrower
        {
            get { return _borrower; }
            set { _borrower = value; }
        }

        public Book Book
        {
            get { return _book; }
            set { _book = value; }
        }

        public DateTime BorrowDate
        {
            get { return _borrowDate; }
            set { _borrowDate = value; }
        }

        public DateTime ReturnDate
        {
            get { return _returnDate; }
            set { _returnDate = value; }
        }
    }
}
