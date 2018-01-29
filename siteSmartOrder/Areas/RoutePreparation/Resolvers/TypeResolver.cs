using siteSmartOrder.Areas.RoutePreparation.Enums;

namespace siteSmartOrder.Areas.RoutePreparation.Resolvers
{
    public static class TypeResolver
    {
        public static string ResolverQuestionType(this int questionType)
        {
            switch (questionType)
            {
                case (int) QuestionType.Text:
                    return "Texto";
                case (int) QuestionType.Numeric:
                    return "Numérico";
                case (int) QuestionType.MultipleChoice:
                    return "Selección Múltiple";
                case (int) QuestionType.Dichotomy:
                    return "Tipo Si/No";
                case (int)QuestionType.Photo:
                    return "Tipo Foto";
                default:
                    return "Sin definir";
            }
        }
        public static string ResolverAlertType(this int alertType)
        {
            switch (alertType)
            {
                case (int)AlertType.Configuration:
                    return "Configuración";
                case (int)AlertType.NewCooler:
                    return "Nuevo enfriador";
                case (int)AlertType.NotExist:
                    return "No existente";
                default:
                    return "Sin definir";
            }
        }
    }
}