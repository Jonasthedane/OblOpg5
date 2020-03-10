using System;
using System.Collections.Generic;
using System.Text;

namespace OblServer
{
    public class Book
    {
        private string _titel;
        private string _forfatter;
        private int _sidetal;
        private string _isbn13;

        public string Titel
        {
            get { return _titel; }
            set { _titel = value; }
        }

        public string Forfatter
        {
            get { return _forfatter; }
            set
            {
                if (value.Length >= 2)
                {
                    _forfatter = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Forfatterens navn skal være over 2 bogstaver");
                }
            }
        }
        public int Sidetal
        {
            get { return _sidetal; }
            set
            {
                if (value >= 4 && value <= 1000)
                {
                    _sidetal = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Sidetal skal være mellem 4 og 1000");
                }
            }
        }

        public string Isbn13
        {
            get { return _isbn13; }
            set
            {
                if (value.Length == 13)
                {
                    _isbn13 = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("isbn13 skal være 13 bogstaver");
                }
            }
        }

        public override string ToString()
        {
            return $"Bog titel: {Titel} Forfatter: {Forfatter} Sidetal: {Sidetal} Isbn13: {Isbn13}";
        }

        public Book(string titel, string forfatter, int sidetal, string isbn13)
        {
            Titel = titel;
            Forfatter = forfatter;
            Sidetal = sidetal;
            Isbn13 = isbn13;
        }

        public Book()
        {
            
        }


    }
}
