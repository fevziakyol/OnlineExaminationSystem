"use strict";
function initTableQuestion() {
    oTableQuestion = $('#table-question').dataTable({
        "bServerSide": true,
        "sAjaxSource": getActionUrl("Question", "GetQuestions"),
        "bProcessing": true,
        "sServerMethod": "POST",
        "language": languageTR,
        "fnServerParams": function (aoData) {
            aoData.push({ "name": "lessonName", "value": $("#select-lesson-type").val() });
            aoData.push({ "name": "examName", "value": $("#select-exam-type").val() });
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