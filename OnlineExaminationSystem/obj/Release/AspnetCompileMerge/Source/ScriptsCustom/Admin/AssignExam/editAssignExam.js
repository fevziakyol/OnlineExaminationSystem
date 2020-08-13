"use strict";
function editAssignExam(id, UserID, ExamId, IsActiv) {
    IdToEdit = id;
    $('#edit-assign-exam-errors').html('');
    $('#select-edit-userType').val(UserID);
    $('#select-edit-examType').val(ExamId);
    $('#select-edit-assign-state').val(IsActiv);
    $('#modal-edit-assign-exam').modal('show');
}
function updateAssignExam() {
    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('AssignExam', 'UpdateAssignExam'),
        data: {
            id: IdToEdit,
            UserID: $("#select-edit-userType").val(),
            ExamId: $("#select-edit-examType").val(),
            IsActiv: $("#select-edit-assign-state").val()
        },
        dataType: 'json',
        success: function (result) {
            stopSpinner();
            if (result.result === 1) {
                oTableAssignExam.fnDraw();
                $("#modal-edit-assign-exam").modal("hide");
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