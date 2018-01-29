using System.Collections.Generic;

namespace siteSmartOrder.Models.Audit
{
   public class UserDataModel
   {
      public UsersDataModel Data { get; set; }
   }

   public class UsersDataModel
   {
      public List<UserToAudit> Data { get; set; }
   }

   public class UserToAudit
   {
      public int userId { set; get; }
      public string code { set; get; }
      public string name { set; get; }
   }
}