$(function () {
  var dropDownToggle = $(".dropdownToggle");
  var animationFinished = true;
  dropDownToggle.next().slideUp(0);

  setTimeout(function () {
    dropDownToggle.next().removeClass("hideDropdown");
  }, 1);

  dropDownToggle.on("click", function () {
    if (animationFinished == true) {
      animationFinished = false;
      var layoutDropdown = $(this).next();
      if (!layoutDropdown.hasClass("activeDropdown")) {
        dropDownToggle.next().slideUp(500).removeClass("activeDropdown");
        layoutDropdown.slideDown(500, function () {
            animationFinished = true;
          }).addClass("activeDropdown");
      } else {
        layoutDropdown.slideUp(500, function () {
            animationFinished = true;
          }).removeClass("activeDropdown");
      }
    } else {
      return false;
    }
  });
});
