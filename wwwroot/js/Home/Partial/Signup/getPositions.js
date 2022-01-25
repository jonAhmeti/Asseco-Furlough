$(function () {
    const selectPosition = $('select[name="PositionId"]');
    const selectDepartment = $('select[name="DepartmentId"]');

    $(selectDepartment).on('change', function () {
        console.log($(this).find(":selected").val());
        $.ajax({
            method: "GET",
            url: "Home/GetPositions",
            data: { departmentId: $(this).find(":selected").val() },
            success: function (result) {
                //console.log(result);
                $(selectPosition).removeAttr('disabled');
                $(selectPosition).html(result.map((item) => {
                    return `<option value="${item.id}">${item.title}</option>`
                }));
            }
        });
    });
});