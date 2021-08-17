$(function () {
    fetchEntries("entriesSelect");
});

function fetchEntries(id) {
    var select = document.getElementById(id);

    $.ajax({
        type: 'GET',
        url: 'Entry/GetAllEntries',
        dataType: 'json',
        success: function (data) {
            data.forEach(function (item, index) {
                var el = document.createElement("option");
                el.textContent = item.uid;
                el.value = JSON.stringify(item);
                select.appendChild(el);
            });

            var locale = data[0].locale;
            updateEntryData(data[0], locale);
            fetchLocales("localesSelect", locale);
        }
    });
}

function fetchEntry(uid, locale, version, onSuccess) {
    var data = {
        uid: uid,
        locale: locale,
        version: version
    }

    $.ajax({
        type: 'GET',
        url: 'Entry/GetEntry',
        dataType: 'json',
        data: data,
        success: onSuccess
    });
}

function localeChanged(data) {
    var uid = JSON.parse($('#entriesSelect').val())["uid"];
    var locale = data.value;
    fetchEntry(uid, locale, null, function (data) {
        if (data.locale == locale) {
            updateEntryData(data, locale);
        }
        else {
            $('#version').removeClass('badge-success');
            $('#version').removeClass('badge-primary');
            $('#version').addClass('badge-secondary');

            $('#version').html("Version " + 1);
        }
    });
}

function updateEntryData(data, locale, includeVersionSelect = true) {
    $('a').removeClass('selected');
    $('.select').removeClass('selected');
    var latestVersion = data._version;
    fetchAssets('assetsTb', data.images);
    setVersion(data._version, locale, data.publish_details);

    if (includeVersionSelect) {
        populateVersionSelect(latestVersion);
    }

    $('#entryTitle').attr('value', data.title);
    $('#linkTitle').attr('value', data.links[0].link.title);
    $('#linkUrl').attr('value', data.links[0].link.href);
    $('#entryTitle').val(data.title);
    $('#linkTitle').val(data.links[0].link.title);
    $('#linkUrl').val(data.links[0].link.href);
    $('#richTextPreview').empty().append(data.texts[0].text);
    $('#summernote').summernote('code', '');
    $('#summernote').summernote('code', data.texts[0].text);
}

function entryChanged(sel) {
    var entryContent = JSON.parse(sel.value);
    var uid = entryContent['uid'];
    var locale = entryContent['locale'];
    $("#localesSelect").val(entryContent["locale"]);

    fetchEntry
        (uid, locale, null, function (data) {
        updateEntryData(data, locale);
    });
}

function setVersion(latestVersion, locale, publishDetails) {
    $('#version').removeClass('badge-success');
    $('#version').removeClass('badge-secondary');
    $('#version').addClass('badge-primary');

    publishDetails.forEach(function (item, index) {
        if (latestVersion == item.version & locale == item.locale) {
            $('#version').removeClass('badge-primary');
            $('#version').removeClass('badge-secondary');
            $('#version').addClass('badge-success');
        }
    });

    $('#version').html("Version " + latestVersion);
}

function populateVersionSelect(latestVersion) {
    var select = document.getElementById("versionSelect");
    $('#versionSelect').empty();

    for (let i = 1; i <= latestVersion; i++) {
        var el = document.createElement("option");
        el.textContent = i;
        el.value = i;
        select.appendChild(el);
    }

    $('#versionSelect').val(latestVersion);
}

function versionChanged(data) {
    var uid = JSON.parse($('#entriesSelect').val())["uid"];
    var locale = $("#localesSelect").val();
    var version = data.value;
    fetchEntry(uid, locale, version, function (data) {
        updateEntryData(data, locale, false);
    });
}

function fetchLocales(id, masterLocale) {
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
                $('select option:contains("' + masterLocale + '")').prop('selected', true);
            });
        }
    });
}

function fetchAssets(id, data) {
    emptyAssetList();
    emptyGlobalAssetList();
    $('#' + id).empty();
    data.forEach(function (item, index) {
        $('#' + id).append(
            '    <div class="col-lg-3 col-md-4 col-6">\n' +
            '        <img class="img-fluid img-thumbnail" src="' + item.image.url + '" name="' + item.image.filename + '" alt="">\n' +
            '    </div>\n'
        )

        appendToAssetList(item.image.uid);
    });
}

function emptyAssetList() {
    $('#assetList').empty();
}

function appendToAssetList(assetUid) {
    $('#assetList').append(
        '    <input id="' + assetUid + '" type="hidden" name="assets[]" value="' + assetUid + '"\>\n'
    )
}

function removeFromAssetList(assetUid) {
    $('#' + assetUid).remove();
} 

function copyEntry() {
    saveEntry(true);
}

function saveEntry(isCopy = false) {
    var entry = JSON.parse($('#entriesSelect').val());
    var richtext = $('#summernote').summernote('code');
    var uid = entry['uid'];
    var title = $('#entryTitle').val();
    var locale = $('#localesSelect').val();
    var linkTitle = $('#linkTitle').val();
    var linkUrl = $('#linkUrl').val();
    var assetsList = $('#assetList input');
    var globalAssetList = $('#globalAssetList input');
    var assets = [];

    for (let i = 0; i < assetsList.length; i++) {
        assets.push(assetsList[i].value);
    }

    for (let i = 0; i < globalAssetList.length; i++) {
        assets.push(globalAssetList[i].value);
    }

    var data = {
        entryTitle: title + '- COPY',
        locale: locale,
        linkTitle: linkTitle,
        linkUrl: linkUrl,
        assets: assets,
        richtext: richtext
    }

    if (!isCopy) {
        data.entryUid = uid;
        data.entryTitle = title;
    }

    $.ajax({
        type: "POST",
        url: "Entry/UpsertEntry",
        dataType: 'json',
        data: data,
        success: function (result) {
            fetchEntry(uid, locale, null, function (data) {
                updateEntryData(data, locale);
            });
        }
    });
}

function publishEntry() {
    var entry = JSON.parse($('#entriesSelect').val());
    var uid = entry["uid"];
    var locale = $('#localesSelect').val();

    var data = {
        locale: locale,
        uid: uid
    }

    $.ajax({
        type: "POST",
        url: "Entry/PublishEntry",
        dataType: 'json',
        data: data,
        success: function (result) {
            fetchEntry(uid, locale, null, function (data) {
                updateEntryData(data, locale);
            });
        }
    });
}