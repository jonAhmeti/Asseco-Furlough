$(function () {
    //init of datatable
    var table = $(".table").DataTable({
        dom: "<'pt-5 px-3'<'d-flex justify-content-between'pf><t><'d-flex flex-column 'B>>",
        buttons: true,
        columnDefs: [
            { targets: [4], sortable: false, searchable: false },
        ]
    });
});