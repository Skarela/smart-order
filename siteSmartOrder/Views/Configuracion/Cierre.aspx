<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Vacio.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Confirmar
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function changeBranch() {
           $("#filterUserException").val("");
            var URL = '<%:ResolveClientUrl("~/Configuracion/GetClosureByBranchId")%>' + '?branchId=' + $("#ComboBranchId option:selected").val();
            $.ajax({
                url: URL,
                success: function (jsonData) {
                    if(jsonData != ""){
                        var response = $.parseJSON(jsonData);
                        if (response.IsSuccess) {
                            $("#ComboClosureId").val(response.Data.ClosureTypeId);
                        }
                        else {
                            alert(response.Message);
                        }
                    }
                          
                }, error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == 404)
                        window.location = '<%:ResolveClientUrl("~/Configuracion/Index")%>';
                }
            });
            loadExceptions();
        }

        function changeTypeClosure() {
            var URL = '<%:ResolveClientUrl("~/Configuracion/UpdateBranchClosure")%>' + '?branchId=' + $("#ComboBranchId option:selected").val()+'&closureType='+$("#ComboClosureId option:selected").val();
            $.ajax({
                url: URL,
                success: function (jsonData) {
                    if(jsonData != ""){
                        var response = $.parseJSON(jsonData);
                        if (!response.IsSuccess) {
                             alert(response.Message);
                        }
                    }
                          
                }, error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == 404)
                        window.location = '<%:ResolveClientUrl("~/Configuracion/Index")%>';
                }
            });

        }

        function filterUser(){
            $('#UserTable').jtable('load', {
                branchId: $("#ComboBranchId option:selected").val(),
                filter: $('#filterUser').val()
            });
        }

        function onClickBtnAdd() {
            $("#filterUser").val("");
            $("#userChooserDialog").dialog("open");
            $('#UserTable').jtable('load', {
                branchId: $("#ComboBranchId option:selected").val(),
                filter: $('#filterUser').val()
            });
            
           
        }

        function addUserToException(userId, code, name)
        {
            var id = "#closureTypeRow"+userId;  
            var closureType =  $(id).val();   
            var URL = '<%:ResolveClientUrl("~/Configuracion/UpdateUserClosure")%>' + '?userId=' + userId + '&code='+ code +'&closureType='+closureType;
           
            $.ajax({
                url: URL,
                userId: userId,
                code: code,
                name: name,
                closureType : closureType,
                success: function (jsonData) {
                    if(jsonData != ""){
                        var response = $.parseJSON(jsonData);
                        if (!response.IsSuccess) {
                             alert(response.Message);
                        }else{
                            var uid = this.userId;
                            var n = this.name;
                            var c = this.code;
                            var ct = this.closureType;
                            
                            if($('#filterUserException').val() == ""){
                                $('#ExceptionTable').jtable('addRecord', {
                                record: {
                                    userId:uid,
                                    name : n,
                                    code : c,
                                    ClosureTypeId : ct
                                },
                                clientOnly:true
                            });
                            }else{
                                loadExceptions();
                            }
                            


                             $('#UserTable').jtable('deleteRecord', {
                                key: uid,
                                clientOnly:true
                            });
                        }
                    }
                          
                }, error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == 404)
                        window.location = '<%:ResolveClientUrl("~/Configuracion/Index")%>';
                }
            });
        }

        function removeUserException(userId, code)
        {  
            var URL = '<%:ResolveClientUrl("~/Configuracion/UpdateUserClosure")%>' + '?userId=' + userId + '&code='+ code;
           
            $.ajax({
                url: URL,
                userId: userId,
                success: function (jsonData) {
                    if(jsonData != ""){
                        var response = $.parseJSON(jsonData);
                        if (!response.IsSuccess) {
                             alert(response.Message);
                        }else{
                            var uid = this.userId;
                             $('#ExceptionTable').jtable('deleteRecord', {
                                key: uid,
                                clientOnly:true
                            });
                        }
                    }
                          
                }, error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == 404)
                        window.location = '<%:ResolveClientUrl("~/Configuracion/Index")%>';
                }
            });
        }

        function loadExceptions(){
           $('#ExceptionTable').jtable('load', {
                branchId: $("#ComboBranchId option:selected").val(),
                filter: $('#filterUserException').val()
            });
        }

        function updateException(select, userId, code){
            var URL = '<%:ResolveClientUrl("~/Configuracion/UpdateUserClosure")%>' + '?userId=' + userId + '&code='+ code +'&closureType='+$(select).val();
           
            $.ajax({
                url: URL,
                success: function (jsonData) {
                    if(jsonData != ""){
                        var response = $.parseJSON(jsonData);
                        if (!response.IsSuccess) {
                             alert(response.Message);
                        }
                    }
                          
                }, error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == 404)
                        window.location = '<%:ResolveClientUrl("~/Configuracion/Index")%>';
                }
            });
        }

        $(function () {
            $("#userChooserDialog").dialog({
                autoOpen: false,
                resizable: false,
                width: 700,
                height: 450,
                modal: true,
                
            });

            $('#UserTable').jtable({
                 title: '',
                 sorting: true,
                 paging: false,
                 saveUserPreferences: false,
                 defaultSorting: 'name ASC',
                 actions: {
                    listAction: '<%:ResolveClientUrl("~/Configuracion/UserListByFiter")%>'

                },
                fields: {
                    userId: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    add: {
                        title: '',
                        display: function(data) {
                             return '<button type="button" class="btn" onclick="addUserToException(' + data.record.userId + ',' + data.record.code + ',\'' + data.record.name + '\')">Añadir</button> ';
                        }
                    },
                    code: {
                        title: 'Código',
                        width: '15%'
                    },
                    name: {
                        title: 'Nombre',
                        width: 'auto'
                    },
                    closureType: {
                        title: 'Tipo de cierre',
                        display: function(data) {
                            return '<select id="closureTypeRow'+ data.record.userId+'"><option value="1"  >Rutas</option><option value="2">Referencias</option></select> ';
                        }
                    }

                }
            });

           $('#ExceptionTable').jtable({
                 title: '',
                 sorting: true,
                 paging: false,
                 saveUserPreferences: false,
                 defaultSorting: 'Name ASC',
                 actions: {
                     listAction: '<%:ResolveClientUrl("~/Configuracion/UserExceptionListByFiter")%>'

                },
                fields: {
                    userId: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    code: {
                        title: 'Código',
                        width: '15%'
                    },
                    name: {
                        title: 'Nombre',
                        width: 'auto'
                    },
                    ClosureTypeId: {
                        title: 'Tipo de cierre',
                        display: function(data) {
                            var selectedRoute = data.record.ClosureTypeId == 1 ? 'selected="selected"' : "";
                            var selectedReferences = data.record.ClosureTypeId == 2 ? 'selected="selected"' : "";
                            return '<select id="closureTypeExceptionRow'+ data.record.userId+'" onchange="updateException(this,'+data.record.userId+','+data.record.code+');"><option value="1" ' + selectedRoute+ ' >Rutas</option><option value="2" ' + selectedReferences + '>Referencias</option></select> ';
                        }
                    },
                    deleteException: {
                        title: '',
                        width: 'auto',
                        display: function(data) {
                             return '<div style="text-align: center"><a href="javascript:removeUserException('+data.record.userId + ',' + data.record.code + ')"><i class="icon-remove"></i></a></div>';
                        }
                    }

                }
            });            


            $("#filterUser").keypress(function(e) {
                if(e.which == 13) {
                    filterUser();
                }
            });

             $("#filterUserException").keypress(function(e) {
                if(e.which == 13) {
                    loadExceptions();
                }
            });


            loadExceptions();
        });
    </script>
    <div>
         <div style="overflow:hidden">
             <div style="float:left">
               <p>Seleccione una sucursal</p>
               <%: Html.DropDownList("ComboBranchId", new SelectList(Model.ListBranch, "branchId", "name", Model.ToString()), new { onchange = "changeBranch();" })%>
             
             </div>
              <div style="float:left;margin-left:20px">
               <p>Seleccione un tipo de cierre por defecto</p>
               <%: Html.DropDownList("ComboClosureId", new SelectList(Model.ListClosure, "closureId", "closureName", Model.ClosureSelected.ClosureId), new { onchange = "changeTypeClosure();" })%>
           
             </div>
         
         </div>
         <legend />
     </div>

     <h3>Excepciones</h3>
     <p></p>

     <div>
         <button class="btn" onclick="onClickBtnAdd();">
             <i class="icon-plus"></i>Agregar</button>
         <div class="input-append" id="filterAvailableDiv" style="float:right">
             <input id="filterUserException" onkeypress="" type="text" />
             <button class="btn" type="button" onclick="loadExceptions();">
                 <i class="icon-search"></i>
             </button>
         </div>
     </div>
    <legend style="margin-top:20px"></legend>

    <div id="ExceptionTable"></div>
    <div id="userChooserDialog" style="display: none;" title="Seleccione empleado">
        <div>
             <div style="overflow:hidden">
             <input class="span3 offset8" type="text" style="float:left;width:auto" id="filterUser"/>
             <button class="btn" type="button" onclick="filterUser();" style="float:left">
                 <i class="icon-search"></i>
             </button>
         </div>
        
        <div id="UserTable"></div>
        </div>

    </div>
</asp:Content>
