using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArusiBook2
{
    public class Subscriber
    {
        private string _firstName;
        private string _lastName;
        private Book[] _borrowedBooks;

        public Subscriber(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            _borrowedBooks = new Book[3];
        }

        public string GetFullName()
        {
            return _firstName + " " + _lastName;
        }

        public bool HasMaximumBooks()
        {
            for (int i=0; i < _borrowedBooks.Length; i++)
            {
                if (_borrowedBooks[i] == null)
                {
                    return false;
                }
            }

            return true;
        }

        public void AddBorrowedBook(Book book)
        {
            for (int i = 0; i < _borrowedBooks.Length; i++)
            {
                if (_borrowedBooks[i] == null)
                {
                    _borrowedBooks[i] = book;
                    break;
                }
            }
        }

        public void RemoveBorrowedBook(Book book)
        {
            for (int i = 0; i < _borrowedBooks.Length; i++)
            {
                if (_borrowedBooks[i].Equals(book))
                {
                    _borrowedBooks[i] = null;
                    break;
                }
            }
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
