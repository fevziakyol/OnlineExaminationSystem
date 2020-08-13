"use strict";
function errorMessage(msg) {
    jAlert("<font style='color:red'>" + msg + "</font>", "Hata!");
}

function warningMessage(msg) {
    jAlert("<font style='color:orange'>" + msg + "</font>", "Uyarı!");
}

function infoMessage(msg) {
    jAlert("<font style='color:green'>" + msg + "</font>", "Bilgi!");
}
