<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="panel-title">
    Sucursales
</div>
<div class="panel-body">
    <div class="form-horizontal">
        <div class="control-group form-group">
            <div id="branchesContainer">
                <select id='mySelect' name="BranchesList" multiple="multiple" class="span12">
                </select>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {

        var branches = getBranchesFromHtml("#Branch")
        addOptionsToSelect("#mySelect", branches)

        $('select[multiple]').searchableOptionList({
            showSelectionBelowList: true,
            selectAllMaxItemsThreshold: 100,
            maxHeight: '200px',
            texts: {
                selectAll: 'Seleccionar Todos',
                selectNone: 'Seleccionar Ninguno',
                searchplaceholder: 'Clic para buscar sucursal'

            }
        });
    });
</script>
