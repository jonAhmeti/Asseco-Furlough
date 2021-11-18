$(function () {
    let rightArrow = $("#infoCardsArrowRight");
    let leftArrow = $("#infoCardsArrowLeft");
    let infoCards = $("#infoCards");
    let stopFlag = false;

    //Crashes --- Fix it 
    $(rightArrow).on("mouseenter", function () {
        do {
            let currentScroll = $(infoCards).scrollLeft() + 10;
            $(infoCards).scrollLeft(currentScroll);
        } while (!stopFlag);
    });

    $(rightArrow).on("mouseleave", function () {
        stopFlag = true;
    });
});