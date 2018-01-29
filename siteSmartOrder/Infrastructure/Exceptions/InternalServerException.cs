using System;

namespace siteSmartOrder.Infrastructure.Exceptions
{
    [Serializable]
    public class InternalServerException : Exception
    {
        public InternalServerException()
            : base("Ocurrió un error en el servidor remoto.")
        {
        }
    }

}