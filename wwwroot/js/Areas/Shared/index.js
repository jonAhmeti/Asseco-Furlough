$(function () { 
  var rowIdArray = $("[rowId]");
  for (let i = 0; i < rowIdArray.length; i++) {
    $(`tr[rowId=${i}]`).find("a.rowLink").hover(
        function () {
          $(`tr[rowId=${i}]`).find("a.rowLink").css("color", "var(--bs-primary)");
        },
        function () {
          $(`tr[rowId=${i}]`).find("a.rowLink").css("color", "var(--bs-dark)");
        }
    );
  }
  var pdTableDiv = $(".positionDepartmentTableDiv");
  var roleTableDiv = $(".roleTableDiv");
  var erTableDiv = $(".employeeRequestTableDiv");
  var detailsWrapper = $("#detailsWrapper");
  $(window).on("load resize", function(){
    if ($(window).width() < 525) {
      $(erTableDiv).removeClass("d-flex justify-content-center");
      $(erTableDiv).addClass("overflow-auto");
    }
    if ($(window).width() > 525) {
      $(erTableDiv).removeClass("overflow-auto");
      $(erTableDiv).addClass("d-flex justify-content-center");
    }
    if ($(window).width() < 350) {
      $(pdTableDiv).removeClass("d-flex justify-content-center");
      $(pdTableDiv).addClass("overflow-auto");
    }
    if ($(window).width() > 350) {
      $(pdTableDiv).removeClass("overflow-auto");
      $(pdTableDiv).addClass("d-flex justify-content-center");
    }
    if ($(window).width() < 440) {
      $(roleTableDiv).removeClass("d-flex justify-content-center");
      $(roleTableDiv).addClass("overflow-auto");
    }
    if ($(window).width() > 440) {
      $(roleTableDiv).removeClass("overflow-auto");
      $(roleTableDiv).addClass("d-flex justify-content-center");
    }
    if ($(window).width() < 768) {
      $(detailsWrapper).removeClass("ms-4");
    }
    if ($(window).width() > 768) {
      $(detailsWrapper).addClass("ms-4");
    }
  });
});
