"use strict";
function editLesson(id, LessonName) {
    IdToEdit = id;
    $('#edit-lesson-errors').html('');
    $('#txt-edit-lesson-name').val(LessonName);
    $('#modal-edit-lesson').modal('show');
}
function updateLesson() {
    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('Lesson', 'UpdateLesson'),
        data: {
            id: IdToEdit,
            LessonName: $("#txt-edit-lesson-name").val()
        },
        dataType: 'json',
        success: function (result) {
            stopSpinner();
            if (result.result === 1) {
                oTableLesson.fnDraw();
                $("#modal-edit-lesson").modal("hide");
                return;
            }
            warningMessage("İşlem Başarısız!");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
            stopSpinner();
        }
    });
}