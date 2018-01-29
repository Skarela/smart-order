using System;

namespace siteSmartOrder.Infrastructure.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException()
            : base("La solicitud es inválida.")
        {
        }
    }
}