$(document).on('change', "#image1", function () {
    let id = $(this).parent().attr('id');
    var files = !!this.files ? this.files : [];
    if (!files.length || !window.FileReader) return;
    if (/^image/.test(files[0].type)) {
        var ReaderObj = new FileReader();
        ReaderObj.readAsDataURL(files[0]);
        ReaderObj.onloadend = function () {
            $("#" + id).css("background-image", "url(" + this.result + ")");
        }
    } else {
        alert("Upload an image");
    }
});
