$(function () {

    const edit = $('#submitBtn');
    const antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();

    const resetAvailDaysBtn = $('#resetBtn');

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
            PositionId: $('select[name="PositionId"]').find(':selected').val(),
            Email: $('input[name="Email"]').val(),
            Name: $('input[name="Name"]').val(),
            WorkStartDate: $('input[name="WorkStartDate"]').val(),
        }

        let availableDays = new Object();
        const availableDaysInputs = $('input.roundIconValue');
        for (var i = 0; i < availableDaysInputs.length; i++) {
            availableDays[$(availableDaysInputs[i]).attr("data-request-type")] = $(availableDaysInputs[i]).val();
        }

        console.log(availableDays);
        $.ajax({
            beforeSend: function (jqXHR) {
                jqXHR.setRequestHeader('X-AFTAsseco', antiForgeryToken);
            },
            method: 'POST',
            url: '/Admin/Employee/Edit',
            data: { id: employee.Id ,employee: employee, availableDays: availableDays },
            success: function (result) {
                alert(result);
            },
            error: function (error) {
                alert(error.responseText);
            }
        });
    });

    $(edit).on('click', function () {
        $(toastMsg).show();
    });

    $(resetAvailDaysBtn).on('click', function () {
        if ($(this).attr('data-employee-id') != $('input[name="Id"]').val())
            return;

        $.ajax({
            beforeSend: function (jqXHR) {
                jqXHR.setRequestHeader('X-AFTAsseco', antiForgeryToken);
            },
            method: 'PUT',
            url: '/Admin/Employee/Reset',
            data: { id: $(this).attr('data-employee-id') },
            success: function (result) {
                alert(result);
            },
            error: function (error) {
                alert(error.responseText);
            }
        });
    });
});