using System;

namespace siteSmartOrder.Infrastructure.Exceptions
{
    [Serializable]
    public class ServerNotFoundException : Exception
    {
        public ServerNotFoundException()
            : base("No es posible conectar con el servidor remoto.")
        {
        }
    }

}