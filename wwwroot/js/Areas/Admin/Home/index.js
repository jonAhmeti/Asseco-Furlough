﻿$(function () {
    const departments = $(".tabBarItem");
    var employeeDep = $(".tabBar").next().find(".listItem");
    const lookup = {
        Banking: 1,
        Test: 2,
        AIS: 5,
        CASD: 6,
    };

    for (let i = 0; i < departments.length; i++) {
        var clickPrevention = false;
        $(departments[i]).on("click", function () {
            if (clickPrevention) {
                return;
            } 
            else if (!$(this).hasClass("activeBar")) {
                console.log("lol");
                var tabText = $(this).find("a").text();
                for (let i = 0; i < employeeDep.length; i++) {
                    var listDepText = employeeDep[i].getElementsByTagName("li");
                    if (lookup[tabText] == $(listDepText[2]).text()) {
                        $(listDepText).parent().removeClass("d-none");
                        $(listDepText).parent().addClass("row");
                    } else {
                        $(listDepText).parent().addClass("d-none");
                        $(listDepText).parent().removeClass("row");
                    }
                }
            }
            clickPrevention = true;
            setTimeout(function(){clickPrevention = false;}, 450);
        });
    }
});