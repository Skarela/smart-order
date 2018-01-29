<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<!-- Modal -->
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel">
                    Editar vigencia</h4>
            </div>
            <div class="modal-body">
                <div class="control-group">
                    <label class="control-label" for="NewStartDate">
                        Fecha de Inicio:</label>
                    <input type="text" class="input-small" id="NewStartDate" name="StartDate" placeholder="DD/MM/AAAA"
                        maxlength="10" autocomplete="off" />
                </div>
                <div class="control-group">
                    <label class="control-label" for="NewEndDate">
                        Fecha de Fin:</label>
                    <input type="text" class="input-small" id="NewEndDate" placeholder="DD/MM/AAAA" maxlength="10"
                        autocomplete="off" />
                </div>
            </div>
            <span class="has-error error-dates" style="display: none;"><small class="help-block"
                style="margin-left: 20%">La fecha de inicio debe ser menor o igual a la fecha fin.</small>
            </span>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">
                    Cerrar</button>
                <button type="button" id="AssingNewDates-btn" class="btn btn-primary">
                    Asignar Vigencia</button>
            </div>
        </div>
    </div>
</div>
