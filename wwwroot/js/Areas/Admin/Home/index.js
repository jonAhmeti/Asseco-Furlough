$(function () {
    const departments = $(".tabBarItem");
    var departmentsID = [];
    var departmentsName = [];
    var employeeDep = $(".tabBar").next().find(".listItem");
    var lookup = {};
    
    for (let i = 0; i < departments.length; i++) {

        departmentsID.push($(departments[i]).attr("departmentId"));
        departmentsName.push(departments[i].innerText);
        lookup[departmentsName[i]] = departmentsID[i];

        var clickPrevention = false;
        
        $(departments[i]).on("click", function () {
            if (clickPrevention) {
                return;
            } 
            else if (!$(this).hasClass("activeBar")) {
                var tabText = $(this).find("a").text().toUpperCase();
                for (let i = 0; i < employeeDep.length; i++) {
                    var listDepText = employeeDep[i].getElementsByTagName("li");
                    if (lookup[tabText] == $(listDepText[2]).text()) {
                        $(listDepText).parent().removeClass("d-none");
                    } else {
                        $(listDepText).parent().addClass("d-none");
                    }
                }
            }
            clickPrevention = true;
            setTimeout(function(){clickPrevention = false;}, 450);
        });
    }
    console.log(departmentsID);
    console.log(departmentsName);
    console.log(lookup);
});