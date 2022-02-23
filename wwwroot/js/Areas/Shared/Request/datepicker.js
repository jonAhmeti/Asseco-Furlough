$(function () {
    const today = new Date();
    today.setMonth(today.getMonth() - 6);
    $('.datepicker').datepicker({
        format: 'yyyy/mm/dd',
        startDate: `${today.getFullYear()}/${today.getMonth()}/${today.getDay()}`, //'0d',
        clearBtn: true,
        daysOfWeekDisabled: '0,6',
        multidate: true,
        todayHighlight: true,
        daysOfWeekHighlighted: '0,6',
        language: 'EN'
    });

    const totalDaysInput = $('input[name="DaysAmount"]');
    const datesInput = $('input[name="Dates"]');
    datesInput.on('change', function () {
        let datesCount = $(this).val().split(',');
        totalDaysInput.val(datesCount[0] == '' ? 0 : datesCount.length);
    });
});