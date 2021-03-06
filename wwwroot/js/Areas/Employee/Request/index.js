$(function () {
    $('.datepicker').datepicker({
        format: 'yyyy/mm/dd',
        startDate: '0d',
        clearBtn: true,
        daysOfWeekDisabled: '0,6',
        multidate: true,
        todayHighlight: true,
        daysOfWeekHighlighted: '0,6',
        language: 'ES'
    });

    //Toast
    const toastMsg = $('#toastMsg');
    const toastBody = $('#toastBody');
    const toastClose = $('#toastMsg button.btn-close');

    $(toastClose).on('click', function () {
        $(toastMsg).hide();
    });

    //Request Cancel Buttons
    let cancelBtns = $('button[name="cancel"]');
    for (var i = 0; i < cancelBtns.length; i++) {
        $(cancelBtns[i]).on('click', function () {
            let requestId = this.getAttribute("requestId");
            $.ajax({
                method: 'DELETE',
                url: `Request/Cancel/${this.getAttribute("requestId")}`,
                success: function (result) {
                    $(`div[requestid="${requestId}"]`).animate({ width: 0, opacity: 0 }, 1000,
                        function () { $(this).remove(); });
                    const requestCount = $("#requestCount");
                    $(requestCount).text(parseInt($(requestCount).text()) - 1);
                },
                error: function (error) {
                    $(toastBody).html(`<p>${error.responseText}</p>`);
                    $(toastMsg).show();
                }
            });
            //console.log(this.getAttribute("requestId"));
        });
    }

    //Request Edit Buttons
    let editBtns = $('button[name="edit"]');
    for (var i = 0; i < editBtns.length; i++) {
        $(editBtns[i]).on('click', function () {
            $.ajax({
                method: 'PUT',
                url: `Request/Edit/${this.getAttribute("requestId")}`,
                data: {Dates: $(`input[requestId="${this.getAttribute("requestId")}"]`).val()},
                success: function (result) {
                    $(toastBody).html(`<p>${result}</p>`);
                    $(toastMsg).show();
                },
                error: function (error) {
                    $(toastBody).html(`<p>${error.responseText}</p>`);
                    $(toastMsg).show();
                }
            });
        });
    }

});