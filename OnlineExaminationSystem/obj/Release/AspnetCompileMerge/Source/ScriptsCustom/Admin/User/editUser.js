"use strict";
function editUser(id, name, surname, studentNumber, department, username, password) {
    IdToEdit = id;
    passwordTemp = password;
    $('#txt-edit-user-name').val(name);
    $('#txt-edit-user-surname').val(surname);
    $('#txt-edit-user-student-no').val(studentNumber);
    $('#txt-edit-user-department').val(department);
    $('#txt-edit-user-username').val(username);
    $('#txt-edit-user-password').val("");
    $('#modal-edit-user').modal('show');
}

function updateUser() {
//    startSpinner();
    $.ajax({
        type: 'POST',
        url: getActionUrl('User', 'UpdateUser'),
        data: {
            ID: IdToEdit,
            Name: $('#txt-edit-user-name').val(),
            Surname: $('#txt-edit-user-surname').val(),
            StudentNumber: $('#txt-edit-user-student-no').val(),
            Department: $('#txt-edit-user-department').val(),
            UserName: $('#txt-edit-user-username').val(),
            Password: passwordTemp
        },
        dataType: 'json',
        success: function (result) {
            var modelError = result.modelError;
            if (modelError !== '') {
                var errors = "";
                for (var i = 0; i < modelError.length; i++) {
                    errors += "* " + modelError[i][0].ErrorMessage + '<br/>';
                }
                $('#edit-user-errors').html(errors);
                return;
            }
            $('#edit-user-errors').html('');
            result = result.result;
            if (result == 1) {
                $('#modal-edit-user').modal('hide');
                oTableUser.fnDraw();
            }
            else if (result == -1) {
                warningMessage('Girilen kullanıcı sistemde kayıtlıdır!');
            }
            else {
                warningMessage('Kullanıcı adı geçeriz!');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
        }
    });
}