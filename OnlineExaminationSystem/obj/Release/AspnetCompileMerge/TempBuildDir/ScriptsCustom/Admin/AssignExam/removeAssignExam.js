"use strict";
function removeAssignExam(Id, UserName, ExamName) {
    jConfirm("Silmek istediğiniz sınav ataması: <br/><b>" + UserName + " </b>için<b> " + ExamName + "</b><br/>Emin misiniz?", "Uyarı!", function (r) {
        if (r == true) {

            $.ajax({
                type: "POST",
                url: getActionUrl("AssignExam", "DeleteAssignExam"),
                data: { id: Id },
                success: function (result) {
                    if (result == 1) {
                        oTableAssignExam.fnDraw();
                    }
                    else if (result == 0) {
                        warningMessage("Atama silinemez!");
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