$(function () {
  var sideNav = $("#sidenav");
  var sideHam = $("#sideHam");
  var menuCheckbox = $("#menu-btn");
  $(window).resize(function(){
    if ($(window).width() > 768) {
      sideNav.removeAttr("style");
      sideNav.css({"display": "none"});
      sideNav.next().css({"display": "block"});
      $(menuCheckbox).prop('checked', false);
    }
    if (($(window).width() < 768) && (menuCheckbox.is(":checked") == true)) {
      sideNav.css({"display": "block", "padding-top": "5rem", "width": "100vw", "position": "absolute", "z-index": "99", "text-align": "center"});
      sideNav.next().css({"display": "none"});
    }
  });
  $(sideHam).on("click", function () {
    if (menuCheckbox.is(":checked")) {
      sideNav.removeAttr("style");
      sideNav.css({"display": "none"});
      sideNav.next().css({"display": "block"});
    } else {
      sideNav.css({"display": "block", "padding-top": "5rem", "width": "100vw", "position": "absolute", "z-index": "99", "text-align": "center"});
      sideNav.next().css({"display": "none"});
    }
  });
});