using System.Web;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;
using siteSmartOrder.Areas.RoutePreparation.Models.Sessions;
using siteSmartOrder.Infrastructure.Extensions;

namespace siteSmartOrder.Infrastructure.Settings
{
    public static class SessionSettings
    {
        private const string UserPortalSessionName = "UserPortal";
        private const string UserBranchesSessionName = "UserBranches";

        #region Retrieve

        public static Models.UserPortal SessionUserPortal
        {
            get { return (Models.UserPortal)HttpContext.Current.Session[UserPortalSessionName]; }
        }

        public static BranchSession SessionBranch
        {
            get { return (BranchSession)HttpContext.Current.Session[UserBranchesSessionName]; }
        }

        #endregion

        #region Create

        public static void CreateBranchSession(BranchPage branchPage)
        {
            var branchSession = new BranchSession
            {
                Branches = branchPage.Branches,
                TotalRecords = branchPage.TotalRecords
            };

            HttpContext.Current.Session.Add(UserBranchesSessionName, branchSession);
        }

        public static void SelectedBranchSession(int selectedBranch)
        {
            var branchSession = new BranchSession
            {
                Branches = SessionBranch.Branches,
                TotalRecords = SessionBranch.TotalRecords,
                SelectedBranch = selectedBranch
            };

            HttpContext.Current.Session.Add(UserBranchesSessionName, branchSession);
        }

        #endregion

        #region Exists

        public static bool ExistsSessionBranch
        {
            get { return SessionBranch.IsNotNull(); }
        }

        public static bool ExistsSessionUserPortal
        {
            get { return SessionUserPortal.IsNotNull(); }
        }

        #endregion
    }
}