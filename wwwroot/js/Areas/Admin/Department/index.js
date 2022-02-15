$(function () {
    const rolesEdit = $("#rolesEditBtn");
    const rolesEditContainer = $("#rolesEditContainer");
    let addedRoles = $(".addedRole");
    let unaddedRoles = $(".unaddedRole");
    const submitRoles = $("#submitRoles");
    const submitSpan = $("#submitRoles > span");
    const toastMsg = $("#toastMsg");
    const toastBody = $("#toastBody");
    const toastClose = $("#toastMsg button")
    const employeeCards = $(".employeePositions");

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
        addedRoles = $(".addedRole");

        if (ArraysAreSame(addedRolesArray, initialValues)) {
            //alert("nothing changed");
            return;
        }
        else {
            $(submitRoles).addClass("spinner-border").css({"width": "40px", "height": "40px", "color": "#41b0eb"});
            $(submitSpan).addClass("visually-hidden");
            $.ajax({
                method: "PUT",
                url: `/Admin/Department/UpdateDepartmentPositions/`,
                data: { departmentId: $('input[name="Id"]').val(), positionsId: addedRolesArray }, //give DepartmentId taken from User Identity
                success: function (result) {
                    initialValues = Array.from(addedRolesArray);
                    $(submitSpan).addClass("disabled");

                    $(toastBody).html(`
                                ${result ? "Roles updated successfully." : "Something went wrong."}
                    `);
                    $(toastMsg).show();

                    //set addedRolesArray to employees' positions dropdown also
                    for (var i = 0; i < employeeCards.length; i++) {
                        if (addedRoles.length == 0) {
                            $(employeeCards[i]).html('<option disabled value="-1" selected>-- Choose one --</option>');
                        }
                        else {
                            $(employeeCards[i]).html('<option disabled value="-1" selected>-- Choose one --</option>');
                            $(employeeCards[i]).append($.map(addedRoles, function (item) {
                                return `<option ${$(employeeCards[i]).attr('currentPosition') == $(item).attr('roleId') ? 'selected' : ''} 
                                                value=${$(item).attr('roleId')}>${$(item).text()}
                                        </option>`
                            }));
                        }
                    }
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

    //set employee's selected position to the currently selected position
    for (var i = 0; i < employeeCards.length; i++) {
        let positionId = $(employeeCards[i]).attr("currentPosition");
        $(employeeCards[i]).children(`option[value="${positionId}"]`).attr("selected", "selected");
        $(employeeCards[i]).on("change", function () {
            $.ajax({
                method: "PUT",
                url: "Department/UpdateEmployeePosition",
                data: { employeeId: parseInt($(this).attr("employeeId")), positionId: parseInt($(this).val()) },
                success: function (result) {
                    $(toastBody).html(`
                                ${result ? "Position changed successfully." :
                            "Something went wrong changing employee's position."}
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
                }

            });
        });
    }
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