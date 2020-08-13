//spinner control...
var target = document.getElementById('spinnerID');
var spinner = new Spinner();
function startSpinner() {
    $("#spinnerBackground").fadeIn(100);
    spinner.spin(target);
}
function stopSpinner() {
    spinner.stop();
    $("#spinnerBackground").fadeOut(0);
}