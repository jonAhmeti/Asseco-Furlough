﻿$(function () {
    const rolesEdit = $("#rolesEditBtn");
    const rolesEditContainer = $("#rolesEditContainer");
    let addedRoles = $(".addedRole");
    let unaddedRoles = $(".unaddedRole");
    const submitRoles = $("#submitRoles");
    const submitSpan = $("#submitRoles > span");
    const toastMsg = $("#toastMsg");
    const toastBody = $("#toastBody");
    const toastClose = $("#toastMsg button")

    $(toastClose).on('click', function () {
        $(toastMsg).hide();
    });


    let initialValues = new Array();
    for (var i = 0; i < addedRoles.length; i++) {
        initialValues.push($(addedRoles[i]).attr("roleId"));
    }

    console.log(initialValues);

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

                    let tempAddedRoles = new Array();
                    for (var i = 0; i < addedRoles.length; i++) {
                        tempAddedRoles.push($(addedRoles[i]).attr("roleId"));
                    }
                    if (ArraysAreSame(tempAddedRoles, initialValues)) {
                        $(submitSpan).addClass("disabled");
                    }
                    else {
                        $(submitSpan).removeClass("disabled");
                    }
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

                    let tempAddedRoles = new Array();
                    for (var i = 0; i < addedRoles.length; i++) {
                        tempAddedRoles.push($(addedRoles[i]).attr("roleId"));
                    }
                    if (ArraysAreSame(tempAddedRoles, initialValues)) {
                        $(submitSpan).addClass("disabled");
                    }
                    else {
                        $(submitSpan).removeClass("disabled")
                    }
                });
            }
        }
    });

    submitRoles.on('click', function () {
        let addedRolesArray = new Array();
        for (var i = 0; i < addedRoles.length; i++) {
            addedRolesArray.push($(addedRoles[i]).attr("roleId"));
        }

        if (ArraysAreSame(addedRolesArray, initialValues)) {
            //alert("nothing changed");
            return;
        }
        else {
            $(submitRoles).addClass("spinner-border").css({"width": "40px", "height": "40px", "color": "#41b0eb"});
            $(submitSpan).addClass("visually-hidden");
            $.ajax({
                method: "PUT",
                url: "Department/UpdateDepartmentPositions",
                data: { positionsId: addedRolesArray }, //give DepartmentId taken from User Identity
                success: function (result) {
                    initialValues = Array.from(addedRolesArray);
                    $(submitSpan).addClass("disabled");

                    $(toastBody).html(`
                                ${result ? "Roles updated successfully." : "Something went wrong."}
                    `);
                    $(toastMsg).show();
                },
                error: function (error) {
                    $(toastBody).html(`
                        Something went wrong. <br/>
                        Status: ${error.status} <br/>
                        ${error.responseText.toString()}
                    `);
                    $(toastMsg).show();
                },
                complete: function () {
                    $(submitRoles).removeClass("spinner-border").removeAttr("style").css("top", "20px");
                    $(submitSpan).removeClass("visually-hidden");
                }
            });
        }
    });
});

function ArraysAreSame(firstArray, secondArray) {
    if (firstArray.length == secondArray.length) {
        for (var i = 0; i < firstArray.length; i++) {
            if (firstArray.indexOf(secondArray[i]) == -1) {
                return false;
            }
        }
        return true;
    }
    return false;
}