$(document).ready(function () {
    const login = $("#login");
    const signup = $("#signup");
    const formWrapper = $("#formWrapper");
    const activeIndicator = $("#activeIndicator");
    const messageContainer = $('#messageContainer');
    const messageDismiss = $("#messageContainerDismiss");

    $(messageDismiss).one('click', function () {
        $(messageContainer).css('animation-name', 'zoomOut').delay(1000).css('display', 'none');
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
