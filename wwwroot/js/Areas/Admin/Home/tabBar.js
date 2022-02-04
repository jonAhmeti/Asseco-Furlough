$(function () {
  var tab = $(".tabBar");
  var line = $( ".line" ).last();
  
  var active = tab.find(".activeBar");
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

  $(window).on("load resize", function () {
    if ($(".tabBarItem").hasClass("activeBar")) {
      var divWidth = $(".tabBarItem").width();
      line.css("width", divWidth);
    }
  });
  
  tab.find(".tabBarItem a").click(function (e) {
    e.preventDefault();
    if (!$(this).parent().hasClass("activeBar") && !tab.hasClass("animate")) {
      tab.addClass("animate");

      var _this = $(this);

      tab.find(".tabBarItem").removeClass("activeBar");

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
            _this.parent().addClass("activeBar");
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
            _this.parent().addClass("activeBar");
          }
        );
      }

      pos = position.left;
      wid = width;
    }
  });
});
