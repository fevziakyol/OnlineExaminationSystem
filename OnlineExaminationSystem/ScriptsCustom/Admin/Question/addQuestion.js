"use strict";
function newQuestion() {
    $('#new-question-errors').html('');
    $('#select-new-lessonType').val('');
    $('#select-new-examType').val('');
    $('#txt-new-question-text').val('');
    $('#txt-new-question-choice-a').val('');
    $('#txt-new-question-choice-b').val('');
    $('#txt-new-question-choice-c').val('');
    $('#txt-new-question-choice-d').val('');
    $('#select-new-questionType').val('');
    $('#modal-new-question').modal('show');
}
function addQuestion() {
    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('Question', 'CreateQuestion'),
        data: {
            LessonId: $("#select-new-lessonType").val(),
            ExamId: $("#select-new-examType").val(),
            QuestionText: $('#txt-new-question-text').val(),
            ChoiceA: $('#txt-new-question-choice-a').val(),
            ChoiceB: $('#txt-new-question-choice-b').val(),
            ChoiceC: $('#txt-new-question-choice-c').val(),
            ChoiceD: $('#txt-new-question-choice-d').val(),
            Answer: $('#select-new-questionType').val()
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
                $('#new-question-errors').html(errors);
                stopSpinner();
                return;
            }
            stopSpinner();
            $('#new-question-errors').html('');
            $('#modal-new-question').modal('hide');
            oTableQuestion.fnDraw();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            stopSpinner();
        }
    });
}