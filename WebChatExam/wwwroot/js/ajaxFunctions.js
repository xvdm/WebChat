function openChatsPage() {
    $.ajax({
        type: 'GET',
        url: '/Home/Chats',
        success: function (result) {
            $("#IndexDiv").html(result);
        }
    })
}

function openSettingsPage() {
    $.ajax({
        type: 'GET',
        url: '/Home/Settings',
        success: function (result) {
            $("#IndexDiv").html(result);
        }
    })
}

function sendMessage() {
    var message = $("#messageInput").serialize();
    $.ajax({
        type: 'POST',
        url: '/Home/SendMessage',
        data: message,
        async: false,
        success: function (result) {
            $("#messages").html(result);
        }
    })
    $.ajax({
        type: 'GET',
        url: '/Home/Chats',
        async: false,
        success: function (result) {
            $("#IndexDiv").html(result);
            document.getElementById("messageInput").focus();
        }
    })
}

function openChat(id) {
    $.ajax({
        traditional: true,
        type: 'POST',
        url: '/Home/OpenChat',
        data: { "id": id },
        success: function (result) {
            $("#messages").html(result);
            document.getElementById("messageInput").focus();
        }
    })
}

function createChat() {
    var chatName = $("#createChatInput").serialize();
    $.ajax({
        type: 'POST',
        url: '/Home/CreateChat',
        data: chatName,
        success: function (result) {
            $("#chats").html(result);
        }
    })
}

function leaveChat() {
    var captcha = $("#captchaForLeavingChat").serialize();
    $.ajax({
        type: 'POST',
        url: '/Home/LeaveChat',
        data: captcha,
        success: function (result) {
            $("#IndexDiv").html(result);
        }
    })
}

function addUserToChat() {
    var login = $("#loginAddToChat").serialize();
    $.ajax({
        type: 'POST',
        url: '/Home/AddUserToChat',
        data: login,
        success: function (result) {
            $("#IndexDiv").html(result);
        }
    })
}

