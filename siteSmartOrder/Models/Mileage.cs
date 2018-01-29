using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteSmartOrder.Models
{
    public class Mileage
    {
        public int BranchId { get; set; }
        public int RouteId { get; set; }
        public int Travels { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Row { get; set; }
    }

    public class MileageDataTable
    {
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public IList<Mileage> Data { get; set; }
        public IList<MileageError> DataWithErrors { get; set; }
    }

    public class MileageError
    {
        public int Row { get; set; }
        public string Details { get; set; }
    }

    public class MileageResponse
    {
        public IEnumerable<Mileage> TheoreticalMileages { get; set; }
        public int TotalRecords { get; set; }
    }
}