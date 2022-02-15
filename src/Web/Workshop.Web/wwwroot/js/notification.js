var notifyConnection =
    new signalR.HubConnectionBuilder()
        .withUrl("/notify")
        .build();

notifyConnection.start().catch(function (err) {
    return console.error(err.toString());
});

notifyConnection.on("NewNotification",
    function (message) {
        var count = $('.toast').length + 1;
        var toastId = 'toast' + count;
        if (count > 5) {
            var toastToRemove = document.getElementsByClassName('toast')[0].getAttribute('id');
            console.log(toastToRemove);
            $("#" + toastToRemove).remove();
        }

        var miliseconds = 10 * 1000;

        document.getElementById("notifications").innerHTML = document.getElementById("notifications").innerHTML +
            ` <div id="${toastId}" class="toast border-all-borders-gold" role="alert" aria-live="assertive" aria-atomic="true" data-delay="${miliseconds}" style="background-color: goldenrod !important;">
              <div class="toast-header" style="background-color: #3f474e !important">
              <img src="/siteLogo.jpg" class="rounded mr-2" alt="/siteLogo.jpg" width="20" height="20">
              <strong class="mr-auto text-green pl-1 pr-5">${message.sender}</strong>
              <button type="button" class="ml-2 mb-1 close " data-dismiss="toast" aria-label="Close">
              <span aria-hidden="true" class="text-warning">&times;</span>
              </button>
              </div>
              <a class="text-decoration-none" href="${message.link}">
              <div class="toast-body text-warning" style="background-color: #3f474e !important; word-wrap: break-word;">
              ${message.message}
              </div>
              </div>
              </a>`;

        $('.toast').toast('show').then(setTimeout(function () { $("#" + toastId).remove(); }, miliseconds));
    }
);