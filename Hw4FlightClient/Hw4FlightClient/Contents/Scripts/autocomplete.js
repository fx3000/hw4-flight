/*jslint  browser: true, white: true, plusplus: true */
/*global $: true */

$(function () {
    'use strict';
    $.ajax({
        url: 'App_Data/AirPort.txt',
        dataType: 'json'
    }).done(function (data) {
        var status = $('#selection'),
            countries = $.map(data, function (value) {
                return value;
            });
        alert(countries);
        $('#placestart').autocomplete({
            lookup: countries,
            onSelect: function (suggestion) {
                status.html('You selected: ' + suggestion);
            }
        });
    });
});