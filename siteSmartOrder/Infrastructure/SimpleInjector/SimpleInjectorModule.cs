using RestSharp;
using SimpleInjector;
using siteSmartOrder.Areas.RoutePreparation.Services;
using siteSmartOrder.Areas.RoutePreparation.Services.Interfaces;
using siteSmartOrder.Infrastructure.Factories;
using siteSmartOrder.Infrastructure.Factories.Interfaces;
using siteSmartOrder.Infrastructure.Tools;

namespace siteSmartOrder.Infrastructure.SimpleInjector
{
    public static class SimpleInjectorModule
    {
        private static Container _container;

        public static void SetContainer(Container container)
        {
            _container = container;
        }

        public static Container GetContainer()
        {
            return _container;
        }

        public static void VerifyContainer()
        {
            _container.RegisterMvcIntegratedFilterProvider();
            _container.Verify();
        }

        public static void Load()
        {
            _container.Register(() => new RestClient(), Lifestyle.Transient);

            _container.Register<ISurveyService, SurveyService>(Lifestyle.Transient);
            _container.Register<ICampaignService, CampaignService>(Lifestyle.Transient);
            _container.Register<IWorkshopService, WorkshopService>(Lifestyle.Transient);
            _container.Register<IChecklistService, ChecklistService>(Lifestyle.Transient);
            _container.Register<IMechanicService, MechanicService>(Lifestyle.Transient);
            _container.Register<IUnitService, UnitService>(Lifestyle.Transient);
            _container.Register<IBranchService, BranchService>(Lifestyle.Transient);
            _container.Register<IRouteService, RouteService>(Lifestyle.Transient);
            _container.Register<IUserService, UserService>(Lifestyle.Transient);
            _container.Register<IUserPortalService, UserPortalService>(Lifestyle.Transient);
            _container.Register<ICampaignReplyService, CampaignReplyService>(Lifestyle.Transient);
            _container.Register<IChecklistReplyService, ChecklistReplyService>(Lifestyle.Transient);
            _container.Register<IWorkshopReplyService, WorkshopReplyService>(Lifestyle.Transient);
            _container.Register<IWorkshopReplyMultimediaService, WorkshopReplyMultimediaService>(Lifestyle.Transient);
            _container.Register<IApplyAssignedSurveyService, ApplyAssignedSurveyService>(Lifestyle.Transient);
            _container.Register<ICampaignMultimediaService, CampaignMultimediaService>(Lifestyle.Transient);
            _container.Register<IAssignedSurveyService, AssignedSurveyService>(Lifestyle.Transient);
            _container.Register<ICustomerService, CustomerService>(Lifestyle.Transient);
            _container.Register<ICoolerService, CoolerService>(Lifestyle.Transient);
            _container.Register<IAlertService, AlertService>(Lifestyle.Transient);
            _container.Register<IAlertConfigurationService, AlertConfigurationService>(Lifestyle.Transient);
            _container.Register<IContactService, ContactService>(Lifestyle.Transient);
            _container.Register<INewCoolerService, NewCoolerService>(Lifestyle.Transient);
            _container.Register<ICoolerConfigurationService, CoolerConfigurationService>(Lifestyle.Transient);
            _container.Register<ICoolerConfigurationReplyService, CoolerConfigurationReplyService>(Lifestyle.Transient);
            _container.Register<IIncidentService, IncidentService>(Lifestyle.Transient);
            _container.Register<IManagerService, ManagerService>(Lifestyle.Transient);
            _container.Register<ISosAlertService, SosAlertService>(Lifestyle.Transient);
            _container.Register<IIncidenceService, IncidenceService>(Lifestyle.Transient);
            _container.Register<ICoolerConfigurationReplyMultimediaService, CoolerConfigurationReplyMultimediaService>(Lifestyle.Transient);
            _container.Register<IEvidenceMultimediaService, EvidenceMultimediaService>(Lifestyle.Transient);
            _container.Register<INewCoolerMultimediaService, NewCoolerMultimediaService>(Lifestyle.Transient);

            _container.Register<IJsonFactory, JsonFactory>(Lifestyle.Singleton);
            _container.Register<IAlertFactory, AlertFactory>(Lifestyle.Singleton);
            _container.Register<IClient, Client>(Lifestyle.Transient);
        }
    }
}