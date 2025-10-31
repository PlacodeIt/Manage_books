using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArusiBook2
{
    public class Book
    {
        private string _id;
        protected string _title;
        protected string _author;
        protected string _genre;

        public Book(string title, string author, string genre)
        {
            _title = title;
            _author = author;
            _genre = genre;
            _id = author + "_" + title;
        }
        public string GetId()
        {
            return _id;
        }
        public string GetTitle()
        {
            return _title;
        }

        public string GetGenre()
        {
            return _genre;
        }

        public override string ToString()
        {
            return "Title: " + _title + ", Author: " + _author + ", Genre: " + _genre;
        }
    }
    
}
