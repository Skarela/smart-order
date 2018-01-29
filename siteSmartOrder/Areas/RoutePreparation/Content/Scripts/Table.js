var table = (function () {
    var tableId = "#Table";
    var uriFilter = "";
    var uriDelete = "";
    var uriEdit = "";
    var columnsToAdd = [];
    var filtersToAdd = [];
    var showColumns = false;
    var cardView = false;
    var showExport = false;
    var clickToSelect = false;

    function privateSetUri(uriToFilter) {
        tableId = tableId;
        columnsToAdd = [];
        filtersToAdd = [];
        showColumns = false;
        cardView = false;
        showExport = false;
        uriFilter = uriToFilter;
    }

    function privateSetTableId(id) {
        tableId = id;
    }

    function privateSetFilters(filters) {
        filtersToAdd = filters;
    }

    function privateSetColumns(columns, withCheckColumn, uriToEdit, uriToDelete) {
        var thisTableId = tableId;
        var formatter = "";
        var operateEvents = [];
        var width = "90";

        if (withCheckColumn) {
            clickToSelect = true;
            columnsToAdd.push({
                field: 'State',
                checkbox: true,
                valign: 'middle',
                align: 'center'
            });
        }
        if (columns) {
            $.each(columns, function(index, object) {
                columnsToAdd.push(object);
            });
        }
        
        if (uriToEdit) {
            uriEdit = uriToEdit;
            formatter += " <a class='btnEdit iconPointer' style='color:black;'><i class='fa fa-pencil'></i></a>";

            operateEvents['click .btnEdit'] = function (e, value, row) {
                window.location.href = uriEdit + "/" + row.Id;
            };
        }

        if (uriToDelete) {
            uriDelete = uriToDelete;
            formatter += " <a class='btnDelete iconPointer padding-left-5' style='color:black;'><i class='fa fa-trash-o'></i></a>";
            operateEvents['click .btnDelete'] = function (e, value, row) {
                BootstrapDialog.show({
                    title: "Eliminar",
                    message: "¿Deseas eliminar el registro?",
                    type: BootstrapDialog.TYPE_DANGER,
                    buttons: [
                        {
                            label: 'Eliminar',
                            cssClass: 'btn-danger',
                            action: function(dialog) {
                                dialog.enableButtons(false);
                                dialog.setClosable(false);
                                this.spin();

                                $.ajax({
                                    url: uriDelete,
                                    type: "DELETE",
                                    async: true,
                                    data: { id: row.Id },
                                    success: function(response) {
                                        checkResponseToAjax(response);
                                        $(thisTableId).bootstrapTable('refresh');

                                        if (response.Result == resultOk)
                                            alertSuccess(response.Message);
                                        dialog.close();
                                    },
                                    error: function() {
                                        alertError("No se ha podido establecer conexión con el servicio.");
                                        dialog.close();
                                    }
                                });
                            }
                        }, {
                            label: 'Cancelar',
                            action: function(dialog) {
                                dialog.close();
                            }
                        }
                    ]
                });
            };
        }

        if (formatter != "") {
            columnsToAdd.push({
                field: 'Buttons',
                align: 'center',
                width: width,
                clickToSelect: false,
                formatter: function() { return formatter; },
                events: window.operateEvents = operateEvents
            });
        }
    }

    function privateShowButtonColumns() {
        showColumns = true;
    }

    function privateChangeToCardView() {
        cardView = true;
    }

    function privateShowEButtonxport() {
        showExport = true;
    }

    function privateCreate(data, callback, dataCallback) {

        var thisTableId = tableId;
        var thisToolbarId = thisTableId + "Toolbar";
        var thisUriFilter = uriFilter;
        var thisUriDelete = uriDelete;
        var thisColumnsToAdd = columnsToAdd;
        var thisFiltersToAdd = filtersToAdd;
        var thisShowColumns = showColumns;
        var thisCardView = cardView;
        var thisShowExport = showExport;
        var thisQuery = data;
        var thisClickToSelect = clickToSelect;

        $(thisTableId).bootstrapTable({
            method: 'Get',
            url: thisUriFilter,
            showColumns: thisShowColumns,
            showExport: thisShowExport,
            cardView: thisCardView,
            //showToggle: true,
            cache: false,
            striped: true,
            exportTypes: ['excel'],
            clickToSelect: thisClickToSelect,
            pagination: true,
            pageSize: 10,
            pageList: [10, 25, 50, 100, 200],
            iconSize: "sm",
            sidePagination: "server",
            minimumCountColumns: 2,
            toolbar: thisToolbarId,
            queryParams: function(params) {
                addOpacity();
                if (markers.length > 0) {
                    clearMarkers();
                    clearInfoBubbles();
                }

                if (dataCallback) dataCallback(thisQuery);
                thisQuery.StartPage = thisQuery.StartPage != 0 ? params.offset + 1 : 0;
                thisQuery.EndPage = thisQuery.EndPage != 0 ? params.limit + params.offset : 0;
                thisQuery.SortBy = params.sort != undefined ? params.sort : thisQuery.SortBy;
                thisQuery.Sort = thisQuery.Sort.toUpperCase();
                return thisQuery;
            },
            responseHandler: function(res) {
                if (res.Result == resultOk) {
                    $(thisTableId).parent().parent().show();
                    $(thisTableId).parent().addClass("table-responsive");
                    toggleDeleteMany();
                    clearOpacity();
                    return { rows: res.Records, total: res.Count };
                }

                disabledToolButtons();
                $(thisTableId + "Loading").html("<i class='fa fa-warning'></i> No se pudieron cargar los registros...</span> ");
                alertError(res.Message);
                return null;
            },
            columns: thisColumnsToAdd
        }).on('check.bs.table', function() {
            toggleDeleteMany();
        }).on('uncheck.bs.table', function() {
            toggleDeleteMany();
        }).on('check-all.bs.table', function() {
            toggleDeleteMany();
        }).on('uncheck-all.bs.table', function() {
            toggleDeleteMany();
        }).on('page-change.bs.table', function() {
            toggleDeleteMany();
            if (callback)
                callback();
        }).on('load-success.bs.table', function() {
            $(thisTableId + "Loading").hide();
            $("#Edit,#Delete").tooltip();
            $(".page-list").tooltip();

            if (callback)
                callback();
        }).on('sort.bs.table', function() {
            if (callback)
                callback();
        }).on('load-error.bs.table', function() {
            toggleDeleteMany();
        });

        $(".fixed-table-container").hide();
        $(thisTableId + "LoadingText").html(" Cargando registros...");
        $(thisTableId + "Toolbar").show();

        $(thisTableId + "DeleteMany").click(function() {
            BootstrapDialog.show({
                title: "Eliminar",
                message: "<p>¿Deseas eliminar los registros?</p>",
                type: BootstrapDialog.TYPE_DANGER,
                buttons: [
                    {
                        label: 'Eliminar',
                        cssClass: 'btn-danger',
                        action: function(dialog) {
                            var countSuccess = 0;
                            var countError = 0;
                            this.spin();

                            var selectedRows = $(thisTableId).bootstrapTable('getSelections');
                            $.each(selectedRows, function(index, value) {
                                $.ajax({
                                    url: thisUriDelete,
                                    type: "DELETE",
                                    async: false,
                                    data: { id: value.Id },
                                    success: function(result) {
                                        if (result.Result == resultOk) {
                                            $(thisTableId).bootstrapTable('remove', {
                                                field: 'Id',
                                                values: [value.Id]
                                            });
                                            countSuccess++;
                                        } else {
                                            countError++;
                                        }
                                    },
                                    error: function() {
                                        countError++;
                                    }
                                });
                            });

                            if (countSuccess > 0)
                                alertSuccess(countSuccess + " registros se han eliminado exitosamente.");
                            if (countError > 0)
                                alertError(countError + " registros no se han podido eliminar.");
                            toggleDeleteMany();
                            $(thisTableId).bootstrapTable('refresh');
                            dialog.close();
                        }
                    }, {
                        label: 'Cancelar',
                        action: function(dialog) {
                            dialog.close();
                        }
                    }
                ]
            });
        });

        var toggleDeleteMany = function() {
            numberRowSelecteds() > 1 ? $(thisTableId + "DeleteMany").prop("disabled", false) : $(thisTableId + "DeleteMany").prop("disabled", true);

            if ($(thisTableId + "InactivateMany")[0] != undefined)
                numberRowSelecteds() > 1 ? $(thisTableId + "InactivateMany").prop("disabled", false) : $(thisTableId + "InactivateMany").prop("disabled", true);

            if ($(thisTableId + "ActivateMany")[0] != undefined)
                numberRowSelecteds() > 1 ? $(thisTableId + "ActivateMany").prop("disabled", false) : $(thisTableId + "ActivateMany").prop("disabled", true);

        };

        var numberRowSelecteds = function() {
            return $(thisTableId).bootstrapTable('getSelections').length;
        };

        var disabledToolButtons = function() {
            $(".export").children().prop("disabled", true);
            $("[title=Columns]").children().prop("disabled", true);
        };


        $.each(thisFiltersToAdd, function(filter, input) {
            $("#" + input).change('input', function() {
                thisQuery[filter] = "" + $("#" + input).val() + "";
                $(thisTableId).bootstrapTable('refresh');
            });
        });

        $("#FromCreationDate").change('input', function () {
            if ($(this).val() != "" && $("#ToCreationDate").val() != "") {
                thisQuery["FromCreationDate"] = $("#FromCreationDate").val();
                $(thisTableId).bootstrapTable('refresh');
            } else if ($(this).val() == "" && thisQuery["ToCreationDate"] != "" && thisQuery["FromCreationDate"] != "") {
                thisQuery["FromCreationDate"] = "";
                $(thisTableId).bootstrapTable('refresh');
            }
        });

        $("#ToCreationDate").change('input', function () {
            if ($(this).val() != "" && $("#FromCreationDate").val() != "") {
                thisQuery["ToCreationDate"] = "" + $("#ToCreationDate").val() + "";
                $(thisTableId).bootstrapTable('refresh');
            } else if ($(this).val() == "" && thisQuery["ToCreationDate"] != "" && thisQuery["FromCreationDate"] != "") {
                thisQuery["ToCreationDate"] = "";
                $(thisTableId).bootstrapTable('refresh');
            }
        });


        var interval;

        $("#UnitCode").on('input', function () {
            clearInterval(interval);
            interval = setInterval(function() {
                clearInterval(interval);
                thisQuery["UnitCode"] = $("#UnitCode").val();
                $(thisTableId).bootstrapTable('refresh');
            }, 1000);
        });

        $("#Code").on('input', function() {
            clearInterval(interval);
            interval = setInterval(function() {
                clearInterval(interval);
                thisQuery["Code"] = $("#Code").val();
                $(thisTableId).bootstrapTable('refresh');
            }, 1000);
        });

        $("#Campaign").on('input', function() {
            clearInterval(interval);
            interval = setInterval(function() {
                clearInterval(interval);
                thisQuery["Name"] = $("#Campaign").val();
                $(thisTableId).bootstrapTable('refresh');
            }, 1000);
        });

        $("#Serie").on('input', function() {
            clearInterval(interval);
            interval = setInterval(function() {
                clearInterval(interval);
                thisQuery["Serie"] = $("#Serie").val();
                $(thisTableId).bootstrapTable('refresh');
            }, 1000);
        });

        $("#Name").on('input', function () {
            clearInterval(interval);
            interval = setInterval(function () {
                clearInterval(interval);
                thisQuery["Name"] = $("#Name").val();
                $(thisTableId).bootstrapTable('refresh');
            }, 1000);
        });

        $("#Email").on('input', function () {
            clearInterval(interval);
            interval = setInterval(function () {
                clearInterval(interval);
                thisQuery["Email"] = $("#Email").val();
                $(thisTableId).bootstrapTable('refresh');
            }, 1000);
        });

        $("#Company").on('input', function () {
            clearInterval(interval);
            interval = setInterval(function () {
                clearInterval(interval);
                thisQuery["Company"] = $("#Company").val();
                $(thisTableId).bootstrapTable('refresh');
            }, 1000);
        });
    }

    function privateRefresh() {
        var thisTableId = tableId;
        $(thisTableId).bootstrapTable('refresh');
    }

    return {
        setUri: privateSetUri,
        setTableId: privateSetTableId,
        setFilters: privateSetFilters,
        setColumns: privateSetColumns,
        showButtonColumns: privateShowButtonColumns,
        showButtonExport: privateShowEButtonxport,
        changeToCardView: privateChangeToCardView,
        create: privateCreate,
        refresh: privateRefresh
    };
})();

