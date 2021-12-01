$(function () {
    const rolesEdit = $("#rolesEditBtn");
    const rolesEditContainer = $("#rolesEditContainer");

    $(rolesEdit).on('click', function () {
        if ($(rolesEditContainer).css('top') == '0px') {
            $(rolesEditContainer).animate({ 'top': '-100%' }, 200);
            //$(rolesEditContainer).animate({ 'top': '-100%' }, 200);

        }
        else {
            $(rolesEditContainer).animate({ 'top': '0px' }, 200);
            //$(rolesEditContainer).animate({ 'opacity': '1' }, 200);
        }
    });

});