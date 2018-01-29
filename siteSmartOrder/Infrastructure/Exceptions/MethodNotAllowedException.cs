using System;

namespace siteSmartOrder.Infrastructure.Exceptions
{
    [Serializable]
    public class MethodNotAllowedException:Exception
    {
        public MethodNotAllowedException()
            :base("Método no permitido")
        {
            
        }
    }
}