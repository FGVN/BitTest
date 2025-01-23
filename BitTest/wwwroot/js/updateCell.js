$(document).ready(function () {
    $('#csvTable').on('blur', '.editable', function () {
        let cell = $(this);
        let row = cell.closest('tr');
        let recordId = row.data('id');
        let column = cell.data('column');
        let newValue = cell.text();

        $.ajax({
            url: '/Csv/Update',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Id: recordId,
                Column: column,
                Value: newValue
            }),
            success: function () {
                console.log('Record updated successfully.');
            },
            error: function (xhr) {
                try {
                    let response = JSON.parse(xhr.responseText);
                    let errorMessage = response.error || "An unknown error occurred.";
                    let originalValue = response.originalValue || cell.data('originalValue');
                    showError(`Error updating column: ${errorMessage}`);
                    cell.text(cell.data('originalValue'));
                    cell.text(originalValue);
                } catch (e) {
                    alert(`Error updating record: ${xhr.responseText || "An unknown error occurred."}`);
                    cell.text(cell.data('originalValue'));
                }
            },
            beforeSend: function () {
                cell.data('originalValue', cell.text());
            }
        });
    });
});
