using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;
using siteSmartOrder.Areas.RoutePreparation.Models.Files;
using siteSmartOrder.Infrastructure.Extensions;

namespace siteSmartOrder.Areas.RoutePreparation.Models
{
    [DataContract]
    public class Manager : File
    {
        private HttpPostedFileBase _file;

        public Manager()
        {
            Id = 0;
            Name = "";
            Company = "";
            Address = "";
            Email = "";
            Phone = "";
            ImagePath = "";
            Incidents = new List<Incident>();
            Branches = new List<Branch>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Branch> Branches { get; set; }
        public string ImagePath
        {
            get { return FilePath; }
            set { ResolvePath(value); }
        }

        public List<int> AssignedIncidents
        {
            get { return Incidents.Select(incident => incident.Id).ToList(); }
            set { Incidents.AddRange(value.Select(incident => new Incident {Id = incident, Name = "Only Id"})); }
        }

        public List<int> AssignedBranches
        {
            get { return Branches.Select(branch => branch.Id).ToList(); }
            set { Branches.AddRange(value.Select(branch => new Branch { Id = branch, Name = "Only Id" })); }
        }

        public List<Incident> Incidents { get; set; }

        [ScriptIgnore]
        public HttpPostedFileBase File
        {
            get { return _file; }
            set
            {
                var file = value;
                if (file.IsNotNull() && file.ContentLength.IsGreaterThanZero())
                    _file = file;
            }
        }

    }
}