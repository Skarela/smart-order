using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Infrastructure.Exceptions.TheoreticalMileage
{
    public class ExcelHeaderException : Exception
    {
        public ExcelHeaderException()
        {

        }
        public ExcelHeaderException(string message) : base(message)
        {
        }
        public ExcelHeaderException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}