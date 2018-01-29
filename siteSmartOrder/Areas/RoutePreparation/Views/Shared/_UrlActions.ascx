<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Settings" %>
<script type="text/javascript" defer>
    <%var area = "RoutePreparation";%>
    
    var markerFolder = "<%: AppSettings.PointersFolder%>";
    var iconFolder = "<%: AppSettings.IconsFolder%>";

    var branch_Get = "<%: Url.Action("Get", "Branch", new {Area = area}) %>";
    var branch_Filter = "<%: Url.Action("Filter", "Branch", new {Area = area}) %>";
    var branch_FilterByUserOnSession = "<%: Url.Action("FilterByUserOnSession", "Branch", new {Area = area}) %>";
    var branch_SelectedBranchSession = "<%: Url.Action("SelectedBranchSession", "Branch", new {Area = area}) %>";
    
    var userPortal_Get = "<%: Url.Action("Get", "UserPortal", new {Area = area}) %>";
    var userPortal_Filter = "<%: Url.Action("Filter", "UserPortal", new {Area = area}) %>";
    
    var user_Get = "<%: Url.Action("Get", "User", new {Area = area}) %>";
    var user_Filter = "<%: Url.Action("Filter", "User", new {Area = area}) %>";
    
    var customer_Get = "<%: Url.Action("Get", "Customer", new {Area = area}) %>";
    var customer_Filter = "<%: Url.Action("Filter", "Customer", new {Area = area}) %>";
    var customer_Export = "<%: Url.Action("Export", "Customer", new {Area = area}) %>";
    
    var cooler_Index = "<%: Url.Action("Index", "Cooler", new {Area = area}) %>";
    var cooler_Get = "<%: Url.Action("Get", "Cooler", new {Area = area}) %>";
    var cooler_Filter = "<%: Url.Action("Filter", "Cooler", new {Area = area}) %>";
    var cooler_Export = "<%: Url.Action("Export", "Cooler", new {Area = area}) %>";

    var newCooler_Get = "<%: Url.Action("Get", "NewCooler", new {Area = area}) %>";
    
    var alert_Get = "<%: Url.Action("Get", "Alert", new {Area = area}) %>";
    var alert_Filter = "<%: Url.Action("Filter", "Alert", new {Area = area}) %>";
    var alert_Edit = "<%: Url.Action("Edit", "Alert", new {Area = area}) %>";
    var alert_Delete = "<%: Url.Action("Delete", "Alert", new {Area = area}) %>";
    var alert_Export = "<%: Url.Action("Export", "Alert", new {Area = area}) %>";
    var alert_GetTypes = "<%: Url.Action("GetTypes", "Alert", new {Area = area}) %>";

    var sosAlert_Filter = "<%: Url.Action("Filter", "SosAlert", new {Area = area}) %>";
    var sosAlert_Export = "<%: Url.Action("Export", "SosAlert", new {Area = area}) %>";
    var sosAlert_GetMultimedias = "<%: Url.Action("GetMultimedias", "SosAlert", new {Area = area}) %>";

    var contact_Filter = "<%: Url.Action("Filter", "Contact", new {Area = area}) %>";
    var contact_Edit = "<%: Url.Action("Edit", "Contact", new {Area = area}) %>";
    var contact_Delete = "<%: Url.Action("Delete", "Contact", new {Area = area}) %>";
    var contact_Export = "<%: Url.Action("Export", "Contact", new {Area = area}) %>";
    
    var route_Get = "<%: Url.Action("Get", "Route", new {Area = area}) %>";
    var route_Filter = "<%: Url.Action("Filter", "Route", new {Area = area}) %>";
    var route_FilterByUser = "<%: Url.Action("FilterByUser", "Route", new {Area = area}) %>";
    
    var unit_Get = "<%: Url.Action("Get", "Unit", new {Area = area}) %>";
    var unit_Filter = "<%: Url.Action("Filter", "Unit", new {Area = area}) %>";

    var mechanic_Get = "<%: Url.Action("Get", "Mechanic", new {Area = area}) %>";
    var mechanic_Filter = "<%: Url.Action("Filter", "Mechanic", new {Area = area}) %>";

    var incident_Get = "<%: Url.Action("Get", "Incident", new {Area = area}) %>";
    var incident_Filter = "<%: Url.Action("Filter", "Incident", new {Area = area}) %>";
    var incident_Export = "<%: Url.Action("Export", "Incident", new {Area = area}) %>";
    
    var incidence_Filter = "<%: Url.Action("Filter", "Incidence", new {Area = area}) %>";
    var incidence_Export = "<%: Url.Action("Export", "Incidence", new {Area = area}) %>";

    var manager_Get = "<%: Url.Action("Get", "Manager", new {Area = area}) %>";
    var manager_Filter = "<%: Url.Action("Filter", "Manager", new {Area = area}) %>";
    var manager_Edit = "<%: Url.Action("Edit", "Manager", new {Area = area}) %>";
    var manager_Delete = "<%: Url.Action("Delete", "Manager", new {Area = area}) %>";
    var manager_Export = "<%: Url.Action("Export", "Manager", new {Area = area}) %>";
    
    var checkList_RedirectToView = "<%: Url.Action("RedirectToView", "Checklist", new {Area = area}) %>";
    var checkList_Filter = "<%: Url.Action("Filter", "Checklist", new {Area = area}) %>";
    var checkList_Edit = "<%: Url.Action("Edit", "Checklist", new {Area = area}) %>";
    var checkList_Delete = "<%: Url.Action("Delete", "Checklist", new {Area = area}) %>";
    var checkList_Index = "<%: Url.Action("Index","Checklist", new {Area = area}) %>";
    var checkList_New = "<%: Url.Action("New","Checklist", new {Area = area}) %>";
    var checkList_Update = "<%: Url.Action("Update","Checklist", new {Area = area}) %>";
    var checkList_Create = "<%: Url.Action("Create", "Checklist", new {Area = area}) %>";

    var checkListReply_Filter = "<%: Url.Action("Filter", "ChecklistReply", new {Area = area}) %>";
    var checkListReply_Detail = "<%: Url.Action("Detail", "ChecklistReply", new {Area = area}) %>";
    var checkListReply_Export = "<%: Url.Action("Export", "ChecklistReply", new {Area = area}) %>";
    var checkListReply_ExportReport = "<%: Url.Action("ExportReport", "ChecklistReply", new {Area = area}) %>";

    var campaign_Filter = "<%: Url.Action("Filter", "Campaign", new {Area = area}) %>";
    var campaign_Edit = "<%: Url.Action("Edit", "Campaign", new {Area = area}) %>";
    var campaign_Delete = "<%: Url.Action("Delete", "Campaign", new {Area = area}) %>";
    var campaign_Export = "<%: Url.Action("Export", "Campaign", new {Area = area}) %>";
    var campaign_DatesBulkUpdate = "<%: Url.Action("DatesBulkUpdate", "Campaign", new {Area = area}) %>";
    var campaign_Create = "<%: Url.Action("Create", "Campaign", new {Area = area}) %>";
    var campaign_Index = "<%: Url.Action("Index","Campaign", new {Area = area}) %>";
    var campaign_New = "<%: Url.Action("New","Campaign", new {Area = area}) %>";
    var campaign_Update = "<%: Url.Action("Update","Campaign", new {Area = area}) %>";


    var campaignReply_Filter = "<%: Url.Action("Filter", "CampaignReply", new {Area = area}) %>";
    var campaignReply_Detail = "<%: Url.Action("Detail", "CampaignReply", new {Area = area}) %>";
    var campaignReply_Export = "<%: Url.Action("Export", "CampaignReply", new {Area = area}) %>";
    var campaignReply_ExportReport = "<%: Url.Action("ExportReport", "CampaignReply", new {Area = area}) %>";
    
    
    var coolerConfiguratio_RedirectToView = "<%: Url.Action("RedirectToView", "CoolerConfiguration", new {Area = area}) %>";
    var coolerConfig_Filter = "<%: Url.Action("Filter", "CoolerConfiguration", new {Area = area}) %>";
    var coolerConfig_Edit = "<%: Url.Action("Edit", "CoolerConfiguration", new {Area = area}) %>";
    var coolerConfig_Delete = "<%: Url.Action("Delete", "CoolerConfiguration", new {Area = area}) %>";
    var coolerConfig_Index = "<%: Url.Action("Index","CoolerConfiguration", new {Area = area}) %>";
    var coolerConfig_New = "<%: Url.Action("New","CoolerConfiguration", new {Area = area}) %>";
    var coolerConfig_Update = "<%: Url.Action("Update","CoolerConfiguration", new {Area = area}) %>";
    var coolerConfig_Create = "<%: Url.Action("Create", "CoolerConfiguration", new {Area = area}) %>";

    var coolerConfigurationReply_Filter = "<%: Url.Action("Filter", "CoolerConfigurationReply", new {Area = area}) %>";
    var coolerConfigurationReply_Detail = "<%: Url.Action("Detail", "CoolerConfigurationReply", new {Area = area}) %>";
    var coolerConfigurationReply_Detail = "<%: Url.Action("Detail", "CoolerConfigurationReply", new {Area = area}) %>";
    var coolerConfigurationReply_Export = "<%: Url.Action("Export", "CoolerConfigurationReply", new {Area = area}) %>";
    var coolerConfigurationReply_ExportReport = "<%: Url.Action("ExportReport", "CoolerConfigurationReply", new {Area = area}) %>";

    var coolerConfigurationReplyMultimedia_Filter = "<%: Url.Action("Filter", "CoolerConfigurationReplyMultimedia", new {Area = area}) %>";

    var newCoolerMultimedia_Filter = "<%: Url.Action("Filter", "NewCoolerMultimedia", new {Area = area}) %>";

    var evidenceMultimedia_Filter = "<%: Url.Action("Filter", "EvidenceMultimedia", new {Area = area}) %>";

    var workshop_Filter = "<%: Url.Action("Filter", "Workshop", new {Area = area}) %>";
    var workshop_Edit = "<%: Url.Action("Edit", "Workshop", new {Area = area}) %>";
    var workshop_Delete = "<%: Url.Action("Delete", "Workshop", new {Area = area}) %>";
    var workshop_Export = "<%: Url.Action("Export", "Workshop", new {Area = area}) %>";

    var workshopReply_Filter = "<%: Url.Action("Filter", "WorkshopReply", new {Area = area}) %>";
    var workshopReply_Detail = "<%: Url.Action("Detail", "WorkshopReply", new {Area = area}) %>";
    var workshopReply_Export = "<%: Url.Action("Export", "WorkshopReply", new {Area = area}) %>";
    var workshopReply_ExportReport = "<%: Url.Action("ExportReport", "WorkshopReply", new {Area = area}) %>";

    var workshopReplyMultimedia_Filter = "<%: Url.Action("Filter", "WorkshopReplyMultimedia", new {Area = area}) %>";
    
    var survey_Get = "<%: Url.Action("Get", "Survey", new {Area = area}) %>";
    var survey_Question = "<%: Url.Action("_Question", "Survey", new {Area = area}) %>";
    var survey_answer = "<%: Url.Action("_Answer", "Survey", new {Area = area}) %>";

    var applyAssignedSurvey_Get = "<%: Url.Action("Get", "ApplyAssignedSurvey", new {Area = area}) %>";

    var campaignTemplate_Edit = "<%: Url.Action("Edit", "CampaignTemplate", new {Area = area}) %>";
    var campaignTemplate_Delete = "<%: Url.Action("Delete", "CampaignTemplate", new {Area = area}) %>";
    var campaignTemplate_New = "<%: Url.Action("New","CampaignTemplate", new {Area = area}) %>";
    var campaignTemplate_Update = "<%: Url.Action("Update","CampaignTemplate", new {Area = area}) %>";
     var campaignTemplate_Create = "<%: Url.Action("Create", "CampaignTemplate", new {Area = area}) %>";
     var campaignTemplate_Index = "<%: Url.Action("Index","CampaignTemplate", new {Area = area}) %>";
    

    var checkListTemplate_Edit = "<%: Url.Action("Edit", "ChecklistTemplate", new {Area = area}) %>";
    var checkListTemplate_Delete = "<%: Url.Action("Delete", "ChecklistTemplate", new {Area = area}) %>";
    var checkListTemplate_New = "<%: Url.Action("New","ChecklistTemplate", new {Area = area}) %>";
    var checkListTemplate_Update = "<%: Url.Action("Update","ChecklistTemplate", new {Area = area}) %>";
     var checkListTemplate_Create = "<%: Url.Action("Create", "ChecklistTemplate", new {Area = area}) %>";
     var checkListTemplate_Index = "<%: Url.Action("Index","ChecklistTemplate", new {Area = area}) %>";
    

    var coolerConfigTemplate_Edit = "<%: Url.Action("Edit", "CoolerConfigurationTemplate", new {Area = area}) %>";
    var coolerConfigTemplate_Delete = "<%: Url.Action("Delete", "CoolerConfigurationTemplate", new {Area = area}) %>";
    var coolerConfigTemplate_New = "<%: Url.Action("New","CoolerConfigurationTemplate", new {Area = area}) %>";
    var coolerConfigTemplate_Update = "<%: Url.Action("Update","CoolerConfigurationTemplate", new {Area = area}) %>";
    var coolerConfigTemplate_Create = "<%: Url.Action("Create", "CoolerConfigurationTemplate", new {Area = area}) %>";
    var coolerConfigTemplate_Index = "<%: Url.Action("Index","CoolerConfigurationTemplate", new {Area = area}) %>";
    

</script>

