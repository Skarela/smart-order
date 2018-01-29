<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
        <div class="alert alert-error" id ="errorBox" style="display: none">
                <a class="close" data-dismiss="alert">×</a>
                <strong>Error!</strong> Ocurrio un error desconocido.
       </div>

         <div style="min-height:200px;">
          <div class="alert alert-info" id="infoBox2" style="display: none">
                <a class="close" onclick="$('#infoBox2').hide();">×</a>
                    <strong>No hay datos para mostrar </strong>
            </div>
                <table class="table  table-hover table-striped">
                    <thead>
                       <tr>
                            <th style="text-align:center">C&oacute;digo</th>
                            <th style="text-align:center"><i class="icon-user"></i>Nombre</th>
                            <th style="text-align:center"><i class="icon-home"></i>Ruta</th>
                            <th style="text-align:center"><i class="icon-wrench"></i>Opciones</th>
                       </tr>
                    </thead>
                     <% 
                       if (Model.Count > 0)
                        {
                            foreach (var item in Model)
                            { 
                    %>
                    <tr>
                        <td style="text-align:center">
                         <%:item.code %>
                        </td>
                         <td style="text-align:center">
                         <%: item.name %>
                        </td>
                         <td style="text-align:center">
                        <%: item.routeName!=null ? item.routeName :""%>
                        </td>
                         <td style="text-align:center">
                         <!--a title="Código QR" style="cursor:pointer"  id="btn_qr_<%:item.userId %>"><i class="icon-qrcode"></i></a-->
                         <a rel="popover"   id="btn_qr_<%:item.userId %>"><i class="icon-qrcode"></i></a>
                         <script type="text/javascript">
                             var url = '<%:ResolveUrl("~/Content/QRCobratario.ashx")%>';
                             $("#btn_qr_<%:item.userId %>").popover({title: 'QR-<%: item.name%>', content: "<img src='"+url+"?userCode="+'<%:item.code %>'+"'>", html: true });
                         </script>
                        </td>
                    </tr>
                     <%    }
                        }
                       else{
                           %>
                            <script type="text/javascript">$("#infoBox2").show();</script>
                         <%} %>
                        
                </table>
                 
            </div>
