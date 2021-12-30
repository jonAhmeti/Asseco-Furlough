$(function () {
    //init of datatable
    var table = $(".table").DataTable({
        dom: "<'employeeRequestTableDiv mt-1 shadow rounded border'<'d-flex justify-content-between'pf><t><'d-flex flex-column 'B>>",
        buttons: true,
        columnDefs: [
            { targets: [4], sortable: false },
        ]
    });
});