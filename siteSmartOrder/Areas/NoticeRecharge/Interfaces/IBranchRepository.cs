using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using siteSmartOrder.Areas.NoticeRecharge.Models;

namespace siteSmartOrder.Areas.NoticeRecharge.Interfaces
{
    public interface IBranchRepository
    {
        List<Branch> Get();
    }
}