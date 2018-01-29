﻿var tabsProcess = {
    Carga: {
        tags: ["ProcessId", "StartProcess", "EndProcess", "Percent"],
        details: [["summary", '<i class="icon-list"></i>', 'ProcessId'], ["errorLog", '<i class="icon-exclamation-sign"></i>', "ProcessId"]],
        tableId: "#cargaGrid",
        type: 2
    },
    Descarga: {
        tags: ["ProcessId", "StartProcess", "EndProcess", "Percent"],
        details: [["summary", '<i class="icon-list"></i>', 'ProcessId'], ["errorLog", '<i class="icon-exclamation-sign"></i>', "ProcessId"]],
        tableId: "#descargaGrid",
        type:3
    }
};

﻿var logs = {
﻿    summary: {
﻿        tags:["TableName", "ToProcess", "Process", "IsWarning", "BranchCode"],
﻿        details:[],
﻿        tableId: "#summaryGrid",
﻿        modal: "#summaryModal",
﻿        url: "/Procesos/GetSummary?processId="
﻿    },
﻿    
    errorLog: {
        tags:["Description", "BranchCode"],
        details:[],
        tableId: "#logErrorGrid",
        modal: "#logErrorModal",
        url: "/Procesos/GetLogError?processId="
    }
﻿};
﻿
function OpenLogs(e)
{
    e.preventDefault();
    var link = $(this).attr('href');
    var split = link.split('/');
    GetLog(split[1], logs[split[0]]);
}


function Initialize() {
    GetProcesses(tabsProcess.Carga);
    $('#tabconfig a').click(ChangeTab);
    $('table').on('click', 'tr a.picLinks', OpenLogs);
}

function GetProcesses(tab) {
    var branchId = $('#ComboBranchId').val();
    
    if (branchId > 0) {
        var url = "/Procesos/GetProcesses" + "?branchId=" + branchId + "&type=" + tab.type;
        GetProcessesTable(url, tab);
    }
}

function GetLog(processId, tab) {
    
    if (processId > 0) {
        GetLogTable(tab.url + processId, tab);
    }
}

function GetLogTable(url, tab) {
    $.ajax({
        url: url,
        success: function (data) {
            var response = JSON.parse(data.Data);            
            if(tab.tableId == logs.summary.tableId) {
                $.each(response.Data, function(index, summary) {
                    summary.IsWarning = (summary.IsWarning) ? '<i class="icon-warning-sign"></i>' : '';
                });
            }            
            PrintTable(tab.tags, response.Data, tab.tableId, tab.details);
        }, error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == 404)
                window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
        }
    });
    
    $(tab.modal).modal().show();
}

function GetProcessesTable(url, config) {
    $.ajax({
        url: url,
        success: function (data) {
            var response = JSON.parse(data.Data);
            PrintTable(config.tags, response.Data, config.tableId, config.details);
        }, error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == 404)
                window.location = '<%:ResolveClientUrl("~/Usuario/Index")%>';
        }
    });
}

function SelectBranch() {
    var tabName = $("ul#tabconfig li.active a").html();
    GetProcesses(tabsProcess[tabName]);
}

function ChangeTab(e) {
    e.preventDefault();
    var tabName = $(this).html();
    
    $(this).tab('show');
    GetProcesses(tabsProcess[tabName]);
}
