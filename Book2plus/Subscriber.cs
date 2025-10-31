using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArusiBook2
{
    public class Subscriber
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private HashSet<Book> _borrowedBooks;

        public Subscriber(string id, string firstName, string lastName)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _borrowedBooks = new HashSet<Book>(3);
        }


        public string GetID()
        {
            return _id;
        }

        public string GetFullName()
        {
            return _firstName + " " + _lastName;
        }

        public bool HasMaximumBooks()
        {
            return _borrowedBooks.Count == 3;
        }

        public void AddBorrowedBook(Book book)
        {
            _borrowedBooks.Add(book);
        }

        public void RemoveBorrowedBook(Book book)
        {
            _borrowedBooks.Remove(book);
        }

        public bool ContainsBorrowedBook(Book book)
        {
            return _borrowedBooks.Contains(book);
        }

        public HashSet<Book> GetBorrowedBooks()
        {
            return _borrowedBooks;
        }

        public override bool Equals(object obj)
        {
            Subscriber other = obj as Subscriber;

            if (other is null)
            {
                return false;
            }

            return GetFullName() == other.GetFullName();
        }
    }
}
