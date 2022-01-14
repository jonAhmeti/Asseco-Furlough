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

    const datepickers = $('.datepicker');

    for (var i = 0; i < datepickers.length; i++) {
        console.log(datepickers.length);
    }

    let cancelBtns = $('button[name="cancel"]');
    for (var i = 0; i < cancelBtns.length; i++) {
        $(cancelBtns[i]).on('click', function () {
            $.ajax({
                requestId: this.getAttribute("requestId"),
                method: 'DELETE',
                url: `Request/Cancel/${this.getAttribute("requestId")}`,
                success: function (result) {
                    $(`div[requestid="${this.requestId}"]`).animate({ width: 0, opacity: 0 }, 1000,
                        function () { $(this).remove(); });
                    const requestCount = $("#requestCount");
                    $(requestCount).text(parseInt($(requestCount).text()) - 1);
                },
                error: function (error) {

                }
            });
            console.log(this.getAttribute("requestId"));
        });
    }
});