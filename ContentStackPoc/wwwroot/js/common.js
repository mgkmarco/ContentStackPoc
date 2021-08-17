$(function () {
    fetchGlobalAssets("globalAssetsTb");
    $('#upload').on('change', function () {
        readURL(input);
    });
    $('#summernote').summernote({
        tabsize: 2,
        height: 120,
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'italic', 'underline', 'clear']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['insert', ['link']],
            ['view', ['fullscreen', 'codeview', 'help']]
        ]
    });
});

function fetchGlobalAssets(id) {
    $.ajax({
        type: 'GET',
        url: 'Home/GetAssets',
        dataType: 'json',
        success: function (data) {
            data.forEach(function (item, index) {
                $('#' + id).append(
                    '    <div class="col-lg-3 col-md-4 col-6">\n' +
                    '      <a href="#" id="globalAsset_' + index + '" onclick="selectGlobalAsset(\'' + index + '\', \'' + item.uid + '\')" class="selectedAsset d-block mb-4 h-100">\n' +
                    '        <img class="img-fluid img-thumbnail" src="' + item.url + '" name="' + item.uid + '" alt="">\n' +
                    '      </a>\n' +
                    '    </div>\n'
                );
            });
        }
    });
}

function uploadGlobalAsset() {
    var dataUrl = $('#imageResult').attr('src').toString();
    var base64result = dataUrl.split(',')[1];
    var dataType = dataUrl.split(';')[0].split(':')[1];
    var title = $('#globalAssetTitle').val();

    var data = {
        title: title,
        type: dataType,
        base64Encoded: base64result
    }

    $.ajax({
        type: "POST",
        url: "Home/UploadAsset",
        dataType: 'json',
        data: data,
        success: function (result) {
            $('#imageResult').attr('src', '#');
            var uid = result.uid;
            fetchGlobalAsset('globalAssetsTb', uid);
        }
    });
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imageResult')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}


var input = document.getElementById('upload');
var infoArea = document.getElementById('upload-label');

input.addEventListener('change', showFileName);
function showFileName(event) {
    var input = event.srcElement;
    var fileName = input.files[0].name;
    infoArea.textContent = 'File name: ' + fileName;
}

function removeFromGlobalAssetList(assetUid) {
    $('#global_' + assetUid).remove();
}

function selectGlobalAsset(index, assetUid) {
    $('#globalAsset_' + index).toggleClass('selected');
    removeFromGlobalAssetList(assetUid);

    if ($('#globalAsset_' + index).hasClass('selected')) {
        appendToGlobalAssetList(assetUid);
    }

    if ($('a.selected').length == 0) {
        $('.select').removeClass('selected');
    }
    else {
        $('.select').addClass('selected');
    }
}

function appendToGlobalAssetList(assetUid) {
    $('#globalAssetList').append(
        '    <input id="global_' + assetUid + '" type="hidden" name="assets[]" value="' + assetUid + '"\>\n'
    )
}

function emptyGlobalAssetList() {
    $('#globalAssetList').empty();
}

function fetchGlobalAsset(id, assetUid) {
    $.ajax({
        type: 'GET',
        url: 'Home/GetAsset',
        data: { uid: assetUid },
        dataType: 'json',
        success: function (data) {
            $('#' + id).append(
                '    <div class="col-lg-3 col-md-4 col-6">\n' +
                '      <a href="#" id="globalAsset_' + data.uid + '" onclick="selectGlobalAsset(\'' + data.uid + '\', \'' + data.uid + '\')" class="selectedAsset d-block mb-4 h-100">\n' +
                '        <img class="img-fluid img-thumbnail" src="' + data.url + '" name="' + data.uid + '" alt="">\n' +
                '      </a>\n' +
                '    </div>\n'
            );
        }
    });
}