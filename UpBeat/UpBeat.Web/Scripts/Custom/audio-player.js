$('.audioPlayBtn').click(function (ev) {
    let myAudio = $(ev.target).parent().next()[0];
    let $target = $(ev.target).parent();
    let $icon = $('<i/>').addClass('material-icons');

    if (myAudio.paused) {
        myAudio.play();
        $icon.html('pause_circle_outline');

        $target.html($icon);
    } else {
        myAudio.pause();
        $icon.html('play_circle_outline');

        $target.html($icon);
    }
});