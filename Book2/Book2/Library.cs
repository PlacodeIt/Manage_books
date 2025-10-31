namespace ArusiBook2
{
    public class Library
    {
        private Book[] _books;
        private Subscriber[] _subscribers;

        public Library()
        {
            _books = new Book[100];
            _subscribers = new Subscriber[1000];
        }

        public void AddBook(Book book)
        {
            for (int i = 0; i < _books.Length; i++)
            {
                if (_books[i] == null)
                {
                    _books[i] = book;
                    break;
                }
            }
        }

        public bool IsBookExist(string title)
        {
            for (int i=0; i < _books.Length; i++)
            {
                Book book = _books[i];

                if (book != null && book.GetTitle() == title)
                {
                    return true;
                }
            }

            return false;
        }

        public Book GetBook(string title, string authorName)
        {
            string id = authorName + "_" + title;

            for (int i = 0; i < _books.Length; i++)
            {
                Book book = _books[i];

                if (book != null && book.GetId() == id)
                {
                    return book;
                }
            }

            return null;
        }

        public void IncreaseBookCopies(string title)
        {
            for (int i = 0; i < _books.Length; i++)
            {
                Book book = _books[i];

                if (book != null && book.GetTitle() == title)
                {
                    ((PaperBook)book).IncreaseCopies();
                    break;
                }
            }
        }

        public bool IsSubscriberExist(Subscriber subscriber)
        {
            for (int i=0; i < _subscribers.Length; i++)
            {
                Subscriber sub = _subscribers[i];

                if (sub != null && sub.Equals(subscriber))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddSubscriber(Subscriber subscriber)
        {
            for (int i = 0; i < _subscribers.Length; i++)
            {
                if (_subscribers[i] == null)
                {
                    _subscribers[i] = subscriber;
                    break;
                }
            }
        }

        public Subscriber GetSubscriber(string firstName, string lastName)
        {
            string fullName = firstName + " " + lastName;
            for (int i = 0; i < _subscribers.Length; i++)
            {
                Subscriber sub = _subscribers[i];

                if (sub != null && sub.GetFullName() == fullName)
                {
                    return sub;
                }

            }

            return null;
        }

        public void PrintBooksByGenre(string genre)
        {
            Console.WriteLine("Books for the genre " + genre);
            for (int i=0; i < _books.Length; i++)
            {
                Book book = _books[i];

                if (book != null && book.GetGenre() == genre)
                {
                    Console.WriteLine(book.ToString());
                    Console.WriteLine("-------------------------------");
                }
            }
        }
    }
}
