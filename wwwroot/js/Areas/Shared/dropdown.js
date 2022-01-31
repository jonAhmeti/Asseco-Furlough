$(function () {
  var dropDownToggle = $(".dropdownToggle");
  dropDownToggle.next().slideUp(0);

  dropDownToggle.on("click", function () {
    var layoutDropdown = $(this).next();
    
    if (!layoutDropdown.hasClass("activeDropdown")) {
      dropDownToggle.next().slideUp(500).removeClass("activeDropdown");
      layoutDropdown.slideDown(500).addClass("activeDropdown");
    } else {
      layoutDropdown.slideUp(500).removeClass("activeDropdown");
    }
  });
});