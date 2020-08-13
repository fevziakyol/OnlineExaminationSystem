"use strict";
function answerPaper() {
    $('#modal-answer-paper').modal('show');
}
function initTableAnswer(examId) {
    oTableAnswer = $('#table-answer').dataTable({
        "bServerSide": true,
        "sAjaxSource": getActionUrl("Test", "GetAnswers"),
        "bProcessing": true,
        "sServerMethod": "POST",
        "fnServerParams": function ( aoData ) {
            aoData.push( { "name": "examId", "value": examId });
        },
        "language": languageTR,
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': ['nosort']
        },
        {
            "bSearchable": false,
            "aTargets": ['nofilter']
        }],
        "paging":   false,
        "ordering": false,
        "info": false,
        "searching": false,
        "order": []
    });
}