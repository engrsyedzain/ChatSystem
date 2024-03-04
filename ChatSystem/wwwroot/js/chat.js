
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
$("#sendMessage").prop('disabled', true);



connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    $("#messagesList").append(li);
});



connection.start().then(function () {
    $("#sendMessage").prop('disabled', false);
    let connectionId = connection.connectionId;

    $("#myConnectionId").text(connectionId);
    console.log("Connection ID:", connectionId);
    
  
}).catch(function (err) {
    return console.error(err.toString());
});


$("#sendMessage").click(function () {
    var connectionId = $("#connectionId").val();
    var sender = $("#sender").val();
    var receiver = $("#receiver").val();
    var message = $("#message").val();

    var encodedMsg = sender + " says " + message;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    $("#messagesList").append(li);



    if (connectionId != "") {
        //send to a user
        connection.invoke("SendMessageToGroup", sender, receiver, message, connectionId).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        //send to all
        connection.invoke("SendMessage", sender, message).catch(function (err) {
            return console.error(err.toString());
        });
    }


    event.preventDefault();

});

