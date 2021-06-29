using System;

namespace SimpleSales.Api.Exceptions
{
    [Serializable]
    public class NotEnoughItemQuantityException : Exception
    {
        public NotEnoughItemQuantityException(string message = "Not enough item quantity") : base(message) { }
    }
}
