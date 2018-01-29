using System.Web.Mvc;

namespace siteSmartOrder.Infrastructure.Factories.Interfaces
{
    public interface IAlertFactory
    {
        void CreateFailure(ControllerBase controller, string message);
        void CreateSuccess(ControllerBase controller, string message);
        void CreateInformation(ControllerBase controller, string message);
        void CreateWarning(ControllerBase controller, string message);
        void CreateLostSession(ControllerBase controller, string message);

        void RemoveFailure(ControllerBase controller);
        void RemoveSuccess(ControllerBase controller);
        void RemoveInformation(ControllerBase controller);
        void RemoveWarning(ControllerBase controller);
        void RemoveLostSession(ControllerBase controller); 
    }
}