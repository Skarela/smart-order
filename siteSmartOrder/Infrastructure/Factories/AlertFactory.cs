using System.Web.Mvc;
using siteSmartOrder.Infrastructure.Enums;
using siteSmartOrder.Infrastructure.Factories.Interfaces;

namespace siteSmartOrder.Infrastructure.Factories
{
    public class AlertFactory : Controller, IAlertFactory
    {
        #region Create

        public void CreateFailure(ControllerBase controller, string message)
        {

            controller.TempData[Alert.Failure.ToString()] = message;
        }

        public void CreateSuccess(ControllerBase controller, string message)
        {
            controller.TempData[Alert.Success.ToString()] = message;
        }

        public void CreateInformation(ControllerBase controller, string message)
        {
            controller.TempData[Alert.Information.ToString()] = message;
        }

        public void CreateWarning(ControllerBase controller, string message)
        {
            controller.TempData[Alert.Warning.ToString()] = message;
        }

        public void CreateLostSession(ControllerBase controller, string message)
        {
            controller.TempData[Alert.LostSession.ToString()] = message;
        }

        #endregion

        #region Remove

        public void RemoveFailure(ControllerBase controller)
        {
            controller.TempData[Alert.Failure.ToString()] = null;
        }

        public void RemoveSuccess(ControllerBase controller)
        {
            controller.TempData[Alert.Success.ToString()] = null;
        }

        public void RemoveInformation(ControllerBase controller)
        {
            controller.TempData[Alert.Information.ToString()] = null;
        }

        public void RemoveWarning(ControllerBase controller)
        {
            controller.TempData[Alert.Warning.ToString()] = null;
        }

        public void RemoveLostSession(ControllerBase controller)
        {
            controller.TempData[Alert.LostSession.ToString()] = null;
        }

        #endregion
    }
}