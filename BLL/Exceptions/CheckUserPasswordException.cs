using System;

namespace BLL.Exceptions
{
    public class CheckUserPasswordException : Exception
    {
        public CheckUserPasswordException() : base() { }
        public CheckUserPasswordException(string message) : base(message) { }
    }
}
