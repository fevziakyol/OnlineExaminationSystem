function previousQuestion(questionId, userAnswer, examId, questionNumber) {
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
                //location.href = "/test/index/" + examId + "?questionNumber=" + --questionNumber;
                return;
            }
            stopSpinner();
            location.href = "/test/index/" + examId + "?questionNumber=" + --questionNumber;

        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            stopSpinner();
        }
    });
}