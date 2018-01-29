<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<siteSmartOrder.Models.Branch>>" %>

   <div id="branches-container">
        
        <div class="row">
            <div class="col-md-5>Sucursal</div>
            <div class="col-md-3 col-md-offset-1">Remover</div>
        </div>
        
        <%foreach(var item in Model){%>
            <div id="<%:item.branchId %>" class="row">
                <div class="col-md-5>
                    <select id="branches-select">
                        <option value="<%: item.branchId %>"><%: item.name %></option>
                    </select>
                </div>
                <div class="col-md-3>
                    <button class="btn btn-danger"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></button>
                </div>
            </div>
                  
        <%}%>
        

    
   </div>
   <button id="addBranch-btn" class="btn btn-primary">Agregar Sucursal</button>

