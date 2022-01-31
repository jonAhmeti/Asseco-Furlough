// $(function () {
//   var reqDd = $("#requestDropdown");
//   var reqDdToggle = $("#requestDropdownToggle");
//   var staffDd = $("#staffDropdown");
//   var staffDdToggle = $("#staffDropdownToggle");
//   var compDd = $("#compDropdown");
//   var compDdToggle = $("#compDropdownToggle");

//   reqDdToggle.on("click", function () {
//     if (!reqDd.hasClass("activeDropdown")) {
//       staffDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
//       compDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
//       reqDd.animate({"height": "270px"},{duration:300,queue:false}).addClass("activeDropdown");
//     } else {
//       reqDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
//     }
//   });
//   staffDdToggle.on("click", function () {
//     if (!staffDd.hasClass("activeDropdown")) {
//       compDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
//       reqDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
//       staffDd.animate({"height": "200px"},{duration:300,queue:false}).addClass("activeDropdown");
//     } else {
//       staffDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
//     }
//   });
//   compDdToggle.on("click", function () {
//     if (!compDd.hasClass("activeDropdown")) {
//       reqDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
//       staffDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
//       compDd.animate({"height": "135px"},{duration:300,queue:false}).addClass("activeDropdown");
//     } else {
//       compDd.animate({"height": "0px"},{duration:300,queue:false}).removeClass("activeDropdown");
//     }
//   });
// });
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