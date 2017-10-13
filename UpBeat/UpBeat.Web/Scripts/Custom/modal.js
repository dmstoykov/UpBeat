$(document).ready(function () {
    $('.modal').modal();
})

$(() => {
    $("#submit-btn").click(function () {
        const $form = $('#addition-form');
        $form.submit();
    });
});

function openModal(title) {
    $('#popupModal').modal('open');
    $('.modal-title').text(title);
}
