$(document).ready(function () {
    let recordToDelete;

    $('#csvTable').on('click', '.delete-btn', function () {
        let button = $(this);
        recordToDelete = button.data('id');

        // Show the modal
        $('#deleteModal').show();
    });

    // Close modal
    $('.close-btn, #cancelDelete').on('click', function () {
        $('#deleteModal').hide();
    });

    // Confirm deletion
    $('#confirmDelete').on('click', function () {
        let row = $('.delete-btn[data-id="' + recordToDelete + '"]').closest('tr');

        $.ajax({
            url: '/Table/Delete',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ Id: recordToDelete }),
            success: function () {
                alert('Record deleted successfully.');
                $('#deleteModal').hide();
                table.row(row).remove().draw();
            },
            error: function (xhr) {
                alert(`Error deleting record: ${xhr.responseText || "An unknown error occurred."}`);
                $('#deleteModal').hide();
            }
        });
    });
});
