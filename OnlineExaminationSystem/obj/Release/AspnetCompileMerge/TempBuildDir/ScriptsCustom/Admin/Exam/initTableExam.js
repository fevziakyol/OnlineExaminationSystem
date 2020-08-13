"use strict";
function initTableExam() {
    oTableExam = $('#table-exam').dataTable({
        "bServerSide": true,
        "sAjaxSource": getActionUrl("Exam", "GetExams"),
        "bProcessing": true,
        "sServerMethod": "POST",
        "language": languageTR,
        "fnServerParams": function (aoData) {
            aoData.push({ "name": "lessonName", "value": $("#select-lesson-type").val() });
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