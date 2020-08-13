"use strict";
function newLesson() {
    $('#new-lesson-errors').html('');
    $('#txt-new-lesson-name').val('');
    $('#modal-new-lesson').modal('show');
}

function addLesson() {
    //    startSpinner();

    $.ajax({
        type: 'POST',
        url: getActionUrl('Lesson', 'CreateLesson'),
        data: {
            LessonName: $('#txt-new-lesson-name').val(),
        },
        dataType: 'json',
        success: function (result) {
            var modelError = result.modelError;
            if (modelError !== '') {
                var errors = "";
                for (var i = 0; i < modelError.length; i++) {
                    errors += "* " + modelError[i][0].ErrorMessage + '<br/>';
                }
                $('#new-lesson-errors').html(errors);
                //                stopSpinner();
                return;
            }
            result = result.result;
            $('#new-lesson-errors').html('');
            if (result == 1) {
                //                stopSpinner();
                $('#modal-new-lesson').modal('hide');
                oTableLesson.fnDraw();
            }
            else if (result == -1) {
                warningMessage('Girilen ders adı sistemde kayıtlıdır!');
                //                stopSpinner();
            }
            else {
                warningMessage('Ders adı geçersiz!');
                //                stopSpinner();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            //            stopSpinner();
        }
    });
}