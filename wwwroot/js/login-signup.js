$(document).ready(function () {
  let login = $("#login");
  let signup = $("#signup");
  let formWrapper = $("#formWrapper");
  $(login).on("click", function () {
    $(signup).attr("active", "false");
    $(login).attr("active", "true");

    $.ajax({
        type: "GET",
        url:"/Home/Partial/Login.cshtml",
        success: function(result) {
            $(formWrapper).html(result);
        }
    });
  });
  $(signup).on("click", function () {
    $(login).attr("active", "false");
    $(signup).attr("active", "true");

    $.ajax({
        type: "GET",
        url:"/Home/Partial/Signup.cshtml",
        success: function(result) {
            $(formWrapper).html(result);
        }
    });
  });
});
