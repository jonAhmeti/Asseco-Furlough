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
});