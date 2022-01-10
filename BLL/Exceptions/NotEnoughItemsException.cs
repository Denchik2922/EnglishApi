using System;

namespace BLL.Exceptions
{
    public class NotEnoughItemsException : Exception
    {
        public NotEnoughItemsException() : base() { }
        public NotEnoughItemsException(string message) : base(message) { }
    }
}
