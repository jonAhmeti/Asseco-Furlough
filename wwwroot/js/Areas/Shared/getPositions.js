$(function () {
    const selectPosition = $('select[name="PositionId"]');
    const selectDepartment = $('select[name="DepartmentId"]');
    const submitBtn = $('#submitBtn');

    //on first load
    $.ajax({
        method: "GET",
        url: "/Home/GetPositions",
        data: { departmentId: $(selectDepartment).find(":selected").val() },
        success: function (result) {
            if (result != undefined && result != null) {
                if (hasUsers || window.location.href.includes("Edit") || window.location.href.includes("Create"))
                    $(submitBtn).removeAttr('disabled');

                $(selectPosition).removeAttr('disabled');

                $(selectPosition).html(result.map((item) => {
                    return `<option value="${item.id}">${item.title}</option>`
                })).removeClass('text-primary');
            }
            else {
                $(submitBtn).attr('disabled', '');
                $(selectPosition).attr('disabled', '');
                $(selectPosition).html('<option>This department doesn\'t have any positions.</option>').addClass('text-primary');
            }
        },
        error: function (error) {
            console.log('error');
        }
    });

    $(selectDepartment).on('change', function () {
        console.log($(this).find(":selected").val());
        $.ajax({
            method: "GET",
            url: "/Home/GetPositions",
            data: { departmentId: $(this).find(":selected").val() },
            success: function (result) {
                if (result != undefined && result != null) {

                    if (hasUsers || window.location.href.includes("Edit") || window.location.href.includes("Create"))
                        $(submitBtn).removeAttr('disabled');
                    $(selectPosition).removeAttr('disabled');

                    $(selectPosition).html(result.map((item) => {
                        return `<option value="${item.id}">${item.title}</option>`
                    })).removeClass('text-primary');
                }
                else {
                    $(submitBtn).attr('disabled', '');
                    $(selectPosition).attr('disabled', '');
                    $(selectPosition).html('<option>This department doesn\'t have any positions.</option>').addClass('text-primary');
                }
            }
        });
    });
});