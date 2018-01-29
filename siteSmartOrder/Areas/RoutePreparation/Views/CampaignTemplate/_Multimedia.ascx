<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Campaign>" %>
<%@ Import Namespace="System.Web.Helpers" %>
<%@ Import Namespace="siteSmartOrder.Areas.RoutePreparation.Enums" %>
<%@ Import Namespace="siteSmartOrder.Infrastructure.Extensions" %>
<div class="panel-title">
    Multimedia
    <%if (Model.CampaignMultimedias.IsNotEmpty())
      {
          var multimediaIndex = 0;%>
    <div id="previewCampaignMultimedias" class="btn btn-default btn-mini seeAllImages"
        data-originaltype="<%:Model.CampaignMultimediaType %>" data-filesupload="<%:Model.CampaignMultimedias.Count%>"
        data-allimages="<%:Json.Encode(Model.CampaignMultimedias)%>">
        <i class="icon-picture"></i>
    </div>
    <% foreach (var campaignMultimedia in Model.CampaignMultimedias)
       {%>
    <%: Html.Hidden("CampaignMultimedias[" + multimediaIndex + "].Id", campaignMultimedia.Id)%>
    <%
        multimediaIndex ++;
       } %>
    <%}
      else
      {%>
    <div id="previewCampaignMultimedias" class="btn btn-mini disabled" style="opacity: 0.60"
        data-filesupload="0" data-originaltype="<%:Model.CampaignMultimediaType %>">
        <i class="icon-picture"></i>
    </div>
    <%}%>
</div>
<div class="panel-body" style="overflow: hidden">
    <div class="form-horizontal">
        <div class="control-group form-group">
            <label class="control-label">
                Tipo</label>
            <div class="controls">
                <% 
                    var types = new CampaignMultimediaType().ConvertToCollection();
                    var @class = new { @class = "form-control span3", id = "CampaignMultimediaType", data_role = "none", data_firsttype = Model.CampaignMultimediaType };
                %>
                <%:Html.DropDownList("CampaignMultimediaType", new SelectList(types, "Value", "Name", Model.ToString()), @class) %>
            </div>
        </div>
        <div class="control-group form-group" style="margin-bottom: 5px">
            <label class="control-label">
                Imágenes</label>
            <div class="controls">
                <div id="buttonsToMultimedia">
                    <div id="btn_uploadMultiemedia" class="btn btn-primary btn-small ">
                        <i class="fa fa-upload"></i>Cargar multimedia
                    </div>
                    <div id="previewMultimedia" class=" span8 iconPointer" style="display: none; margin-left: 0 !important">
                    </div>
                </div>
                <input type="file" multiple="multiple" name="Files" id="Files" class="hide" />
            </div>
        </div>
    </div>
</div>
