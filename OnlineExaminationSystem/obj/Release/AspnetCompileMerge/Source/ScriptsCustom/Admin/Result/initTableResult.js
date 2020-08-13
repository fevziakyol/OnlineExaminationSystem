"use strict";
function initTableResult() {
    oTableResult = $('#table-result').dataTable({
        "bServerSide": true,
        "sAjaxSource": getActionUrl("Result", "GetResults"),
        "bProcessing": true,
        "sServerMethod": "POST",
        "language": languageTR,
        "fnServerParams": function (aoData) {
            aoData.push({ "name": "examId", "value": $("#select-exam-type").val() });
        },
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': ['nosort']
        },
        {
            "bSearchable": false,
            "aTargets": ['nofilter']
        }],
        "order": [[0, "desc"]],
        "aLengthMenu": [10, 25, 50]
    });
}