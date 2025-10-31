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
                Console.WriteLine("7. Exit");
                Console.Write("Enter operation: ");

                int op = 0;
                while (!int.TryParse(Console.ReadLine(), out op))
                {
                    Console.WriteLine("Invalid op.");
                    Console.Write("Enter operation: ");
                }

                switch (op)
                {
                    case 1:
                        AddNewBook();
                        break;

                    case 2:
                        AddNewSubscriber();
                        break;

                    case 3:
                        BorrowBook();
                        break;

                    case 4:
                        ReturnBook();
                        break;

                    case 5:
                        PrintBookInfo();
                        break;

                    case 6:
                        PrintBooksByGenre();
                        break;

                    case 7:
                        Console.WriteLine("Goodbye.");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Available operations are 1-7");
                        break;
                }

                Console.Read();
            }
        
        }

        private static void AddNewBook()
        {
            Console.Write("Type: ");
            string type = Console.ReadLine();
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();
            Console.Write("Genre: ");
            string genre = Console.ReadLine();

            Book book = null;

            if (type == "paper")
            {
                book = new PaperBook(title, author, genre);
            }
            else if (type == "digital")
            {
                book = new DigitalBook(title, author, genre);
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
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Subscriber subscriber = new Subscriber(firstName, lastName);

            if (library.IsSubscriberExist(subscriber))
            {
                Console.WriteLine("Subscriber already exist.");
                return;
            }

            library.AddSubscriber(subscriber);
            Console.WriteLine("Success!");
        }

        private static void BorrowBook()
        {
            Console.WriteLine("Subscriber Info:");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Subscriber subscriber = library.GetSubscriber(firstName, lastName);

            if (subscriber == null)
            {
                Console.WriteLine("Subscriber does not exist.");
                return;
            }

            Console.WriteLine("Book Info:");
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();

            Book book = library.GetBook(title, author);

            if (book == null)
            {
                Console.WriteLine("Book does not exist.");
                return;
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
            Console.WriteLine("Subscriber Info:");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Subscriber subscriber = library.GetSubscriber(firstName, lastName);

            if (subscriber == null)
            {
                Console.WriteLine("Subscriber does not exist.");
                return;
            }

            Console.WriteLine("Book Info:");
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();

            Book book = library.GetBook(title, author);

            if (book == null)
            {
                Console.WriteLine("Book does not exist.");
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
            Console.WriteLine("Book Info:");
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();

            Book book = library.GetBook(title, author);

            if (book == null)
            {
                Console.WriteLine("Book does not exist.");
                return;
            }

            Console.WriteLine(book.ToString());
        }

        private static void PrintBooksByGenre()
        {
            Console.Write("Genre: ");
            string genre = Console.ReadLine();

            library.PrintBooksByGenre(genre);
        }
    }
}