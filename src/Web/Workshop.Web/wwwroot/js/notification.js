var notifyConnection =
    new signalR.HubConnectionBuilder()
        .withUrl("/notify")
        .build();

notifyConnection.start().catch(function (err) {
    return console.error(err.toString());
});

notifyConnection.on("NewNotification",
    function (message) {
        var toastLiveExample = document.getElementById('liveToast');
        var toast = new bootstrap.Toast(toastLiveExample);
        $("#notifyMessage").empty();
        $("#notificationSender").empty();
        $("#notificationLink").val('');
        $("#notificationLink").val(message.link);
        $("#notifyMessage").append(message.message);
        $("#notificationSender").append(message.sender);
        toast.show();
    });

$("#notification").click(function () {
    var link = $("#notificationLink").val();
    window.location = link;
});