using System.Text.RegularExpressions;

namespace ArusiBook2
{
    public class Library
    {
        private Dictionary<string, Book> _books;
        private Dictionary<string, Subscriber> _subscribers;

        public Library()
        {
            _books = new Dictionary<string, Book>();
            _subscribers = new Dictionary<string, Subscriber>();
        }

        public void AddBook(Book book)
        {
            _books.Add(book.GetId(), book);
        }

        public bool IsBookExist(string id)
        {
            return _books.ContainsKey(id);
        }

        public Book GetBook(string id)
        {
            return _books[id];
        }

        public List<Book> GetBooks(string title)
        {
            List<Book> books = new List<Book>();
            foreach(Book book in _books.Values)
            {
                if (book.GetTitle() == title)
                {
                    books.Add(book);
                }
            }
            return books;
        }

        public void IncreaseBookCopies(string id)
        {
            ((PaperBook)_books[id]).IncreaseCopies();
        }

        public bool IsSubscriberExist(string id)
        {
            return _subscribers.ContainsKey(id);
        }

        public void AddSubscriber(Subscriber subscriber)
        {
            _subscribers.Add(subscriber.GetID(), subscriber);
        }

        public Subscriber GetSubscriber(string id)
        {
            return _subscribers[id];
        }

        public void PrintBooksByGenre(string genre)
        {
            Console.WriteLine("Books for the genre " + genre);
            foreach (Book book in _books.Values)
            {
                if (book.GetGenre() == genre)
                {
                    Console.WriteLine(book.ToString());
                    Console.WriteLine("-------------------------------");
                }
            }
        }

        public Book SearchBook(string search)
        {
            Regex regex = new Regex("^(" + search + ")");
            
            foreach(Book book in _books.Values)
            {
                if (regex.IsMatch(book.GetTitle()))
                {
                    return book;
                }
            }

            return null;
        }
    }
}
