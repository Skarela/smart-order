using siteSmartOrder.Areas.RoutePreparation.Models;
using siteSmartOrder.Areas.RoutePreparation.Models.Filters;
using siteSmartOrder.Areas.RoutePreparation.Models.Pages;

namespace siteSmartOrder.Areas.RoutePreparation.Services.Interfaces
{
    public interface IContactService
    {
        Contact Get(int id);
        ContactPage Filter(ContactFilter request);
        void Create(Contact contact);
        void Update(Contact contact);
        void Delete(int id);
    }
}