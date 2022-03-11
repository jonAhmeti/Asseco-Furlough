$(function () {
    let rightArrow = $("#infoCardsArrowRight");
    let leftArrow = $("#infoCardsArrowLeft");
    let infoCards = $("#infoCards");
    const infoCardsWrapper = $("#infoCardsWrapper");

    //info cards
    $(infoCardsWrapper).on('mouseenter', function () {

        $(rightArrow).animate({ 'right': 0 }, 300);
        $(leftArrow).animate({ 'left': 0 }, 300);
    });


    $(infoCardsWrapper).on('mouseleave', function () {
        //if hovering over the left and right arrows
        if ($("#infoCardsArrowRight:hover").length != 0 || $("#infoCardsArrowLeft:hover").length != 0)
            return;

        $(rightArrow).animate({ 'right': '-100px' }, 300);
        $(leftArrow).animate({ 'left': '-100px' }, 300);
    });

    $(rightArrow).on("click", function () {
        let item_width = $('.card').outerWidth(true);
        console.log(item_width);
        let scrollVal = $(infoCards).scrollLeft();
        $(infoCards).animate({ scrollLeft: scrollVal + item_width }, 500);
    });


    $(leftArrow).on("click", function () {
        let item_width = $('.card').outerWidth(true);
        console.log(item_width);
        let scrollVal = $(infoCards).scrollLeft();
        $(infoCards).animate({ scrollLeft: scrollVal - item_width }, 500);
    });

    //check RequestType and inform user
    const prevYearDays = $(infoCards).find('p.card-text[requestId="11"] span.fw-bold').text(); //get value of prevYear days amount
    const typeSelect = $('select[name="requestType"]');
    $(typeSelect).on('change', function () {
        console.log($(typeSelect).find(':selected').val());
        if ($(typeSelect).find(':selected').val() == 2) { //selected Yearly requestType
            alert('Previous year days will be used if available before annual leave days.'); //make change here
        }
    })
});