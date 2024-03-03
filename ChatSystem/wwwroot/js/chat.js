
//var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

////Disable send button until connection is established
//$("#sendMessage").prop('disabled', true);

//connection.on("ReceiveMessage", function (user, message) {
//    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
//    var encodedMsg = user + " says " + msg;
//    var li = document.createElement("li");
//    li.textContent = encodedMsg;
//    $("#messagesList").append(li);
//});

//connection.start().then(function () {
//    $("#sendMessage").prop('disabled', false);
//}).catch(function (err) {
//    return console.error(err.toString());
//});


//$("#sendMessage").click(function () {

//    var sender = $("#sender").val();
//    var receiver = $("#receiver").val();
//    var message = $("#message").val();
//    console.log(sender, receiver, message);
//    if (receiver != "") {
//        //send to a user
//        connection.invoke("SendMessageToGroup", sender, receiver, message).catch(function (err) {
//            return console.error(err.toString());
//        });
//    }
//    else {
//        //send to all
//        connection.invoke("SendMessage", sender, message).catch(function (err) {
//            return console.error(err.toString());
//        });
//    }


//    event.preventDefault();

//});


"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var sender = document.getElementById("senderInput").value;
    var receiver = document.getElementById("receiverInput").value;
    var message = document.getElementById("messageInput").value;

    if (receiver != "") {

        connection.invoke("SendMessageToGroup", sender, receiver, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        connection.invoke("SendMessage", sender, message).catch(function (err) {
            return console.error(err.toString());
        });
    }


    event.preventDefault();
});
