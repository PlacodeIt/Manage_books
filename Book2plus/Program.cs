namespace ArusiBook2
{
    internal class Program
    {
        static Library library = new Library();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Available operations:");
                Console.WriteLine("1. Add new book");
                Console.WriteLine("2. Add new subscription");
                Console.WriteLine("3. Borrow book");
                Console.WriteLine("4. Return book");
                Console.WriteLine("5. Print book info");
                Console.WriteLine("6. Print books by genre");
                Console.WriteLine("7. Print subscriber books");
                Console.WriteLine("8. Search book by name");
                Console.WriteLine("9. Exit");
                Console.Write("Enter operation: ");

                int op = 0;
                while (!int.TryParse(Console.ReadLine(), out op))
                {
                    Console.WriteLine("Invalid op.");
                    Console.Write("Enter operation: ");
                }

                switch ((Operation)op)
                {
                    case Operation.AddBook:
                        AddNewBook();
                        break;

                    case Operation.AddSubscriber:
                        AddNewSubscriber();
                        break;

                    case Operation.BorrowBook:
                        BorrowBook();
                        break;

                    case Operation.ReturnBook:
                        ReturnBook();
                        break;

                    case Operation.PrintBookInfo:
                        PrintBookInfo();
                        break;

                    case Operation.PrintBooksByGenre:
                        PrintBooksByGenre();
                        break;

                    case Operation.PrintSubscriberBooks:
                        PrintSubscriberBooks();
                        break;

                    case Operation.SearchBook:
                        SearchBook();
                        break;

                    case Operation.Exit:
                        Console.WriteLine("Goodbye.");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Available operations are 1-8");
                        break;
                }

                Console.Read();
            }
        
        }

        private static void AddNewBook()
        {
            Console.Write("Type: ");
            string type = Console.ReadLine();
            if (type != "paper" && type != "digital")
            {
                Console.WriteLine("Book type must be paper or digital.");
                return;
            }
            Console.Write("ID: ");
            string id = Console.ReadLine();
            if (!int.TryParse(id, out int a) || id.Length != 5)
            {
                Console.WriteLine("Book ID must be 5 digits.");
                return;
            }

            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();
            Console.Write("Genre: ");
            string genre = Console.ReadLine();

            Book book = null;

            if (type == "paper")
            {
                book = new PaperBook(id, title, author, genre);
            }
            else if (type == "digital")
            {
                book = new DigitalBook(id, title, author, genre);
            }

            if (book is DigitalBook && library.IsBookExist(title))
            {
                Console.WriteLine("Book already exist.");
                return;
            }
            else if (book is PaperBook && library.IsBookExist(title))
            {
                library.IncreaseBookCopies(title);
            }

            library.AddBook(book);
            Console.WriteLine("Success!");
        }

        private static void AddNewSubscriber()
        {
            Console.Write("ID:");
            string id = Console.ReadLine();
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            if (library.IsSubscriberExist(id))
            {
                Console.WriteLine("Subscriber already exist.");
                return;
            }

            Subscriber subscriber = new Subscriber(id, firstName, lastName);

            library.AddSubscriber(subscriber);
            Console.WriteLine("Success!");
        }

        private static void BorrowBook()
        {
            Console.Write("Subscriber ID:");
            string id = Console.ReadLine();

            if (!library.IsSubscriberExist(id))
            {
                Console.WriteLine("Subscriber does not exist.");
                return;
            }

            Subscriber subscriber = library.GetSubscriber(id);

            Console.Write("Enter Book ID or Title:");
            string bookId = Console.ReadLine();
            Book book = null;

            if (bookId.Length == 5)
            {
                if (!library.IsBookExist(bookId))
                {
                    Console.WriteLine("Book does not exist.");
                    return;
                }
                book = library.GetBook(bookId);
            }
            else
            {
                List<Book> books = library.GetBooks(bookId);
                if (books.Count == 0)
                {
                    Console.WriteLine("Book does not exist.");
                    return;
                }
                else if (books.Count == 1)
                {
                    book = books[0];
                }
                else
                {
                    Console.WriteLine("Multiple books found. Choose the book ID you want to borrow:");
                    int i = 0;
                    foreach(Book b in books)
                    {
                        Console.WriteLine(i + ": " + b.GetId());
                    }
                    int index = int.Parse(Console.ReadLine());
                    book = books[index];
                }
            }

            if (book is PaperBook &&
                ((PaperBook)book).GetCopies() == 0)
            {
                Console.WriteLine("All copies of the book are already taken.");
                return;
            }

            if (subscriber.HasMaximumBooks())
            {
                Console.WriteLine("Subscriber has maximum number of allowed boaks on loan.");
            }

            if (book is PaperBook)
            {
                ((PaperBook)book).DecreaseCopies();
            }

            subscriber.AddBorrowedBook(book);

            Console.WriteLine("Success!");
        }

        private static void ReturnBook()
        {
            Console.Write("Subscriber ID: ");
            string id = Console.ReadLine();

            if (!library.IsSubscriberExist(id))
            {
                Console.WriteLine("Subscriber does not exist.");
                return;
            }

            Subscriber subscriber = library.GetSubscriber(id);

            Console.Write("Book ID: ");
            string bookId = Console.ReadLine();

            if (!library.IsBookExist(bookId))
            {
                Console.WriteLine("Book does not exist.");
                return;
            }

            Book book = library.GetBook(bookId);

            if (!subscriber.ContainsBorrowedBook(book))
            {
                Console.WriteLine("Borrowed books does not contain this book.");
                return;
            }

            if (book is PaperBook)
            {
                ((PaperBook)book).IncreaseCopies();
            }

            subscriber.RemoveBorrowedBook(book);

            Console.WriteLine("Success!");
        }

        private static void PrintBookInfo()
        {
            Console.Write("Book ID: ");
            string id = Console.ReadLine();

            if (!library.IsBookExist(id))
            {
                Console.WriteLine("Book does not exist.");
                return;
            }

            Book book = library.GetBook(id);
            Console.WriteLine(book.ToString());
        }

        private static void PrintBooksByGenre()
        {
            Console.Write("Genre: ");
            string genre = Console.ReadLine();

            library.PrintBooksByGenre(genre);
        }

        private static void PrintSubscriberBooks()
        {
            Console.Write("Subscriber ID: ");
            string id = Console.ReadLine();

            if (!library.IsSubscriberExist(id))
            {
                Console.WriteLine("Subscriber does not exist.");
                return;
            }

            Subscriber subscriber = library.GetSubscriber(id);
            HashSet<Book> books = subscriber.GetBorrowedBooks();

            Console.WriteLine("Borrowed books for " + subscriber.GetFullName() + ":");
            foreach(Book book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        private static void SearchBook()
        {
            Console.Write("Enter start of book name to search: ");
            string search = Console.ReadLine();

            Book book = library.SearchBook(search);

            if (book == null)
            {
                Console.WriteLine("No book found.");
                return;
            }

            Console.WriteLine(book.ToString());
            
        }
    }
}