"use strict";
function initTableAssignExam() {
    oTableAssignExam = $('#table-assign-exam').dataTable({
        "bServerSide": true,
        "sAjaxSource": getActionUrl("AssignExam", "GetAssignExams"),
        "bProcessing": true,
        "sServerMethod": "POST",
        "language": languageTR,
        "fnServerParams": function (aoData) {
            aoData.push({ "name": "examName", "value": $("#select-exam-type").val() });
            aoData.push({ "name": "userName", "value": $("#select-user-type").val() });
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
        "aLengthMenu": [50, 100, 200]
    });
}