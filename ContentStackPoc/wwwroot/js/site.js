// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    fetchLocales("localesSelect");
});

function fetchLocales(id) {
    var select = document.getElementById(id);

    $.ajax({
        type: 'GET',
        url: 'Home/GetLanguages',
        dataType: 'json',
        success: function (data) {
            data.forEach(function (item, index) {
                var el = document.createElement("option");
                el.textContent = item.code;
                el.value = item.code;
                select.appendChild(el);
            });
        }
    });
}

function createEntry() {
    var title = $('#entryTitle').val();
    var richtext = $('#summernote').summernote('code');
    var locale = $('#localesSelect').val();
    var linkTitle = $('#linkTitle').val();
    var linkUrl = $('#linkUrl').val();
    var globalAssetList = $('#globalAssetList input');
    var assets = [];

    for (let i = 0; i < globalAssetList.length; i++) {
        assets.push(globalAssetList[i].value);
    }

    var data = {
        entryTitle: title,
        locale: locale,
        linkTitle: linkTitle,
        linkUrl: linkUrl,
        assets: assets,
        richText: richtext
    }

    $.ajax({
        type: "POST",
        url: "Home/CreateEntry",
        dataType: 'json',
        data: data,
        success: function (result) {
        }
    });
}