filterBranchToUserOnSession();
createListUnitSelectize();
createListMechanicSelectize();
FromCreationDateRangePicker();
ToCreationDateRangePicker();
eventToChangeBranch();
clickToExport();
clickToExportReport();
createTable();

function eventToChangeBranch() {
    $("#Branch").change('input', function () {
        destroySelectize("#UnitIds");
        createListUnitSelectize();
        destroySelectize("#MechanicIds");
        createListMechanicSelectize();
    });
}

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "UnitIds=" + $("#UnitIds").val() + "&" + "MechanicIds=" + $("#MechanicIds").val() + "&" + "FromCreationDate=" + $("#FromCreationDate").val() + "&" + "ToCreationDate=" + $("#ToCreationDate").val();
        exportPerAjax(workshopReply_Export + data);
    });
}

function clickToExportReport() {
    $("#btnExportReport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "UnitIds=" + $("#UnitIds").val() + "&" + "MechanicIds=" + $("#MechanicIds").val() + "&" + "FromCreationDate=" + $("#FromCreationDate").val() + "&" + "ToCreationDate=" + $("#ToCreationDate").val();
        exportPerAjax(workshopReply_ExportReport + data);
    });
}

function createTable() {
    table.setUri(workshopReply_Filter);
    table.setColumns(initColumns(), false, false, false);
    table.setFilters({ BranchId: "Branch", UnitIds: "UnitIds", MechanicIds: "MechanicIds" });
    table.create(getData(), false);
}

function initColumns() {
    return [
        {
            field: 'Id',
            align: 'center',
            visible: false,
            valign: 'middle'
        }, {
            field: 'MechanicId',
            title: 'Mec&aacute;nico',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#mechanicNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(mechanic_Get, { id: row.MechanicId }, callback);
                return "<div id='mechanicNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando mec&aacute;nico... </div>";
            }
        }, {
            field: 'UnitId',
            title: 'Unidad',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#unitCodeColumn" + row.Id).html(response.Record.Code);
                };
                getPerAjax(unit_Get, { id: row.UnitId }, callback);
                return "<div id='unitCodeColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando unidad... </div>";
            }
        },{
            field: 'ApplyAssignedSurveyId',
            title: 'Encuesta',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#surveyNameColumn" + row.Id).html(response.Record.AssignedSurvey.Survey.Name);
                };
                getPerAjax(applyAssignedSurvey_Get, { id: row.ApplyAssignedSurveyId }, callback);
                return "<div id='surveyNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando encuesta... </div>";
            }
        },  {
            field: 'CreationDate',
            title: 'Fecha',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'OtherButtons',
            title: '',
            align: 'left',
            width: "40",
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var btnMultimedia = " <a id='multimedia_" + row.Id + "' class='btnMultimedia iconPointer' style='color:black; margin-right:10px'><i class='fa fa-picture-o'></i></a>";
                var btnVieweDetail = " <a class='btnViewDetail iconPointer' style='color:black;'><i class='fa fa-eye'></i></a>";
                return btnMultimedia + btnVieweDetail;
            },
            events: window.operateEvents = {
                'click .btnMultimedia': function (e, value, row) {
                    var callback = function (response) {
                        if (response.Records.length > 0) {
                            setImagesToModal(response.Records);
                        } else {
                            alertInfo("Sin contenido multimedia.");
                            $("#multimedia_" + row.Id).removeClass("btnMultimedia").removeClass("iconPointer").css("color", "grey").unbind();
                            clearOpacity();
                        }
                    };
                    addOpacity();
                    filterPerAjax(workshopReplyMultimedia_Filter, { WorkshopReplyId: row.Id }, callback);
                }, 'click .btnViewDetail': function (e, value, row) {
                    window.location.href = workshopReply_Detail + "?id=" + row.ApplyAssignedSurveyId;
                }
            }
        }
    ];
}

function getData() {
    return {
        BranchId: $("#Branch").val(),
        FromCreationDate: $("#FromCreationDate").val(),
        ToCreationDate: $("#ToCreationDate").val(),
        SortBy: "Id",
        Sort: "Asc",
        PageSize: 10,
        PageNumber: 1
    };
}
