"use strict";
function removeUser(ID, userName) {
    jConfirm("Silmek istediğiniz kullanıcı: <b>" + userName + "</b><br/>Emin misiniz?", "Uyarı!", function (r) {
        if (r == true) {
//            startSpinner();

            $.ajax({
                type: "POST",
                url: getActionUrl("User", "DeleteUser"),
                data: { id: ID },
                success: function (result) {
                    if (result == 1) {
//                        stopSpinner();
                        oTableUser.fnDraw();
                    }
                    else if (result == 0) {
                        warningMessage("Kullanıcı silinemez!");
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