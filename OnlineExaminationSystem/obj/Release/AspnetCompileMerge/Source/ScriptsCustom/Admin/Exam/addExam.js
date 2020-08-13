"use strict";
function newExam() {
    $('#new-exam-errors').html('');
    $('#select-new-lessonType').val('');
    $('#txt-new-exam-name').val('');
    $('#txt-new-exam-lecture-name').val('');
    $('#txt-new-exam-question-number').val('');
    $('#txt-new-exam-time').val('');
    $('#modal-new-exam').modal('show');
}
function addExam() {
    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('Exam', 'CreateExam'),
        data: {
            LessonId: $("#select-new-lessonType").val(),
            ExamName: $('#txt-new-exam-name').val(),
            ExamLectureName: $('#txt-new-exam-lecture-name').val(),
            ExamQuestionNumber: $('#txt-new-exam-question-number').val(),
            ExamTime: $('#txt-new-exam-time').val()
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
                $('#new-exam-errors').html(errors);
                stopSpinner();
                return;
            }
            stopSpinner();
            $('#new-exam-errors').html('');
            $('#modal-new-exam').modal('hide');
            oTableExam.fnDraw();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            stopSpinner();
        }
    });
}