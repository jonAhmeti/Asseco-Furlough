$(function () {
  var tab = $(".tabBar");
  var line = $( ".tabBar div" ).last();
  line.addClass("line");

  var active = tab.find(".active");
  var pos = 0;
  var wid = 0;

  if (active.length) {
    pos = active.position().left;
    wid = active.width();
    line.css({
      left: pos,
      width: wid,
    });
  }

  tab.find(".tabBarItem a").click(function (e) {
    e.preventDefault();
    if (!$(this).parent().hasClass("active") && !tab.hasClass("animate")) {
        tab.addClass("animate");

      var _this = $(this);

      tab.find(".tabBarItem").removeClass("active");

      var position = _this.parent().position();
      var width = _this.parent().width();

      if (position.left >= pos) {
        line.animate(
          {
            width: position.left - pos + width,
          },
          300,
          function () {
            line.animate(
              {
                width: width,
                left: position.left,
              },
              150,
              function () {
                tab.removeClass("animate");
              }
            );
            _this.parent().addClass("active");
          }
        );
      } else {
        line.animate(
          {
            left: position.left,
            width: pos - position.left + wid,
          },
          300,
          function () {
            line.animate(
              {
                width: width,
              },
              150,
              function () {
                tab.removeClass("animate");
              }
            );
            _this.parent().addClass("active");
          }
        );
      }

      pos = position.left;
      wid = width;
    }
  });
});
