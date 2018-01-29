using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class SmtpSetting
    {
        public int SmtpSettingId { set; get; }
        public string Smtp { set;get; }
        public string Sender { set; get; }
        public int Port { set; get; }
        public bool Ssl { set; get; }
        public bool Autentication { set; get; }
        public string UserCredential { set; get; }
        public string Password { set; get; }
    }
}