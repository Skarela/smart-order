
using System.Web.Script.Serialization;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class IncidenceFilter : Filter
    {
        public IncidenceFilter()
        {
            TypeId = 0;
            ActionId = 0;
            BranchId = 0;
            UserId = 0;
            UnitCode = "";
            StartDate = "";
            EndDate = "";
        }

        public int TypeId { get; set; }
        public int ActionId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public string UnitCode { get; set; }
        public string  StartDate { get; set; }
        public string EndDate { get; set; }

        public JsonRequest CreateJsonRequest()
        {
            return new JsonRequest(new JavaScriptSerializer().Serialize(this));
        }
    }

    public class JsonRequest
    {
        public JsonRequest(string strJson)
        {
            IncidenceFilterJson = strJson;

        }
        public string IncidenceFilterJson { get; set; }


    }
}