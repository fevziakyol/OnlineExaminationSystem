"use strict";
function initTableLesson() {
    oTableLesson = $('#table-lesson').dataTable({
        "bServerSide": true,
        "sAjaxSource": getActionUrl("Lesson", "GetLessons"),
        "bProcessing": true,
        "sServerMethod": "POST",
        "language": languageTR,
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