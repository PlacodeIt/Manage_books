using System.Data;
using System.Data.SqlClient;

namespace ArusiBook2
{
    internal class Program
    {
        private static string _connectionString = @"Server=localhost\SQLEXPRESS;Database=Arusi_db;Trusted_Connection=True;";
        private static SqlConnection _sqlConnection;

        static void Main(string[] args)
        {
            InitDatabase();
            CreateTables();
            PopulateTables();
            MainMenu();
        }

        private static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Book Library");
            Console.WriteLine("1. Manage Library");
            Console.WriteLine("2. Borrow/Return Books");
            Console.WriteLine("3. Library Information");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    ManageLibrary();
                    break;

                case 2:
                    BorrowReturnBooks();
                    break;

                case 3:
                    LibraryInformation();
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.ReadLine();
        }

        private static void BorrowReturnBooks()
        {
            Console.Clear();
            Console.WriteLine("Enter subscriber id:");
            string subscriberId = Console.ReadLine();
            if (!SubscriberExists(subscriberId))
            {
                Console.WriteLine("Subscriber does not exist");
                return;
            }

            Console.WriteLine("Select action");
            Console.WriteLine("1. Borrow a book");
            Console.WriteLine("2. Return a book");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    BorrowBook(subscriberId);
                    break;
                case 2:
                    ReturnBook(subscriberId);
                    break;
                default:
                    break;
            }

            Console.WriteLine("Press any key to go back to main menu . . .");
            Console.ReadLine();
            MainMenu();
        }

        private static void BorrowBook(string subscriberId)
        {
            // Check if the subscriber has available slots to borrow
            DataSet subscriberDataSet = new DataSet();
            SqlDataAdapter subscriberAdapter = new SqlDataAdapter("SELECT * FROM subscribers WHERE Id = '" + subscriberId + "'", _sqlConnection);
            subscriberAdapter.Fill(subscriberDataSet, "subscribers");
            DataRow subscriberRow = subscriberDataSet.Tables["subscribers"].Rows[0];

            if (subscriberRow["book1"].ToString().Length > 0 &&
                   subscriberRow["book2"].ToString().Length > 0 &&
                   subscriberRow["book3"].ToString().Length > 0)
            {
                Console.WriteLine("Subscriber with ID " + subscriberId + " has no available slots to borrow a book.");
                return;
            }

            // Take book ID from the user
            Console.Write("Enter book ID: ");
            int bookId = int.Parse(Console.ReadLine());

            // Check if book exists
            DataSet bookDataSet = new DataSet();
            SqlDataAdapter bookAdapter = new SqlDataAdapter("SELECT * FROM books WHERE Id = '" + bookId + "'", _sqlConnection);
            bookAdapter.Fill(bookDataSet, "books");

            // Check if book exists
            if (bookDataSet.Tables["books"].Rows.Count == 0)
            {
                Console.WriteLine("Book with ID " + bookId + " doesn't exist.");
                return;
            }

            // Check the book copies
            DataRow bookRow = bookDataSet.Tables["books"].Rows[0];
            int copies = (int)bookRow["copies"];
            if (copies == 0)
            {
                Console.WriteLine("Book with ID " + bookId + " has no available copies.");
                return;
            }

            // Add the book's ID to the next available slot for the subscriber
            if (subscriberRow["book1"].ToString().Length == 0)
            {
                subscriberRow["book1"] = bookId;
            }
            else if (subscriberRow["book2"].ToString().Length == 0)
            {
                subscriberRow["book2"] = bookId;
            }
            else
            {
                subscriberRow["book3"] = bookId;
            }

            // Reduce the book copies by 1
            bookRow["copies"] = copies - 1;
            // Save changes to the database
            SqlCommandBuilder subscribersCommandBuilder = new SqlCommandBuilder(subscriberAdapter);
            subscriberAdapter.Update(subscriberDataSet, "subscribers");
            SqlCommandBuilder booksCommandBuilder = new SqlCommandBuilder(bookAdapter);
            bookAdapter.Update(bookDataSet, "books");

            Console.WriteLine("Book with ID " + bookId + " has been successfully borrowed by subscriber with ID " + subscriberId + ".");

            //Console.WriteLine("Book with ID " + bookId + " has been successfully borrowed by subscriber with ID " + subscriberId + ".");
        }

        private static void ReturnBook(string subscriberId)
        {
            DataSet subscriberDataSet = new DataSet();
            SqlDataAdapter subscriberAdapter = new SqlDataAdapter("SELECT * FROM subscribers WHERE Id = '" + subscriberId + "'", _sqlConnection);
            subscriberAdapter.Fill(subscriberDataSet, "subscribers");
            DataRow subscriberRow = subscriberDataSet.Tables["subscribers"].Rows[0];

            // Take book ID from the user
            Console.Write("Enter book ID: ");
            string bookId = Console.ReadLine();

            // Check if the subscriber has the book
            if (subscriberRow["book1"].ToString() == bookId)
            {
                subscriberRow["book1"] = DBNull.Value;
            }
            else if (subscriberRow["book2"].ToString() == bookId)
            {
                subscriberRow["book2"] = DBNull.Value;
            }
            else if (subscriberRow["book3"].ToString() == bookId)
            {
                subscriberRow["book3"] = DBNull.Value;
            }
            else
            {
                Console.WriteLine("Subscriber with ID " + subscriberId + " has not borrowed book with ID " + bookId + ".");
                return;
            }

            // Increase the book copies by 1
            DataSet bookDataSet = new DataSet();
            SqlDataAdapter bookAdapter = new SqlDataAdapter("SELECT * FROM books WHERE Id = '" + bookId + "'", _sqlConnection);
            bookAdapter.Fill(bookDataSet, "books");
            DataRow bookRow = bookDataSet.Tables["books"].Rows[0];
            int copies = (int)bookRow["copies"];
            bookRow["copies"] = copies + 1;

            // Save changes to the database
            SqlCommandBuilder subscribersCommandBuilder = new SqlCommandBuilder(subscriberAdapter);
            subscriberAdapter.Update(subscriberDataSet, "subscribers");
            SqlCommandBuilder booksCommandBuilder = new SqlCommandBuilder(bookAdapter);
            bookAdapter.Update(bookDataSet, "books");

            Console.WriteLine("Book with ID " + bookId + " has been successfully returned by subscriber with ID " + subscriberId + ".");
        }

        private static void ManageLibrary()
        {
            Console.Clear();
            Console.WriteLine("Manage Library");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add Subscriber");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    AddBook();
                    break;

                case 2:
                    AddSubscriber();
                    break;

                default:
                    break;
            }

            Console.WriteLine("Press any key to go back to main menu . . .");
            Console.ReadLine();
            MainMenu();
        }

        private static void LibraryInformation()
        {
            Console.Clear();
            Console.WriteLine("Library Information");
            Console.WriteLine("1. Show Book By ID");
            Console.WriteLine("2. Show Books By Genre");
            Console.WriteLine("3. Show Subscriber By ID");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    PrintBookInfo();
                    break;

                case 2:
                    PrintBooksByGenre();
                    break;

                case 3:
                    PrintSubscriberInfo();
                    break;

                default:
                    break;
            }

            Console.WriteLine("Press any key to go back to main menu . . .");
            Console.ReadLine();
            MainMenu();
        }

        private static void PrintBookInfo()
        {
            Console.WriteLine("Enter book ID: ");
            string id = Console.ReadLine();
            DataSet dataSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM books WHERE id = '" + id + "'", _sqlConnection))
            {
                adapter.Fill(dataSet, "books");
            }

            if (dataSet.Tables["books"].Rows.Count == 0)
            {
                Console.WriteLine("No book with the given ID was found.");
                return;
            }

            DataRow row = dataSet.Tables["books"].Rows[0];

            Console.WriteLine("Book Information:");
            Console.WriteLine("ID: " + row["id"].ToString());
            Console.WriteLine("Author: " + row["author"].ToString());
            Console.WriteLine("Title: " + row["title"].ToString());
            Console.WriteLine("Genre: " + row["genre"].ToString());
            Console.WriteLine("Type: " + row["type"].ToString());
            Console.WriteLine("Copies: " + row["copies"].ToString());
        }

        private static void PrintBooksByGenre()
        {
            Console.WriteLine("Enter genre: ");
            string genre = Console.ReadLine();
            DataSet booksDataSet = new DataSet();     
            using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM books WHERE genre = '" + genre + "'", _sqlConnection))
            {
                adapter.Fill(booksDataSet, "books");
            }

            if (booksDataSet.Tables["books"].Rows.Count == 0)
            {
                Console.WriteLine("No books with the given genre were found.");
            }
            else
            {
                Console.WriteLine("Books Information:");
                foreach (DataRow bookRow in booksDataSet.Tables["books"].Rows)
                {
                    Console.WriteLine("ID: " + bookRow["id"].ToString());
                    Console.WriteLine("Author: " + bookRow["author"].ToString());
                    Console.WriteLine("Title: " + bookRow["title"].ToString());
                    Console.WriteLine("Genre: " + bookRow["genre"].ToString());
                    Console.WriteLine("Type: " + bookRow["type"].ToString());
                    Console.WriteLine("Copies: " + bookRow["copies"].ToString());
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine();
                }
            }
        }

        private static void PrintSubscriberInfo()
        {
            Console.WriteLine("Enter subscriber ID: ");
            string id = Console.ReadLine();
            DataSet subscribersDataSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM subscribers WHERE id = '" + id + "'", _sqlConnection))
            {
                adapter.Fill(subscribersDataSet, "subscribers");
            }

            if (subscribersDataSet.Tables["subscribers"].Rows.Count == 0)
            {
                Console.WriteLine("No subscriber with the given ID was found.");
                return;
            }

            DataRow subscriberRow = subscribersDataSet.Tables["subscribers"].Rows[0];

            Console.WriteLine("Subscriber Information:");
            Console.WriteLine("ID: " + subscriberRow["id"].ToString());
            Console.WriteLine("First Name: " + subscriberRow["firstName"].ToString());
            Console.WriteLine("Last Name: " + subscriberRow["lastName"].ToString());
            Console.WriteLine("Book 1: " + subscriberRow["book1"].ToString());
            Console.WriteLine("Book 2: " + subscriberRow["book2"].ToString());
            Console.WriteLine("Book 3: " + subscriberRow["book3"].ToString());
        }

        private static void AddBook()
        {
            Console.WriteLine("Enter book id (5 digits): ");
            string id = Console.ReadLine();
            Console.WriteLine("Enter author: ");
            string author = Console.ReadLine();
            Console.WriteLine("Enter title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter  genre: ");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter type (digital/paper): ");
            string type = Console.ReadLine();

            if (BookExists(id))
            {
                if (type == "digital")
                {
                    Console.WriteLine("Book already exists.");
                }
                else
                {
                    IncreaseBookCopies(id);
                }

                return;
            }

            InsertBook(id, author, title, genre, type);
        }

        private static void AddSubscriber()
        {
            Console.WriteLine("Enter subscriber id (9 digits): ");
            string id = Console.ReadLine();
            Console.WriteLine("Enter first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter last name: ");
            string lastName = Console.ReadLine();

            if (SubscriberExists(id))
            {
                Console.WriteLine("Subscriber already exists.");
                return;
            }

            InsertSubscriber(id, firstName, lastName);
        }

        private static void InitDatabase()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlConnection.Open();
        }

        private static void CreateTables()
        {
            using (SqlCommand command = new SqlCommand("DROP TABLE IF EXISTS books", _sqlConnection))
            {
                command.ExecuteNonQuery();
            }

            using (SqlCommand command = new SqlCommand("CREATE TABLE books (id NVARCHAR(5) PRIMARY KEY, author NVARCHAR(50), title NVARCHAR(50), genre NVARCHAR(50), type NVARCHAR(10), copies INT)", _sqlConnection))
            {
                command.ExecuteNonQuery();
            }

            using (SqlCommand command = new SqlCommand("DROP TABLE IF EXISTS subscribers", _sqlConnection))
            {
                command.ExecuteNonQuery();
            }

            using (SqlCommand command = new SqlCommand("CREATE TABLE subscribers (id NVARCHAR(9) PRIMARY KEY, firstName NVARCHAR(50), lastName NVARCHAR(50), book1 NVARCHAR(5), book2 NVARCHAR(5), book3 NVARCHAR(5))", _sqlConnection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void PopulateTables()
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO books (id, author, title, genre, type, copies) VALUES (10001, 'J.K. Rowling', 'Harry Potter and the Philosopher''s Stone', 'Fantasy', 'Paper', 5), (10002, 'J.K. Rowling', 'Harry Potter and the Chamber of Secrets', 'Fantasy', 'Paper', 3), (10003, 'J.K. Rowling', 'Harry Potter and the Prisoner of Azkaban', 'Fantasy', 'Paper', 4), (10004, 'J.K. Rowling', 'Harry Potter and the Goblet of Fire', 'Fantasy', 'Paper', 2), (10005, 'J.K. Rowling', 'Harry Potter and the Order of Phoenix', 'Fantasy', 'Paper', 3)", _sqlConnection))
            {
                command.ExecuteNonQuery();
            }

            using (SqlCommand command = new SqlCommand("INSERT INTO subscribers (id, firstName, lastName, book1, book2, book3) VALUES (20001, 'John', 'Doe', '', '', ''), (20002, 'Jane', 'Doe', '', '', ''), (20003, 'Jim', 'Smith', '', '', ''), (20004, 'Jack', 'Johnson', '', '', ''), (20005, 'Jill', 'Williams', '', '', '')", _sqlConnection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static bool BookExists(string id)
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM books WHERE id = @id", _sqlConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private static void IncreaseBookCopies(string id)
        {
            using (SqlCommand command = new SqlCommand("UPDATE books SET copies = copies + 1 WHERE id = @id", _sqlConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        private static void InsertBook(string id, string author, string title, string genre, string type)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO books (id, author, title, genre, type, copies) VALUES (@id, @author, @title, @genre, @type, @copies)", _sqlConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@genre", genre);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@copies", 1);
                command.ExecuteNonQuery();
            }
        }

        private static bool SubscriberExists(string id)
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM subscribers WHERE id = @id", _sqlConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private static void InsertSubscriber(string id, string firstName, string lastName)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO subscribers (id, firstName, lastName) VALUES (@id, @firstName, @lastName)", _sqlConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.ExecuteNonQuery();
            }
        }
    }
}