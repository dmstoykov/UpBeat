$(document).ready(function () {
    $(".button-collapse").sideNav();
    $('select').material_select();

    $('ul.tabs.pagination').tabs();
    $('ul.tabs.pagination').tabs('select_tab', 'tab_id');
});
