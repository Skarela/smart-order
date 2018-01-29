filterBranchToUserOnSession();
createListUserSelectize();
createListCustomerSelectize();
FromCreationDateRangePicker();
ToCreationDateRangePicker();
eventToChangeBranch();
clickToExport();
clickToExportReport();
createTable();

function eventToChangeBranch() {
    $("#Branch").change('input', function () {
        destroySelectize("#UserIds");
        createListUserSelectize();
        destroySelectize("#CustomerIds");
        createListCustomerSelectize();
    });
}

function clickToExport() {
    $("#btnExport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "UserIds=" + $("#UserIds").val() + "&" + "CustomerIds=" + $("#CustomerIds").val() + "&" + "FromCreationDate=" + $("#FromCreationDate").val() + "&" + "ToCreationDate=" + $("#ToCreationDate").val();
        exportPerAjax(coolerConfigurationReply_Export + data);
    });
}

function clickToExportReport() {
    $("#btnExportReport").click(function () {
        var data = "?" + "BranchId=" + $("#Branch").val() + "&" + "UserIds=" + $("#UserIds").val() + "&" + "CustomerIds=" + $("#CustomerIds").val() + "&" + "FromCreationDate=" + $("#FromCreationDate").val() + "&" + "ToCreationDate=" + $("#ToCreationDate").val();
        exportPerAjax(coolerConfigurationReply_ExportReport + data);
    });
}
function createTable() {
    table.setUri(coolerConfigurationReply_Filter);
    table.setColumns(initColumns(), false, false, false);
    table.setFilters({ BranchId: "Branch", UserIds: "UserIds", CustomerIds: "CustomerIds" });
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
            field: 'CoolerConfigurationId',
            visible: false,
            align: 'left',
            valign: 'middle'
        }, {
            field: 'UserId',
            title: 'Usuario',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                var callback = function (response) {
                    $("#userNameColumn" + row.Id).html(response.Record.Name);
                };
                getPerAjax(user_Get, { id: row.UserId }, callback);
                return "<div id='userNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando usuario... </div>";
            }
        }, {
            field: 'CustomerId',
            title: 'Cliente',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function(value, row) {
                if (row.CoolerId != "" && row.CoolerId != null) {
                    var callback = function(response) {
                        $("#customerNameColumn" + row.Id).html(response.Record.Name);
                    };
                    getPerAjax(customer_Get, { id: row.CustomerId }, callback);
                    return "<div id='customerNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando cliente... </div>";
                }
                return "-";
            }
        }, {
            field: 'CoolerId',
            title: 'Enfriador',
            align: 'left',
            sortable: false,
            valign: 'middle',
            formatter: function(value, row) {
                if (value != "" && value !=null) {
                    var callback = function(response) {
                        $("#coolerNameColumn" + row.Id).html(response.Record.Name);
                    };
                    getPerAjax(cooler_Get, { id: row.CoolerId }, callback);
                    return "<div id='coolerNameColumn" + row.Id + "'><i class='fa fa-spinner fa-spin text-muted'></i> Cargando enfirador... </div>";
                }
                return "-";
            }
        },{
            field: 'CreationDate',
            title: 'Fecha',
            align: 'left',
            sortable: false,
            valign: 'middle'
        }, {
            field: 'Status',
            title: 'Estados',
            align: 'center',
            sortable: false,
            width: "120",    
            valign: 'middle',
            formatter: function (value, row) {
                var style = 'margin-left: 3px; padding-top:1px; padding-bottom:1px;font-size: 9px;';
                var exists = row.Exists ? '<span class="btnMultimedia badge badge-success iconPointer" style="' + style + '"><i  class="fa fa-check" ></i></span>' : '<span class="badge badge-important" style="' + style + '"><i  class="fa fa-times"></i></span>';
                var evidences = row.Exists ? '<span class="btnEvidences badge badge-success iconPointer" style="' + style + '"><i  class="fa fa-picture-o" ></i></span>' : '<span class="badge badge-degault" style="' + style + '"><i  class="fa fa-picture-o"></i></span>';
                var contaminated = !row.Exists ? '<span class="badge badge-default" style="' + style + '"><i  class="fa fa-heartbeat" ></i></span>' : row.Contaminated ? '<span class="badge badge-important" style="' + style + '"><i  class="fa fa-heartbeat" ></i></span>' : '<span class="badge badge-success" style="' + style + '"><i  class="fa fa-heart"></i></span>';
                var goodCondition = !row.Exists ? '<span class="badge badge-default" style="' + style + '"><i  class="fa fa-thumbs-up" ></i></span>' : row.GoodCondition ? '<span class="badge badge-success" style="' + style + '"><i  class="fa fa-thumbs-up" ></i></span>' : '<span class="badge badge-important" style="' + style + '"><i  class="fa fa-thumbs-down"></i></span>';
                var newCooler = row.NewCoolerId > 0 ? '<span class=" btnNewCoolerMultimedia badge badge-success  iconPointer" style="margin-left: 3px; padding-left: 9px; padding-top:1px; padding-bottom:1px;font-size: 10px; width: 80px;">Nuevo</span> ' : '';
                return exists + contaminated + goodCondition + evidences + newCooler;
            },
            events: window.operateEvents = {
                'click .btnMultimedia': function(e, value, row) {
                    var callback = function(response) {
                        if (response.Records.length > 0) {
                            setImagesToModal(response.Records);
                        } else {
                            alertInfo("Sin contenido multimedia.");
                            clearOpacity();
                        }
                    };
                    addOpacity();
                    otherActionPerAjax(coolerConfigurationReplyMultimedia_Filter, { CoolerConfigurationReplyId: row.Id }, false, callback, "GET", true);
                },
                'click .btnNewCoolerMultimedia': function(e, value, row) {
                    var callback = function(response) {
                        if (response.Records.length > 0) {
                            setImagesToModal(response.Records);
                        } else {
                            alertInfo("Sin contenido multimedia.");
                            clearOpacity();
                        }
                    };
                    addOpacity();
                    otherActionPerAjax(newCoolerMultimedia_Filter, { NewCoolerId: row.NewCoolerId }, false, callback, "GET", true);
                },
                'click .btnEvidences': function (e, value, row) {
                    var callback = function (response) {
                        if (response.Records.length > 0) {
                            setImagesToModal(response.Records);
                        } else {
                            alertInfo("Sin evidencias.");
                            clearOpacity();
                        }
                    };
                    addOpacity();
                    otherActionPerAjax(evidenceMultimedia_Filter, { CoolerConfigurationReplyId: row.Id }, false, callback, "GET", true);
                }
            }
        }, {
            field: 'ViewDetail',
            title: '',
            align: 'left',
            width: "20",
            sortable: false,
            valign: 'middle',
            formatter: function (value, row) {
                if (row.ApplyAssignedSurveyId > 0) {
                    return " <a class='btnViewDetail iconPointer' style='color:black;'><i class='fa fa-eye'></i></a>";
                }
                return " <a style='color:grey;'><i class='fa fa-eye'></i></a>";
            },
            events: window.operateEvents = {
                'click .btnViewDetail': function (e, value, row) {
                    if (row.ApplyAssignedSurveyId > 0) {
                        window.location.href = coolerConfigurationReply_Detail + "?applyAssignedSurveyId=" + row.ApplyAssignedSurveyId + "&customerId=" + row.CustomerId;
                    }
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
        CoolerId: "",
        SortBy: "ExecutionDate",
        Sort: "DESC",
        PageSize: 10,
        PageNumber: 1
    };
}
