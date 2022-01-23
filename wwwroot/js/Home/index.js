$(document).ready(function () {
    const login = $("#login");
    const signup = $("#signup");
    const formWrapper = $("#formWrapper");
    const activeIndicator = $("#activeIndicator");
    const messageDismiss = $("#messageContainerDismiss");

    $(messageDismiss).on('click', function () {
        $('#messageContainer').css('transform', 'scale(1.1)');
        $('#messageContainer').css('transform', 'scale(1.)');
        .animate({ opacity: 0 }, 300);
    });

    $(login).on("click", function () {
        $(signup).attr("active", "false").addClass("text-muted");
        $(login).attr("active", "true").removeClass("text-muted");
        $(activeIndicator).css("margin-left", "0%");

        $.ajax({
            type: "GET",
            url: "Home/LoginPartial",
            success: function (result) {
                $(formWrapper).html(result);
            }
        });
    });

    $(signup).on("click", function () {
        $(login).attr("active", "false").addClass("text-muted");
        $(signup).attr("active", "true").removeClass("text-muted");
        $(activeIndicator).css("margin-left","50%");

        $.ajax({
            type: "GET",
            url: "Home/SignupPartial",
            success: function (result) {
                $(formWrapper).html(result);
            },
            error: function (error) {
                console.log(error.responseText);
            }
        });
    });
});
