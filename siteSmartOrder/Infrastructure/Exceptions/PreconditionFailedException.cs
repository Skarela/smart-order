using System;

namespace siteSmartOrder.Infrastructure.Exceptions
{
    [Serializable]
    public class PreconditionFailedException : Exception
    {
        public PreconditionFailedException()
            : base("El folio ya está registrado y aceptado.")
        {

        }

        public PreconditionFailedException(string message)
            : base(message)
        {

        }
    }
}