namespace siteSmartOrder.Areas.RoutePreparation.Resolvers
{
    public static class SosAlertStatusResolver
    {
        public static string ResolverStatus(this string sosAlertStatus)
        {
            switch (sosAlertStatus)
            {
                case "NotStarted":
                    return "NO iniciado";
                case "InProgress":
                    return "En progreso";
                case "Finalized":
                    return "Finalizado";
                default:
                    return "Sin definir";
            }
        }
    }
}