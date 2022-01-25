$(function () {

    const edit = $('#submitBtn');
    const antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();

    //Toast
    const toastMsg = $('#toastMsg');
    const toastClose = $('#toastBody button[data-bs-dismiss="toast"]');
    $(toastClose).on('click', function () {
        $(toastMsg).hide();
    });
    const toastConfirm = $('#toastBody button.btn-outline-primary');
    $(toastConfirm).on('click', function () {
        let employee = {
            Id: $('input[name="Id"]').val(),
            UserId: $('input[name="UserId"]').val(),
            DepartmentId: $('select[name="DepartmentId"]').find(':selected').val(),
            PositionId: $('input[name="PositionId"]').val(),
            Email: $('input[name="Email"]').val(),
            Name: $('input[name="Name"]').val(),
            WorkStartDate: $('input[name="WorkStartDate"]').val(),
        }

        $.ajax({
            beforeSend: function (jqXHR) {
                jqXHR.setRequestHeader('X-AFTAsseco', antiForgeryToken);
            },
            method: 'POST',
            url: '/Admin/Employee/Edit',
            data: { id: employee.Id ,employee: employee },
            success: function (result) {
                console.log(result);
            },
            error: function (error) {
                alert(error.responseText);
            }
        });
    });

    $(edit).on('click', function () {
        $(toastMsg).show();
    });
});