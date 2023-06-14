using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSoftware
{
    public class Book
    {
        private int _id;
        private string _title;
        private string _author;
        private string _desc;
        private int _quantity;

        public Book(int id, string title, string author, string desc, int quantity)
        {
            _id = id;
            _title = title;
            _author = author;
            _desc = desc;
            _quantity = quantity;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
    }
}