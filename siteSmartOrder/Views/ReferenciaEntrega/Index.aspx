<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Referencias de entrega   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="http://jqwidgets.com/jquery-widgets-demo/jqwidgets/jqx-all.js"></script>
<link rel="stylesheet" type="text/css" href="http://jqwidgets.com/jquery-widgets-demo/jqwidgets/styles/jqx.base.css" />

<script type="text/javascript">

    $('tr').change(function () {
        var valor = $(this).val();
        if (valor == "") {
            $('#referenceEdit').attr('disabled', 'disabled');
        } else {
            $('#referenceEdit').removeAttr('disabled');
        }
    });

    $(document).ready(function () {

        $('#formReference').dialog({
            autoOpen: false,
            height: 400,
            width: 400,
            modal: true,
            resizable: false,
            close: function (event, ui) {
                CierraDialog('#formReference');
            }
        });

        $('#formReferenceUpdate').dialog({
            autoOpen: false,
            height: 400,
            width: 400,
            modal: true,
            resizable: false,
            close: function (event, ui) {
                CierraDialog('#formReferenceUpdate');
            }
        });



    });

    function toggleCheckbox(element) {
        var retVal = confirm("¿Realmente desea eliminar esta referencia de entrega?");
        if (retVal == true) {
            //codigo de eliminar referencia
            var tableRowSelected = $("tr.info");
            var element = tableRowSelected.attr('id').split("Reference");
            var ReferenceId = element[1];
            EliminarReferencia(tabsReferences, ReferenceId);
            element.checked = true;
            $('#formReferenceUpdate').dialog('close');
        } else {
            element.checked = true;
        }

    }

    function isNormalInteger(str) {
        return /^([1-9]\d*)$/.test(str);
    }

    function ValidaForms(id) {
        var regreso = true;

        $(id).find('#referenceDesInput').each(function () {
            if ($(this).val() == '') {
                regreso = false;
                $(this).closest('.control-group').removeClass('success').addClass('error');
                $('#errorDescription').show();
            }
            else {
                $(this).closest('.control-group').removeClass('error').addClass('success');
                $('#errorDescription').hide();
            }
        });

        $(id).find('#referenceValueInput').each(function () {
            if (!isNormalInteger($(this).val())) {
                regreso = false;
                $(this).closest('.control-group').removeClass('success').addClass('error');
                $('#errorValue').show();
            }
            else {
                $(this).closest('.control-group').removeClass('error').addClass('success');
                $('#errorValue').hide();
            }
        });


        return regreso;
    }

    function ValidaFormsUpdate(id) {
        var regreso = true;

        $(id).find('#referenceDesUpdate').each(function () {
            if ($(this).val() == '') {
                regreso = false;
                $(this).closest('.control-group').removeClass('success').addClass('error');
                $('#errorDescriptionUpdate').show();
            }
            else {
                $(this).closest('.control-group').removeClass('error').addClass('success');
                $('#errorDescriptionUpdate').hide();
            }
        });

        $(id).find('#referenceValUpdate').each(function () {
            if (!isNormalInteger($(this).val())) {
                regreso = false;
                $(this).closest('.control-group').removeClass('success').addClass('error');
                $('#errorValueUpdate').show();
            }
            else {
                $(this).closest('.control-group').removeClass('error').addClass('success');
                $('#errorValueUpdate').hide();
            }
        });


        return regreso;
    }


    function ModalReference(nuevo) {
        $('#errorValue').hide();
        $('#errorValueUpdate').hide();
        if (nuevo) {
            $('#referenceDesInput, #referenceValueInput').val('');

            $('#formReference').dialog('option', {
                buttons: {
                    "Registrar referencia": function () {
                        if (ValidaForms('#fReference')) {
                            $(this).dialog('close');
                            CreaReferencia(tabsReferences);
                        } else {
                            $('#formReferencia > p').each(function () {
                                $(this).addClass('text-error');
                            });
                        }
                    },
                    "Cancelar": function () {
                        $(this).dialog('close');
                    }
                }
            })
                    .dialog('option', 'title', 'Nueva referencia de entrega').dialog('open');
        } else {

            document.getElementById("checkboxRefDelete").checked = true;
            var tableRowSelected = $("tr.info");
            var $td = tableRowSelected.children('td');
            var description = $td.eq(0).text();
            var value = $td.eq(1).text();

            var element = tableRowSelected.attr('id').split("Reference");
            var ReferenceId = element[1];

            $('#referenceDesUpdate').val(description);
            $('#referenceValUpdate').val(value);

            $('#formReferenceUpdate').dialog('option', {
                buttons: {
                    "Guardar cambios": function () {
                        if (ValidaFormsUpdate('#fReferenceUpdate')) {
                            $(this).dialog('close');
                            ActualizarReferencia(tabsReferences, ReferenceId);
                        } else {
                            $('#formReferenciaUpdate > p').each(function () {
                                $(this).addClass('text-error');
                            });
                        }
                    },
                    "Cancelar": function () {
                        $(this).dialog('close');
                    }
                }
            })
                    .dialog('option', 'title', 'Actualizar referencia de entrega').dialog('open');

        }
    }

    function CreaReferencia(config) {
        var newDescription = $('#referenceDesInput').val();
        var newValue = $('#referenceValueInput').val();
        var url = '<%:ResolveClientUrl("~/ReferenciaEntrega/RegisterReference")%>' + '?description=' + newDescription + '&value=' + newValue;
        $.ajax({
            url: url,
            success: function (data) {
                var response = JSON.parse(data);
                var jsonResponse = response["Data"];
                if (jsonResponse != "") {
                    PrintReferenceTable(config.tags, JSON.parse(jsonResponse), config.tableId, config.details);
                    //                PrintTable(config.tags, ParseData(JSON.parse(response.Data)), config.tableId, config.details);

                    var px = Math.floor(960 / $(config.tableId + ' th').length);
                    $(config.tableId + ' td').css('max-width', px.toString() + 'px');
                } else {
                    if (response["Message"] != "" || response["Message"] != null) {
                        alert("error: " + response["Message"]);
                    }
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {

                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/GetDeliveryReferences")%>';
            }
        });

    };

    function ActualizarReferencia(config, ReferenceId) {
        var newDescription = $('#referenceDesUpdate').val();
        var newValue = $('#referenceValUpdate').val();
        var url = '<%:ResolveClientUrl("~/ReferenciaEntrega/UpdateReference")%>';
        $.ajax({
            url: url,
            type: 'POST',
            data: {
                description: newDescription,
                value: newValue,
                id: ReferenceId
            },
            success: function (data) {
                var response = JSON.parse(data);
                var jsonResponse = response["Data"];
                if (jsonResponse != "") {
                    PrintReferenceTable(config.tags, JSON.parse(jsonResponse), config.tableId, config.details);
                    //                PrintTable(config.tags, ParseData(JSON.parse(response.Data)), config.tableId, config.details);
                    $('#referenceEdit').attr('disabled', 'disabled');
                    var px = Math.floor(960 / $(config.tableId + ' th').length);
                    $(config.tableId + ' td').css('max-width', px.toString() + 'px');
                } else {
                    if (response["Message"] != "" || response["Message"] != null) {
                        alert("Error: " + response["Message"]);
                    }
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/GetDeliveryReferences")%>';
            }
        });

    };

    function EliminarReferencia(config, ReferenceId) {
        var url = '<%:ResolveClientUrl("~/ReferenciaEntrega/DeleteReference")%>' + '?id=' + ReferenceId;
        $.ajax({
            url: url,
            success: function (data) {
                var response = JSON.parse(data);
                var jsonResponse = response["Data"];
                PrintReferenceTable(config.tags, JSON.parse(jsonResponse), config.tableId, config.details);
                //                PrintTable(config.tags, ParseData(JSON.parse(response.Data)), config.tableId, config.details);
                $('#referenceEdit').attr('disabled', 'disabled');
                var px = Math.floor(960 / $(config.tableId + ' th').length);
                $(config.tableId + ' td').css('max-width', px.toString() + 'px');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/GetDeliveryReferences")%>';
            }
        });

    };

    function trClick(id) {
        if ($("#referenceEdit").attr('disabled')) {
            //Disabled
            $('#referenceEdit').removeAttr('disabled');
        } else {
            //Not Disabled
            //            $('#referenceEdit').attr('disabled', 'disabled');
        }
        $(id).closest('tr').addClass('info').siblings().removeClass('info');
    }

    function CierraDialog(id) {
        var select = id;
        $(select + ' > p').each(function () {
            $(this).removeClass('text-error');
        });

        $(select).find('.control-group').each(function () {
            $(this).removeClass('error').removeClass('success');
        });
    }

    var tabsReferences = {
        tags: ["Description", "Value"],
        details: [],
        tableId: "#referencesGrid"
    };

    function AplicarFiltroReferencias() {
        var config = tabsReferences;
        var url = '<%:ResolveClientUrl("~/ReferenciaEntrega/SearchDeliveryReferences")%>';
        var filter = $('#filtroReferencias').val();
        $.ajax({
            url: url,
            type: 'POST',
            data: { filter: filter },
            success: function (data) {
                var response = JSON.parse(data);
                var jsonResponse = response["Data"];
                if (JSON.parse(jsonResponse).length == 0) {
                    $('#infoBoxReferences').show();
                    PrintReferenceTable(config.tags, JSON.parse(jsonResponse), config.tableId, config.details);
                } else {
                    $('#infoBoxReferences').hide();
                    PrintReferenceTable(config.tags, JSON.parse(jsonResponse), config.tableId, config.details);
                    //                PrintTable(config.tags, ParseData(JSON.parse(response.Data)), config.tableId, config.details);
                    $('#referenceEdit').attr('disabled', 'disabled');
                    var px = Math.floor(960 / $(config.tableId + ' th').length);
                    $(config.tableId + ' td').css('max-width', px.toString() + 'px');
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/GetDeliveryReferences")%>';
            }
        });
    }

    function GetReferencesTable(config) {
        
        var url = '<%:ResolveClientUrl("~/ReferenciaEntrega/GetDeliveryReferences")%>';
        $.ajax({
            url: url,
            success: function (data) {
                
                var response = JSON.parse(data);
                var jsonResponse = response["Data"];
                PrintReferenceTable(config.tags, JSON.parse(jsonResponse), config.tableId, config.details);
                //                PrintTable(config.tags, ParseData(JSON.parse(response.Data)), config.tableId, config.details);

                var px = Math.floor(960 / $(config.tableId + ' th').length);
                $(config.tableId + ' td').css('max-width', px.toString() + 'px');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 404)
                    window.location = '<%:ResolveClientUrl("~/GetDeliveryReferences")%>';
            }
        });
    }

    function inicialize() {
        GetReferencesTable(tabsReferences);
    }

</script>
<div class="row-fluid" style="margin-bottom: 30px;">
<div class="span8 offset2">
                    <div id="referenceOptions" class="tab-pane">
                                <div id="btnsReference" class="controls controls-row">
                                    <div class="span3">
                                        <button class="btn" onclick="ModalReference(true);"><i class="icon-plus"></i>Nuevo</button>
                                        <button class="btn" id="referenceEdit" onclick="ModalReference(false);" disabled><i class="icon-edit"></i>Editar</button>
                                    </div> 
                                    </div>
                </div> 
                <br>  
                    <div class="input-append" id="filterClientesDiv">
                        <input class="span3 offset8"id="filtroReferencias" type="text"/>
                      <button class="btn" type="button" onclick="AplicarFiltroReferencias();"><i class="icon-search"></i></button>
                    </div>
                                   
                    <div id="referenceGridContainer">
                          <table class="table  table-hover table-striped" width="850px" id="referencesGrid">
                            <thead>
                               <tr>
                                    <th style="text-align:center">Descripci&oacute;n</th>
                                    <th style="text-align:center">Valor</th>
                               </tr>
                            </thead>
                          </table>
                    </div>
                    <div id="referencias" class="tab-pane active">
                    <div class="alert alert-info" id="infoBoxReferences" style="display: none">
                            <strong>No hay datos para mostrar </strong>
                    </div>
                    <div class="pagination-centered" id="customerPagination">
                    </div>
                    <div id="gridCustomers">
                    </div>
                
                <div id="formReference">
                                    <p>Todos los campos son requeridos.</p>
                    <form id="fReference">
                    <div class="control-group" id="referenceDescriptiondiv">
                        <label for="referenceDescription">
                            Descripci&oacute;n</label>
                        <div class="controls">
                            <input class="input-block-level" type="text" id="referenceDesInput" onpaste="pasted(this, 20);"
                                required />
                        </div>
                    </div>
                    <div class="control-group" id="referenceValuediv">
                        <label for="referenceValue">
                            Valor</label>
                        <div class="controls">
                            <input class="input-block-level" type="text" id="referenceValueInput" onpaste="pasted(this, 20);"
                                required />
                            <div id="errorDescription" class="alert alert-danger" style="display: none">
                                <a href="#" class="alert-link">El campo "Descripci&oacute;n" no puede ser vac&iacute;o.</a>
                            </div>
                            <div id="errorValue" class="alert alert-danger" style="display: none">
                                <a href="#" class="alert-link">El campo "Valor" tiene que ser un valor num&eacute;rico.</a>
                            </div>
                        </div>
                    </div>
                </div>
                        </form>
                                   
                                </div>

                                <div id="formReferenceUpdate">
                                    <p>Todos los campos son requeridos.</p>
                                    <form id="fReferenceUpdate">
                                        <div class="control-group" id="referenceDescriptionUpdate">
                                            <label for="referenceDescripcionUpdate">Descripci&oacute;n</label>
                                            <div class="controls">
                                                <input class="input-block-level" type="text" id="referenceDesUpdate" onpaste="pasted(this, 20);" required />
                                            </div>
                                        </div>
                                        <div class="control-group" id="referenceValueUpdate">
                                            <label for="referenceValorUpdate">Valor</label>
                                            <div class="controls">
                                                <input class="input-block-level" type="text" id="referenceValUpdate" onpaste="pasted(this, 20);"
                                                    required />
                                                <div id="errorDescriptionUpdate" class="alert alert-danger" style="display: none">
                                                    <a href="#" class="alert-link">El campo "Descripci&oacute;n" no puede ser vac&iacute;o.</a>
                                                </div>
                                                <div id="errorValueUpdate" class="alert alert-danger" style="display: none">
                                                    <a href="#" class="alert-link">El campo "Valor" tiene que ser un valor num&eacute;rico.</a>
                                                </div>
                                            </div>

                                            <div class="control-group" id="referenceCheckBoxUpdate">
                                            <input type="checkbox" id="checkboxRefDelete" onchange="toggleCheckbox(this)" name="activo"  checked /> Activo

                                        </div>
                                    </form>
                                   
                                </div>


                                </div>
                               
        </div>
</div>
        



                                 <script type="text/javascript">
                                     inicialize();
                                     $('#formReference').hide();
                                     $('#formReferenceUpdate').hide();
 </script>

</asp:Content>
