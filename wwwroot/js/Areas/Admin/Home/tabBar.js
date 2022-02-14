$(function () {
  var tab = $(".tabBar");
  // var tabBox = $("#tabBarBox");
  // var line = $( ".line" ).last();
  
  // var active = tab.find(".activeBar");
  // var pos = 0;
  // var wid = 0;

  // if (active.length) {
  //   pos = active.position().left;
  //   wid = active.width();
  //   line.css({
  //     left: pos,
  //     width: wid,
  //   });
  // }

  // $(window).on("load resize", function () {
  //   if ($(".tabBarItem").hasClass("activeBar")) {
  //     var divWidth = $(".tabBarItem").width();
  //     line.css("width", divWidth);
  //   }
  // });
  
  // $(tabBox).on("scroll", function () {
  //   var divPos = $(".activeBar").position();
  //   var divWidth = $(".activeBar").width();
  //     if (line.width() != 0) {
  //     tab.addClass("animate");

  //     if (divPos.left >= pos) {
  //       line.animate(
  //         {
  //           width: divPos.left - pos + divWidth,
  //         },
  //         300,
  //         function () {
  //           line.animate(
  //             {
  //               width: divWidth,
  //               left: divPos.left,
  //             },
  //             150,
  //             function () {
  //               tab.removeClass("animate");
  //             }
  //           );
  //         }
  //       );
  //     } else {
  //       line.animate(
  //         {
  //           left: divPos.left,
  //           width: pos - divPos.left + wid,
  //         },
  //         300,
  //         function () {
  //           line.animate(
  //             {
  //               width: divWidth,
  //             },
  //             150,
  //             function () {
  //               tab.removeClass("animate");
  //             }
  //           );
  //         }
  //       );
  //     }
  //     pos = divPos.left;
  //     wid = divWidth;
  //     done = true;
  //   }
  // });

  tab.find(".tabBarItem a").click(function (e) {
    e.preventDefault();
    if (!$(this).parent().hasClass("activeBar") /* && !tab.hasClass("animate")*/) {
      // tab.addClass("animate");
      var _this = $(this);
      tab.find(".tabBarItem").removeClass("activeBar");
      _this.parent().addClass("activeBar");

      // var position = _this.parent().position();
      // var width = _this.parent().width();

      // if (position.left >= pos) {
      //   line.animate(
      //     {
      //       width: position.left - pos + width,
      //     },
      //     300,
      //     function () {
      //       line.animate(
      //         {
      //           width: width,
      //           left: position.left,
      //         },
      //         150,
      //         function () {
      //           tab.removeClass("animate");
      //         }
      //       );
      //       _this.parent().addClass("activeBar");
      //     }
      //   );
      // } else {
      //   line.animate(
      //     {
      //       left: position.left,
      //       width: pos - position.left + wid,
      //     },
      //     300,
      //     function () {
      //       line.animate(
      //         {
      //           width: width,
      //         },
      //         150,
      //         function () {
      //           tab.removeClass("animate");
      //         }
      //       );
      //       _this.parent().addClass("activeBar");
      //     }
      //   );
      // }

      // pos = position.left;
      // wid = width;
    }
  });

  const leftArrow = $("#tabBarLeftArrow"),
    rightArrow = $("#tabBarRightArrow"),
    tabBar = $("#tabBarBox"),
    tabItems = $(".tabBarItem");

  const tabItem_width = tabItems.width();
  let tabBar_scroll_left = tabBar[0].scrollLeft;
  rightArrow.on("click", () => {
    tabBar_scroll_left += tabItem_width;
    if (tabBar_scroll_left >= tabItem_width * 3) {
      tabBar_scroll_left = tabItem_width * 3;
    }
    tabBar[0].scrollLeft = tabBar_scroll_left;
  });
  leftArrow.on("click", () => {
    tabBar_scroll_left -= tabItem_width;
    if (tabBar_scroll_left <= 0) {
      tabBar_scroll_left = 0;
    }
    tabBar[0].scrollLeft = tabBar_scroll_left;
  });

});