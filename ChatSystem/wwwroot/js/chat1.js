
function getFormattedDateTime(date) {
    const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    const day = ('0' + date.getDate()).slice(-2);
    const month = months[date.getMonth()];
    const year = date.getFullYear();
    let hours = date.getHours();
    const minutes = ('0' + date.getMinutes()).slice(-2);
    const seconds = ('0' + date.getSeconds()).slice(-2);
    const meridiem = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // Handle midnight
    const time = ('0' + hours).slice(-2) + ':' + minutes + ':' + seconds + ' ' + meridiem;
    return day + '/' + month + '/' + year + ' : ' + time;
}



var senderId = $("#senderId").val();
var connection = new signalR.HubConnectionBuilder().withUrl(`/chatHub?senderId=${senderId}`).build();

//Disable send button until connection is established
$("#sendMessage").prop('disabled', true);



connection.on("ReceiveMessage", function (user, message, dateTime) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = `<span class="h5 mb-0 text-danger"><b>${user}:</b> ${message}</span>
                        <span style="font-size:10px;">${dateTime}</span>`;
    var li = document.createElement("li");
    li.classList = "align-items-start d-flex flex-column list-group-item";
    li.innerHTML = encodedMsg;
    $("#messagesList").append(li);
    $("#message").val('');
});

connection.start().then(function () {
    $("#sendMessage").prop('disabled', false);
    //let connectionId = connection.connectionId;

    //$("#myConnectionId").text(connectionId);
    //console.log("Connection ID:", connectionId);
    
  
}).catch(function (err) {
    return console.error(err.toString());
});


$("#sendMessage").click(function () {
    var connectionId = connection.connectionId;//$("#connectionId").val();
    var senderId = $("#senderId").val();
    var sender = $("#sender").val();
    var receiverId = $("#receiverId").val();
    var receiver = $("#receiver").val();
    var message = $("#message").val();
    var dateTime = new Date();

    var encodedMsg = `<span class="h5 mb-0 text-success"><b>${sender}:</b> ${message}</span>
                        <span style="font-size:10px;">${getFormattedDateTime(dateTime)}</span>`;
    var li = document.createElement("li");
    li.classList = "align-items-end d-flex flex-column list-group-item";
    li.innerHTML = encodedMsg;
    $("#messagesList").append(li);
    $("#message").val('');


    if (message != "") {
        //send to a user
        connection.invoke("SendMessageToGroup", senderId, receiverId, sender, message, dateTime).catch(function (err) {
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