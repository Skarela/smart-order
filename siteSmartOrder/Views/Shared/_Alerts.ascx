<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Enums" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Extensions" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Helpers" %>

<%
    var success = TempData[Alert.Success.ToString()];
    var warning = TempData[Alert.Warning.ToString()];
    var information = TempData[Alert.Information.ToString()];
    var failure = TempData[Alert.Failure.ToString()];
    var lostSession = TempData[Alert.LostSession.ToString()];

    if (TempData[Alert.Success.ToString()].IsNotNull())
    {%>
        <%=Html.Success(success.ToString())%>
        <%TempData[Alert.Success.ToString()] = null;
    }

    if (TempData[Alert.Warning.ToString()].IsNotNull())
    {%>
        <%=Html.Warning(warning.ToString())%>
        <%TempData[Alert.Warning.ToString()] = null;
    }

    if (TempData[Alert.Information.ToString()].IsNotNull())
    {%>
        <%=Html.Information(information.ToString())%>
        <%TempData[Alert.Information.ToString()] = null;
    }

    if (TempData[Alert.LostSession.ToString()].IsNotNull())
    {%>
        <%=Html.Information(lostSession.ToString())%>
        <%TempData[Alert.LostSession.ToString()] = null;
    }

    if (failure.IsNotNull())
    {%>
        <%=Html.Failure(failure.ToString())%>
        <%TempData[Alert.Failure.ToString()] = null;
    }
    
%>
