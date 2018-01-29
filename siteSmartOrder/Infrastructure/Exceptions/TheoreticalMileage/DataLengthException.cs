using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Infrastructure.Exceptions.TheoreticalMileage
{
    public class DataLengthException : Exception
    {
        public DataLengthException()
        {

        }
        public DataLengthException(string message) : base(message)
        {
        }
        public DataLengthException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}