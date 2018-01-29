using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using siteSmartOrder.Areas.NoticeRecharge.Models;

namespace siteSmartOrder.Areas.NoticeRecharge.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetByBranch(int branchId);
        User Get(int Id);
        User Create(User user);
        User Update(int id, User user);
        int AssignRoutes(int id, User user);
        int Deactivate(int id);
        
    }
}