"use strict";
function editExam(id, LessonId, ExamName, ExamLectureName, ExamQuestionNumber, ExamTime) {
    IdToEdit = id;
    $('#edit-exam-errors').html('');
    $('#select-edit-lessonType').val(LessonId);
    $('#txt-edit-exam-name').val(ExamName);
    $('#txt-edit-exam-lecture-name').val(ExamLectureName);
    $('#txt-edit-exam-question-number').val(ExamQuestionNumber);
    $('#txt-edit-exam-time').val(ExamTime);
    $('#modal-edit-exam').modal('show');
}
function updateExam() {
    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('Exam', 'UpdateExam'),
        data: {
            id: IdToEdit,
            LessonId: $("#select-edit-lessonType").val(),
            ExamName: $('#txt-edit-exam-name').val(),
            ExamLectureName: $('#txt-edit-exam-lecture-name').val(),
            ExamQuestionNumber: $('#txt-edit-exam-question-number').val(),
            ExamTime: $('#txt-edit-exam-time').val()
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
                $('#edit-exam-errors').html(errors);
                stopSpinner();
                return;
            }
            stopSpinner();
            $('#edit-exam-errors').html('');
            $('#modal-edit-exam').modal('hide');
            oTableExam.fnDraw();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            stopSpinner();
        }
    });
}