$(document).ready(function () {

    // filtering for catch-all output table
    $("#catchAllSearchFilter").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#catchAllOutputTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});