using System;

namespace BLL.Exceptions
{
    public class WordExistsExeption : Exception
    {
        public WordExistsExeption() : base() { }
        public WordExistsExeption(string message) : base(message) { }
    }
}
