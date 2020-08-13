"use strict";
function initTableUser() {
    oTableUser = $('#table-user').dataTable({
        "bServerSide": true,
        "sAjaxSource": getActionUrl("User", "GetUsers"),
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