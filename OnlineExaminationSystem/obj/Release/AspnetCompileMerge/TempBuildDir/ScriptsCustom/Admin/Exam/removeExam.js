"use strict";
function removeExam(Id, ExamName) {
    jConfirm("Silmek istediğiniz sınav: <b>" + ExamName + "</b><br/>Emin misiniz?", "Uyarı!", function (r) {
        if (r == true) {
            $.ajax({
                type: "POST",
                url: getActionUrl("Exam", "DeleteExam"),
                data: { id: Id },
                success: function (result) {
                    if (result == 1) {
                        oTableExam.fnDraw();
                    }
                    else if (result == 0) {
                        warningMessage("Sınav silinemez!");
                    }
                    else {
                        warningMessage("İşlem Başarısız!");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    errorMessage("Hata Oluştu");
                }
            });
        }
    });
}