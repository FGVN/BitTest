$(document).ready(function () {
    $('#csvTable').on('change', '.editable-date', function () {
        let input = $(this);
        let recordId = input.closest('tr').data('id');
        let newValue = input.val();

        $.ajax({
            url: '/Csv/Update',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Id: recordId,
                Column: "DateOfBirth",
                Value: newValue
            }),
            success: function () {
                console.log('Date of Birth updated successfully.');
            },
            error: function (xhr) {
                const errorMessage = xhr.responseText || "An unknown error occurred.";
                showError(`Error updating column: ${errorMessage}`);
                input.val(input.data('originalValue'));
            }
        });
    });
});
