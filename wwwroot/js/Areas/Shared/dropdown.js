$(function () {
  var dropDownToggle = $(".dropdownToggle");
  dropDownToggle.next().slideUp(0);
  
  setTimeout(function() {
    dropDownToggle.next().removeClass("hideDropdown");
  }, 1);
  
  dropDownToggle.on("click", function () {
    
    var layoutDropdown = $(this).next();
    if (!layoutDropdown.hasClass("activeDropdown")) {
      dropDownToggle.next().slideUp(300).removeClass("activeDropdown");
      layoutDropdown.slideDown(300).addClass("activeDropdown");
    } else {
      layoutDropdown.slideUp(300).removeClass("activeDropdown");
    }
  });
});