var courseIdRaw = window.location.href.match(`Course/[0-9]+`)[0];
var courseId = courseIdRaw.split('/')[1];
$("#courseId").val(courseId);

function ShowTopic(index) {
    var youtubeUrl = $("#youtubeUrl" + index).val();
    $(`<div id="Info` + index + `" class="card-body border-gold rounded-bottom" style="background-color: lightgoldenrodyellow">
    <button id="ShowNote` + index + `" class="btn text-decoration-none  font-weight-bold  text-dark" onclick="ShowNote(` + index + `)"> Notes </button>
    <br />
    <button id="ShowVideoResourse` + index + `" class="btn text-decoration-none text-dark font-weight-bold" onclick="ShowVideoResourse(` + index + `)"> Watch Video </button>
    <br />
</div>`).insertAfter('#Topic' + index).hide().show('fast');
    $('#Topic' + index).attr("onclick", "RemoveTopic("+ index + ")");
}

function RemoveTopic(index) {
    $('#Info' + index).remove('#Info' + index);
    $('#Note' + index).remove('#Note' + index);
    $('#VideoResourse' + index).remove('#VideoResourse' + index);
    $('#Topic' + index).attr("onclick", "ShowTopic(" + index + ")");
}

function ShowNote(index) {
    var notes = $("#note" + index).val();
    $(` <div id="Note${index}" class="card-body mb-3 rounded-bottom rounded-top" style="background-color: #2392a8">
    <label class="text-center font-weight-bold">Notes</label>
      <textarea id="NoteValue${index}" class="container-fluid" rows="4" style="background-color: transparent">${notes}</textarea>
<button onclick="ChangeNoteValues('${index}')" class="btn btn-warning">Save</button>
</div>`).insertAfter('#Info' + index).hide().show('fast');
    $('#ShowNote' + index).attr("onclick", "RemoveNote("+ index +")");
}

function ChangeNoteValues(index) {
    var topic = $("#topic" + index).val();
    var note = $("#NoteValue" + index).val();
    $.get(`/api/change-note?topicId=${topic}&noteValue=${note}`);
}

function RemoveNote(index) {
    $('#Note' + index).remove('#Note' + index);
    $('#ShowNote' + index).attr("onclick", "ShowNote(" + index + ")");
}

function ShowVideoResourse(index) {
    var youtubeUrl = $("#youtubeUrl" + index).val();
    $(`<div id="VideoResourse` + index + `" class="text-center">
    <iframe width="100%" height="450px" src="${youtubeUrl}" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</div>`).insertAfter('#Info' + index).hide().show('slow');
    $('#ShowVideoResourse' + index).attr("onclick", "RemoveVideoResourse("+ index + ")");

    $('html,body').animate({ scrollTop: $("#" + 'VideoResourse' + index).offset().top }, 'slow'); // scrolling to fragment with id
}

function MakeVideoFullScreen() {

}

function RemoveVideoResourse(index) {
    $('#VideoResourse' + index).remove('#VideoResourse' + index);
    $('#ShowVideoResourse' + index).attr("onclick", "ShowVideoResourse(" + index + ")");
}
