var notifyConnection =
    new signalR.HubConnectionBuilder()
        .withUrl("/notifications")
        .build();

notifyConnection.start().catch(function (err) {
    return console.error(err.toString());
});

notifyConnection.on("NewNotification",
    function (notification) {
        var count = $('.toast').length + 1;
        var toastId = 'toast' + count;
        if (count > 5) {
            var toastToRemove = document.getElementsByClassName('toast')[0].getAttribute('id');

            $("#" + toastToRemove).remove();
        }

        var miliseconds = 10 * 1000;

        document.getElementById("notifications").innerHTML = document.getElementById("notifications").innerHTML +
            ` <div id="${toastId}" class="toast border-all-borders-gold" role="alert" aria-live="assertive" aria-atomic="true" data-delay="${miliseconds}" style="background-color: goldenrod !important;">
              <div class="toast-header" style="background-color: #3f474e !important">
              <img src="${notification.senderAvatarUrl}" class="rounded mr-2" width="20" height="20">
              <strong class="mr-auto text-green pl-1 pr-5">${notification.senderUserName}</strong>
              <button type="button" class="ml-2 mb-1 close " data-dismiss="toast" aria-label="Close">
              <span aria-hidden="true" class="text-warning">&times;</span>
              </button>
              </div>
              <a class="text-decoration-none" href="${notification.link}">
              <div class="toast-body text-warning" style="background-color: #3f474e !important; word-wrap: break-word;">
              ${notification.description}
              </div>
              </div>
              </a>`;
       
        if ($("#notificationsCount").val() === undefined) {
            $(`<span id="notificationsCount" class="badge-custom">1</span>`).insertAfter("#placeNotificationsCountAfterMe");

        }
        else {
            $("#notificationsCount").html(parseInt($("#notificationsCount").html()) + 1 );
        }
       
        $('.toast').toast('show').then(setTimeout(function () { $("#" + toastId).remove(); }, miliseconds));
    }
);

$('#myDropDown').on('show.bs.dropdown', function () {
    UpdateNotificationStatus();
})
$('#myDropDown').on('hide.bs.dropdown', function () {
    UnmarkUnreadNotifications();
})

function UpdateNotificationStatus() {
    var apiUrl = `/api/notifications`;
    $.ajax({
        url: apiUrl,
        type: 'get',
        datatype: 'json',
        success: function (response) {
            let result = '';

            for (var i = 0; i < response.length; i++) {
                result += ` <a  class="dropdown-item" href="${response[i].link}" style="background-color: RGBA(255,208,0,0.4) !important">
                            <div class="text-green">
                            <img src="${response[i].senderAvatarUrl}" alt="avatar" width="20" height="20">
                            ${response[i].senderUserName}
                            <span class="text-chocolate small">${response[i].createdOn}</span>
                            </div>
                            <small class="wordwrap text-warning">${response[i].description}</small>
                            </a>`;
            }
            $("#dropdownMenuButton").append(`<input id="newNotificationsCount" type="hidden" value="${response.length}"/>`)
            document.getElementById("notifications1").innerHTML = result += document.getElementById("notifications1").innerHTML
           
        }
    });
}

function UnmarkUnreadNotifications() {
    var items = document.getElementsByClassName('dropdown-item');
    var count = $("#newNotificationsCount").val() + 1;

    for (var i = 0; i < count; i++) {
        items[i].removeAttribute("style");
    }
    $("#newNotificationsCount").remove();
    $("#notificationsCount").remove();
}