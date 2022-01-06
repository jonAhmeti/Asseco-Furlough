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
                orientation: 'landscape',
                exportOptions: {
                    stripHtml: false,
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body)
                        .prepend(
                            `<img src="https://${window.location.host}/images/asseco_southeasterneurope_logo.svg" style="position:absolute; bottom:0; left:0; opacity:0.1; width:50%;"/>`
                        ); //window.location.host gets localhost:port at the moment
                }
                
            }],
        columnDefs: [
            { targets: [4], sortable: false, searchable: false },
        ]
    });
});