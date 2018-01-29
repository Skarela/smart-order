using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class Response<T>
    {
        public T Data { set; get; }
        public string Message { set; get; }
        public bool IsSuccess { set; get; }
        public int ErrorCode { set; get; }
        public int SuccessCode { set; get; }

    }
}