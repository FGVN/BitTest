$(document).ready(function () {
    const table = $('#csvTable').DataTable({
        "paging": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "lengthMenu": [10, 25, 50, 100],
        "order": [[1, 'asc']]
    });
});
