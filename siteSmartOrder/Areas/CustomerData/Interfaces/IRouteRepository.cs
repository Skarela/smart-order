using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using siteSmartOrder.Areas.CustomerData.Models;

namespace siteSmartOrder.Areas.CustomerData.Interfaces
{
    public interface IRouteRepository
    {
        List<Route> GetByBranch(int branchId);
        List<Route> GetByUser(int userId);

    }
}