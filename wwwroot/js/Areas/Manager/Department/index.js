$(function () {
    const rolesEdit = $("#rolesEditBtn");
    const rolesEditContainer = $("#rolesEditContainer");
    let addedRoles = $(".addedRole");
    let unaddedRoles = $(".unaddedRole");

    $(rolesEdit).on('click', function () {
        if ($(rolesEditContainer).css('top') == '0px') { //when unaddedRoleContainer is hidden
            $(rolesEditContainer).animate({ 'top': '-100%' }, 200);
            $(rolesEditContainer).css('z-index', '-2');

            //remove click events for addedRoles
            for (var i = 0; i < addedRoles.length; i++) {
                $(addedRoles[i]).off('click');
            }

            //remove click events for unaddedRoles
            for (var i = 0; i < unaddedRoles.length; i++) {
                $(unaddedRoles[i]).off('click');
            }
        }
        else { //when unaddedRoleContainer is shown
            $(rolesEditContainer).animate({ 'top': '0px' }, 200,
                function () { //display unaddedRoles container
                    $(rolesEditContainer).css('z-index', '1');
                });

            //add click events for addedRoles
            for (var i = 0; i < addedRoles.length; i++)
            {
                $(addedRoles[i]).on('click', function () {

                    if ($(this).hasClass("addedRole")) {
                        $(rolesEditContainer).append(this);

                        $(this).removeClass("addedRole");
                        $(this).addClass("unaddedRole");
                    }
                    else if ($(this).hasClass("unaddedRole")) {
                        $(rolesContainer).append(this);

                        $(this).removeClass("unaddedRole");
                        $(this).addClass("addedRole");
                    }

                    addedRoles = $(".addedRole");
                    unaddedRoles = $(".unaddedRole");
                });
            }

            //add click events for unaddedRoles
            for (var i = 0; i < unaddedRoles.length; i++) {
                $(unaddedRoles[i]).on('click', function () {

                    if ($(this).hasClass("unaddedRole")) {
                        $(rolesContainer).append(this);

                        $(this).removeClass("unaddedRole");
                        $(this).addClass("addedRole");
                    }
                    else if ($(this).hasClass("addedRole")) {
                        $(rolesEditContainer).append(this);

                        $(this).removeClass("addedRole");
                        $(this).addClass("unaddedRole");
                    }

                    addedRoles = $(".addedRole");
                    unaddedRoles = $(".unaddedRole");
                });
            }
        }
    });
});