$(function () {
    //init of datatable
    var table = $(".table").DataTable({
        dom: "<'pt-5'<'d-flex justify-content-between px-3'pf><t><'d-flex flex-column 'B>>",
        buttons: [
            {
                extend: 'pdf',
                title: 'Admin_Request-' + new Date().toISOString(),
                exportOptions: {
                    columns: 'th:not(:last-child)'
                }
            },
            {
                extend: 'csv',
                title: 'Admin_Request-' + new Date().toISOString(),
                exportOptions: {
                    columns: 'th:not(:last-child)'
                }
            },
            {
                extend: 'print',
                title: 'Admin_Request-' + new Date().toISOString(),
                exportOptions: {
                    columns: 'th:not(:last-child)'
                }
                
            }],
        columnDefs: [
            { targets: [4], sortable: false, searchable: false },
        ]
    });
});