"use strict";
function finishExam(examId) {
    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('Test', 'FinishAssignExam'),
        data: {
            ExamId: examId
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
                stopSpinner();
                return;
            }
            stopSpinner();
            window.location.href = window.location.origin + "/UserHome/index";
        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            stopSpinner();
        }
    });
}
function exitExam(questionId, userAnswer, examId, questionNumber) {
    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('Test', 'SetAnswer'),
        data: {
            QuestionId: questionId,
            UserAnswer: userAnswer
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
                stopSpinner();
                warningMessage('Soru üzerinde işaretleme yapmadınız. <br/>Lütfen cevap veriniz.');
                return;
            }
            stopSpinner();
            $('#modal-finish-exam').modal('show');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            stopSpinner();
        }
    });
}