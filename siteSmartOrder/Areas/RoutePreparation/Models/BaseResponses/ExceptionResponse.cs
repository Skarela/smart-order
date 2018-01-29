namespace siteSmartOrder.Areas.RoutePreparation.Models.BaseResponses
{
    public class ExceptionResponse
    {
        public ExceptionResponse()
        {
            Message = "";
            ErrorCode = 0;
        }

        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }
}