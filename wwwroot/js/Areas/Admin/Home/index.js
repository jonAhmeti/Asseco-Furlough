$(function () {
    const departments = $(".tabBarItem");
    var employeeDep = $(".listItem");
    const lookup = {
        Banking: 1,
        Test: 2,
        AIS: 5,
        CASD: 6,
    };

    for (let i = 0; i < departments.length; i++) {
        $(departments[i]).on("click", function () {
            if (!$(this).hasClass("activeBar")) {
                var tabText = $(this).find("a").text();
                // employeeDep.forEach(element => {
                //     var listDepText = element.text();
                //     if (lookup[tabText] == listDepText) {
                //         employeeDep[i].parent().addClass("d-block");
                //     } else {
                //         employeeDep[i].parent().addClass("d-none");
                //     }
                // });
                for (let i = 0; i < employeeDep.length; i++) {
                    var ar = employeeDep[i].getElementsByTagName("li");
                    if ($("ul li").hasClass("col")) {
                        console.log(ar[2]);
                    }
                }
            }
        });
    }
});
