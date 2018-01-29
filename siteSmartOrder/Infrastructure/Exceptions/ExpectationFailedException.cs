using System;

namespace siteSmartOrder.Infrastructure.Exceptions
{
    [Serializable]
    public class ExpectationFailedException:Exception
    {
        public ExpectationFailedException()
            : base("Se perdió la sesión con el servidor.")
        {
            
        }
    }
}