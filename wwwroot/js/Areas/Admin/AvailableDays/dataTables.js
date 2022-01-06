$(function () {
    //init of datatable
    var table = $(".table").DataTable({
        dom: "<'pt-5'<'d-flex justify-content-between px-3'pf><t><'d-flex flex-column 'B>>",
        buttons: [
            //{
            //    extend: 'copyHtml5',
            //    title: 'Admin_AvailableDays-' + new Date().toISOString(),
            //}, //doesn't copy correctly
            {
                extend: 'pdfHtml5',
                orientation: 'landscape',
                title: 'Admin_AvailableDays-' + new Date().toISOString()
            },
            {
                extend: 'csvHtml5',
                title: 'Admin_AvailableDays-' + new Date().toISOString(),
            },
            {
                extend: 'print',
                title: 'Admin_AvailableDays-' + new Date().toISOString(),
                orientation: 'landscape',
                exportOptions: {
                    stripHtml: false
                },
                customize: function (win) {
                    $(win.document.body)
                        .prepend(
                            `<img src="https://${window.location.host}/images/asseco_southeasterneurope_logo.svg" style="position:absolute; bottom:0; left:0; opacity:0.1; width:50%;"/>`
                    ); //window.location.host gets localhost:port at the moment
                }
            }],
    });
});