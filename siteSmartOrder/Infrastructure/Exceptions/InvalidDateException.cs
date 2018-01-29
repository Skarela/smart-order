using System;

namespace siteSmartOrder.Infrastructure.Exceptions
{
    [Serializable]
    public class InvalidDateException : Exception
    {
        public InvalidDateException()
            : base("Fechas inválidas.")
        {
        }
        public InvalidDateException(string message)
            : base(message)
        {
        }
    }

}