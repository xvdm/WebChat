function sendMessage() {
    var message = $("#messageInput").serialize();
    $.ajax({
        type: 'POST',
        url: '/Home/SendMessage',
        data: message,
        success: function (result) {
            $("#messages").html(result);
            $.ajax({
                type: 'GET',
                url: '/Home/UpdateChatsList',
                success: function (result) {
                    $("#chats").html(result);
                    document.getElementById("messageInput").focus();
                }
            })
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
            $.ajax({
                type: 'GET',
                url: '/Home/UpdateChatsList',
                success: function (result) {
                    $("#chats").html(result);
                }
            })
            $.ajax({
                type: 'GET',
                url: '/Home/UpdateChatSettings',
                success: function (result) {
                    $("#settings").html(result);
                    document.getElementById("messageInput").focus();
                }
            })
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
            document.getElementById("messageInput").focus();
        }
    })
}

function leaveChat() {
    var captcha = $("#captchaForLeavingChat").serialize();
    $.ajax({
        type: 'POST',
        url: '/Home/LeaveChat',
        data: captcha,
        success: function () {
            $.ajax({
                type: 'GET',
                url: '/Home/UpdateChatsList',
                success: function (result) {
                    $("#chats").html(result);
                }
            })
            $.ajax({
                type: 'GET',
                url: '/Home/UpdateMessages',
                success: function (result) {
                    $("#messages").html(result);
                }
            })
            $.ajax({
                type: 'GET',
                url: '/Home/UpdateChatSettings',
                success: function (result) {
                    $("#settings").html(result);
                    document.getElementById("messageInput").focus();
                }
            })
        }
    })
}

function addUserToChat() {
    var login = $("#loginAddToChat").serialize();
    $.ajax({
        type: 'POST',
        url: '/Home/AddUserToChat',
        data: login,
        success: function () {
            $.ajax({
                type: 'GET',
                url: '/Home/UpdateChatSettings',
                success: function (result) {
                    $("#settings").html(result);
                }
            })
            $.ajax({
                type: 'GET',
                url: '/Home/UpdateMessages',
                success: function (result) {
                    $("#messages").html(result);
                    document.getElementById("messageInput").focus();
                }
            })
        }
    })
}

