$(document).ready(function () {
    $('#csvTable').on('change', '.married-selector', function () {
        let selector = $(this);
        let recordId = selector.data('id');
        let newValue = selector.val();

        $.ajax({
            url: '/Csv/Update',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Id: recordId,
                Column: "Married",
                Value: newValue
            }),
            success: function () {
                alert('Married status updated successfully.');
            },
            error: function (xhr) {
                const errorMessage = xhr.responseText || "An unknown error occurred.";
                $('#error-text').text(`Error updating married status: ${errorMessage}`);
                $('#error-message').fadeIn().delay(5000).fadeOut();
                selector.val(selector.data('originalValue'));
            },
            beforeSend: function () {
                selector.data('originalValue', selector.val());
            }
        });
    });
});
