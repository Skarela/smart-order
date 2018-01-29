using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using siteSmartOrder.Areas.CustomerData.Models;

namespace siteSmartOrder.Areas.CustomerData.Interfaces
{
    public interface IBranchRepository
    {
        List<Branch> Get();
    }
}