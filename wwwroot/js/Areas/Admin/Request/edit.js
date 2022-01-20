$(function () {
    $('.datepicker').datepicker({
        format: 'yyyy/mm/dd',
        startDate: '0d',
        clearBtn: true,
        daysOfWeekDisabled: '0,6',
        multidate: true,
        todayHighlight: true,
        daysOfWeekHighlighted: '0,6',
        language: 'EN'
    });

    const datepickers = $('.datepicker');

    //idk what I was planning here
    //for (var i = 0; i < datepickers.length; i++) {
    //    console.log(datepickers.length);
    //}
});