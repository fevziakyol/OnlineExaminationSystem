"use strict";
function removeLesson(ID, LessonName) {
    jConfirm("Silmek istediğiniz ders: <b>" + LessonName + "</b><br/>Emin misiniz?", "Uyarı!", function (r) {
        if (r == true) {
            //            startSpinner();

            $.ajax({
                type: "POST",
                url: getActionUrl("Lesson", "DeleteLesson"),
                data: { id: ID },
                success: function (result) {
                    if (result == 1) {
                        //                        stopSpinner();
                        oTableLesson.fnDraw();
                    }
                    else if (result == 0) {
                        warningMessage("Ders silinemez!");
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