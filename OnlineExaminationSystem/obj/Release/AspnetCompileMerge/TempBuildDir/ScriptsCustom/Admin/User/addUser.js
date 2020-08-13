"use strict";
function newUser() {
    $('#new-user-errors').html('');
    $('#txt-new-user-name').val('');
    $('#txt-new-user-surname').val('');
    $('#txt-new-user-student-no').val('');
    $('#txt-new-user-department').val('');
    $('#txt-new-user-username').val('');
    $('#txt-new-user-password').val('');
    $('#modal-new-user').modal('show');
}

function addUser() {
    $.ajax({
        type: 'POST',
        url: getActionUrl('User', 'CreateUser'),
        data: {
            Name: $('#txt-new-user-name').val(),
            Surname: $('#txt-new-user-surname').val(),
            StudentNumber: $('#txt-new-user-student-no').val(),
            Department: $('#txt-new-user-department').val(),
            UserName: $('#txt-new-user-username').val(),
            Password: $('#txt-new-user-password').val()
        },
        dataType: 'json',
        success: function (result) {
            var modelError = result.modelError;
            if (modelError !== '') {
                var errors = "";
                for (var i = 0; i < modelError.length; i++) {
                    errors += "* " + modelError[i][0].ErrorMessage + '<br/>';
                }
                $('#new-user-errors').html(errors);
                return;
            }
            result = result.result;
            $('#new-user-errors').html('');
            if (result == 1) {
                $('#modal-new-user').modal('hide');
                oTableUser.fnDraw();
            }
            else if (result == -1) {
                warningMessage('Girilen kullanıcı adı sistemde kayıtlıdır!');
            }
            else {
                warningMessage('Kullanıcı adı geçersiz!');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage('Hata Oluştu');
        }
    });
}