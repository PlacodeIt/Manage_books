using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArusiBook2
{
    internal class PaperBook : Book
    {
        private int _copies;

        public PaperBook(string title, string author, string genre)
            : base(title, author, genre)
        {
            _copies = 1;
        }

        public int GetCopies()
        {
            return _copies;
        }

        public void IncreaseCopies()
        {
            _copies++;
        }

        public void DecreaseCopies()
        {
            _copies--;
        }

        public override string ToString()
        {
            return base.ToString() + ", Number of available copies: " + _copies;
        }
    }
}
