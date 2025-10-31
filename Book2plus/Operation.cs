using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArusiBook2
{
    public enum Operation
    {
        AddBook = 1,
        AddSubscriber = 2,
        BorrowBook = 3,
        ReturnBook = 4,
        PrintBookInfo = 5,
        PrintBooksByGenre = 6,
        PrintSubscriberBooks = 7,
        SearchBook = 8,
        Exit = 9    
    }
}
