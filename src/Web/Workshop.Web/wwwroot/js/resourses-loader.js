
function ShowTopic(index) {
    $(`<div id="Info` + index + `" class="card-body border-gold rounded-bottom" style="background-color: lightgoldenrodyellow">
    <button id="ShowNote` + index + `" class="btn text-decoration-none  font-weight-bold  text-dark" onclick="ShowNote(` + index + `)"> Notes </button>
    <br />
    <button id="ShowVideoResourse` + index + `" class="btn text-decoration-none text-dark font-weight-bold" onclick="ShowVideoResourse(` + index + `)"> Watch Here </button>
    <br />
    <a class="btn text-decoration-none text-dark font-weight-bold" target="_blank" href="https://www.youtube.com/watch?v=Jdi0uEWaRkc">Youtube Link: Timmy Trumpet Live LA </a>
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
    $(` <div id="Note` + index + `" class="card-body mb-3 rounded-bottom rounded-top" style="background-color: burlywood">
    <label class="text-center font-weight-bold">Notes</label>
    <div class="container">
        <div>braking array</div>
    </div>
</div>`).insertAfter('#Info' + index).hide().show('fast');
    $('#ShowNote' + index).attr("onclick", "RemoveNote("+ index +")");
}
function RemoveNote(index) {
    $('#Note' + index).remove('#Note' + index);
    $('#ShowNote' + index).attr("onclick", "ShowNote(" + index + ")");
}

function ShowVideoResourse(index) {
    $(`<div id="VideoResourse` + index + `" class="text-center">
    <iframe width="100%" height="315" src="https://www.youtube.com/embed/Jdi0uEWaRkc" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</div>`).insertAfter('#Info' + index).hide().show('slow');
    $('#ShowVideoResourse' + index).attr("onclick", "RemoveVideoResourse("+ index + ")");

    $('html,body').animate({ scrollTop: $("#" + 'VideoResourse' + index).offset().top }, 'slow'); // scrolling to fragment with id
}
function RemoveVideoResourse(index) {
    $('#VideoResourse' + index).remove('#VideoResourse' + index);
    $('#ShowVideoResourse' + index).attr("onclick", "ShowVideoResourse(" + index + ")");
}
