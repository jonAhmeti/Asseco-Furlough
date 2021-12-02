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
  $(window).on("load resize", function(){
    if ($(window).width() < 470) {
      $(".tableDiv").removeClass("d-flex justify-content-center");
      $(".tableDiv").addClass("overflow-auto");
    }
    if ($(window).width() > 470) {
      $(".tableDiv").removeClass("overflow-auto");
      $(".tableDiv").addClass("d-flex justify-content-center");
    }
    if ($(window).width() < 350) {
      $(".roleTableDiv").removeClass("d-flex justify-content-center");
      $(".roleTableDiv").addClass("overflow-auto");
    }
    if ($(window).width() > 350) {
      $(".roleTableDiv").removeClass("overflow-auto");
      $(".roleTableDiv").addClass("d-flex justify-content-center");
    }
  });
});
