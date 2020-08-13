"use strict";
function newAssignExam() {
    $('#new-assign-exam-errors').html('');
    $('#select-new-userType').val('');
    $('#select-new-examType').val('');
    $('#select-new-assign-state').val('');
    $('#modal-new-assign-exam').modal('show');
}
function addAssignExam() {
    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('AssignExam', 'CreateAssignExam'),
        data: {
            UserId: $("#select-new-userType").val(),
            ExamId: $("#select-new-examType").val(),
            IsActiv: $('#select-new-assign-state').val()
        },
        dataType: 'json',
        success: function (result) {
            var modelError = result.modelError;
            if (modelError !== '') {
                var errors = "";
                for (var i = 0; i < modelError.length; i++) {
                    for (var j = 0; j < modelError[i].length; j++) {
                        errors += "* " + modelError[i][j].ErrorMessage + '<br/>';
                    }
                }
                $('#new-assign-exam-errors').html(errors);
                stopSpinner();
                return;
            }
            stopSpinner();
            $('#new-assign-exam-errors').html('');
            $('#modal-new-assign-exam').modal('hide');
            oTableAssignExam.fnDraw();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            stopSpinner();
        }
    });
}