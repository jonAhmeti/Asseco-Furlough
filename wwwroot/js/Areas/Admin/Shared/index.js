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
});
