<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<siteSmartOrder.Areas.RoutePreparation.Models.Pages.CampaignMultimediaPage>" %>
<%@ Import Namespace="System.Web.Helpers" %>

<multimedia-modal multimedias="<%: Json.Encode(Model.CampaignMultimedias) %>">
    <div id="multimediaModal" class="modal hide fade">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>     
            <h3>Cat&aacute;logo Multimedia</h3>               
        </div>
        <div class="modal-body">            
            <multimedia-modal-filter>
                <div class="form-horizontal">
                    <div class="control-group form-group">
                        <label class="control-label" for="searchBox">Filtrar por nombre:</label>
                        <div class="controls">                            
                            <input type="text" id="searchBox" onkeyup="MultimediaModalFilter.search()" placeholder="Filtrar por nombre"/>                        
                        </div>
                    </div>                                    
                </div>                
            </multimedia-modal-filter>
            <ul class="multimedias clearfix"></ul>
        </div>
        <div class="modal-footer">
            <span class="error-message"></span>
            <button class="btn" data-dismiss="modal">Cerrar</button>
        </div>
    </div>
</multimedia-modal>
