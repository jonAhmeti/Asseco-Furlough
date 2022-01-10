$(function () {
    const departments = $(".tabBarItem");
    
    for (let i = 0; i < departments.length; i++) 
    {   
        $(departments[i]).on("click", function() 
        {
            if (!$(this).hasClass("activeBar")) {
                console.log($(this).find("a").text());
            }
        });
    }
});