"use strict";
function editQuestion(id, LessonId, ExamId, QuestionText, ChoiceA, ChoiceB, ChoiceC, ChoiceD, Answer) {
    IdToEdit = id;
    $('#edit-question-errors').html('');
    $('#select-edit-lessonType').val(LessonId);
    $('#select-edit-examType').val(ExamId);
    $('#txt-edit-question-text').val(QuestionText);
    $('#txt-edit-question-choice-a').val(ChoiceA);
    $('#txt-edit-question-choice-b').val(ChoiceB);
    $('#txt-edit-question-choice-c').val(ChoiceC);
    $('#txt-edit-question-choice-d').val(ChoiceD);
    $('#select-edit-questionType').val(Answer);
    $('#modal-edit-question').modal('show');
}
function updateQuestion() {
    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('Question', 'UpdateQuestion'),
        data: {
            id: IdToEdit,
            LessonId: $("#select-edit-lessonType").val(),
            ExamId: $("#select-edit-examType").val(),
            QuestionText: $('#txt-edit-question-text').val(),
            ChoiceA: $('#txt-edit-question-choice-a').val(),
            ChoiceB: $('#txt-edit-question-choice-b').val(),
            ChoiceC: $('#txt-edit-question-choice-c').val(),
            ChoiceD: $('#txt-edit-question-choice-d').val(),
            Answer: $('#select-edit-questionType').val()
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
                $('#edit-question-errors').html(errors);
                stopSpinner();
                return;
            }
            stopSpinner();
            $('#edit-question-errors').html('');
            $('#modal-edit-question').modal('hide');
            oTableQuestion.fnDraw();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            stopSpinner();
        }
    });
}