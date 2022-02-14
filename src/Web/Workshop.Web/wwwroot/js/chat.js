﻿var connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

function LoadUser(id) {
    window.location = '/Chat/Private/' + id;
};

connection.start().catch(function (err) {
    return console.error(err.toString());
});

var sender = document.getElementById("messageInput");
sender.addEventListener("keydown", function (e) {
    if (e.code === "Enter" || e.which == 13) {
        Sender();
    }
});

setTimeout(function () { Join(); }, 1000);

function Join() {
    var url = window.location.pathname;
    var id = url.substring(url.lastIndexOf('/') + 1);
    $("#ToUserWithId").val(id);
    connection.invoke("Add", id);
};

connection.on("NewMessage",
    function (message) {
        var currentUserMessage =
            `<li name="message" class="clearfix">
                               <div class="message-data">
                               <span> <a href="javascript:void(0);" data-toggle="modal" data-target="#view_info">
                               <img src="https://bootdey.com/img/Content/avatar/avatar2.png" alt="avatar">
                               </a></span>
                               <div class="message my-message">${escapeHtml(message.messageText)}
                               <div class="text-darkgold" style="font-size: 70%">${message.createdOn}</div>
                               </div>
                               </div>
                               </li>`;

        var otherPersonMessage =
            `<li name="message" class="clearfix">
                                <div class="message-data text-right">
                                <a class="float-right" href="javascript:void(0);" data-toggle="modal" data-target="#view_info">
                                <img src="https://bootdey.com/img/Content/avatar/avatar2.png" alt="avatar">
                                </a>
                                <div class="message other-message float-right">${escapeHtml(message.messageText)}
                                <div class="text-darkgold" style="font-size: 70%">${message.createdOn}</div></div>
                                </div>
                                </li>`;

        if ($("h6#Username").html() !== message.username) {
            var result = currentUserMessage;
        }
        else {
            var result = otherPersonMessage;
        }

        $("#messagesList").append(result);

        var snd = new Audio("data:audio/wav;base64,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");
        snd.volume = 0.02;
        snd.play();

        $("li").last()[0].scrollIntoView({ behavior: "smooth" });
    });

$("#sendButton").click(Sender);

function Sender() {
    var messageInput = $("#messageInput").val();
    if (messageInput.trim().length !== 0) {
        var id = $("#ToUserWithId").val();
        connection.invoke("Send", id, messageInput);
        $("#messageInput").val("");
    }
};

function escapeHtml(unsafe) {
    return unsafe
        .replace(':)', "🙂")
        .replace(':(', "🙁")
        .replace(';)', "😉")
        .replace(':*', "😘")
        .replace(/<3/g, "❤")
        .replace(/:D/g, "😀")
        .replace(/:d/g, "😀")
        .replace(/:p/g, "😜")
        .replace(/:P/g, "😜")
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}
