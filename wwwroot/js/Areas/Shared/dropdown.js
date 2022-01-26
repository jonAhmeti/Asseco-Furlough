$(function () {
  var reqDd = $("#requestDropdown");
  var reqDdToggle = $("#requestDropdownToggle");
  reqDdToggle.on("click", function () {
    if (!reqDd.hasClass("activeDropdown")) {
      reqDd.animate({"height": "200px"},{duration:300,queue:false}).addClass("activeDropdown");
    } else {
      reqDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
    }
  });
});
