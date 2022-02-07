$(function () {
  var sideNav = $("#sidenav");
  var sideHam = $("#sideHam");
  var menuCheckbox = $("#menu-btn");
  $(window).resize(function(){
    if ($(window).width() > 768) {
      sideNav.removeAttr("style");
      sideNav.css({"display": "none"});
      sideNav.next().css({"display": "flex"});
      $(menuCheckbox).prop('checked', false);
    }
    if (($(window).width() < 768) && (menuCheckbox.is(":checked") == true)) {
      sideNav.css({"display": "flex", "padding-top": "1rem", "width": "100vw", "position": "absolute", "z-index": "99", "text-align": "center"});
      sideNav.next().css({"display": "none"});
    }
  });
  $(sideHam).on("click", function () {
    if (menuCheckbox.is(":checked")) {
      sideNav.removeAttr("style");
      sideNav.css({"display": "none"});
      sideNav.next().css({"display": "flex"});
    } else {
      sideNav.css({"display": "flex", "padding-top": "1rem", "width": "100vw", "position": "absolute", "z-index": "99", "text-align": "center"});
      sideNav.next().css({"display": "none"});
    }
  });

  var logOutButton = $(".logOutSidenav");
  $(logOutButton).hover( 
    function () {
    $(logOutButton).removeClass('logOutGradientOutHover').addClass('logOutGradientOnHover');
  }, function () {
    $(logOutButton).removeClass('logOutGradientOnHover').addClass('logOutGradientOutHover');
  });
});