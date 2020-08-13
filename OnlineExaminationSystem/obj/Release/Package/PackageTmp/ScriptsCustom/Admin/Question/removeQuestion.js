"use strict";
function removeQuestion(Id, QuestionName) {
    jConfirm("Silmek istediğiniz soru: <b>" + QuestionName + "</b><br/>Emin misiniz?", "Uyarı!", function (r) {
        if (r == true) {
            //            startSpinner();

            $.ajax({
                type: "POST",
                url: getActionUrl("Question", "DeleteQuestion"),
                data: { id: Id },
                success: function (result) {
                    if (result == 1) {
                        //                        stopSpinner();
                        oTableQuestion.fnDraw();
                    }
                    else if (result == 0) {
                        warningMessage("Soru silinemez!");
                        //                        stopSpinner();
                    }
                    else {
                        warningMessage("İşlem Başarısız!");
                        //                        stopSpinner();
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    errorMessage("Hata Oluştu");
                    //                    stopSpinner();
                }
            });
        }
    });
}