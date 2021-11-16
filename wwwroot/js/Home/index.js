$(document).ready(function () {
    let login = $("#login");
    let signup = $("#signup");
    let formWrapper = $("#formWrapper");
    let activeIndicator = $("#activeIndicator");

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
            }
        });
    });
});
