$(function () {
    $('#search-button').click(function (e) {
        e.preventDefault();

        document.location.href = "/Books/Book?userId=" + $('#ViewModel_UserId').val();
    })
});