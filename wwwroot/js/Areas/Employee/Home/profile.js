$(function () {
    const submit = $('#btnSubmit');

    $(submit).on('click', function () {
        const oldPassword = $('#oldPassword').val();
        const newPassword = $('#Password').val();

        let data = { OldPassword: oldPassword, NewPassword: newPassword };
        $.ajax({
            method: 'POST',
            url: '/Employee/Home/ChangePassword',
            data: data,
            success: function (result) {
                alert('Password changed successfully!');
            },
            error: function (error) {
                alert(error.responseText);
            }
        });
    });
});