namespace siteSmartOrder.Areas.RoutePreparation.Models.Filters
{
    public class Filter
    {
        public Filter()
        {
            StartPage = 0;
            EndPage = 0;
            Sort = "";
            SortBy = "";
        }

        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public string Sort { get; set; }
        public string SortBy { get; set; }
    }
}