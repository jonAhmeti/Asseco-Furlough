$(function () {
  var sideNav = $("#sidenav");
  var sideHam = $("#sideHam");
  var menuCheckbox = $("#menu-btn");
  $(sideHam).on("click", function () {
    if ($(menuCheckbox).is(":checked")) {
      sideNav.css({"display": "none"});
      sideNav.next().css({"display": "block"});
    } else {
      sideNav.css({"display": "block", "padding-top": "5rem", "width": "100vw", "position": "absolute", "z-index": "99", "text-align": "center"});
      sideNav.next().css({"display": "none"});
    }
  });
});
