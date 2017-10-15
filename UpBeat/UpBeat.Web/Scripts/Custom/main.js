$(document).ready(function () {
    $(".button-collapse").sideNav();

});

$('#favourite-btn').click(function (ev) {
    let $form = $('#favourite-form');
    $form.submit();
});

function afterFavourite(responce) {
    $('#favourite-btn').html(responce);
    $('#favourite-btn').css('background-color', 'pink');
}
