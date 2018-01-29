using System;

namespace siteSmartOrder.Infrastructure.Exceptions
{
    [Serializable]
    public class ForbiddenException : Exception
    {
        public ForbiddenException()
            : base("Usuario sin permisos suficientes.")
        {
        }
    }
}