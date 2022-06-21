var tumbler = document.getElementById("themeCheckbox");
var link = document.getElementById("theme-link")

window.onload = function () {
    var name = "theme";
    var theme = document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)')?.pop() || 'null';
    if (theme == "null") {
        link.setAttribute("href", "/css/light.css");
    }
    else { 
        if (tumbler != null) {
            if (theme == "/css/light.css") tumbler.checked = false;
            else tumbler.checked = true;
        }
        link.setAttribute("href", theme);
    }
}

if (tumbler != null) {
    tumbler.addEventListener("change", function () {
        let lightTheme = "/css/light.css";
        let darkTheme = "/css/dark.css";
        var currentTheme = link.getAttribute("href");
        if (currentTheme === darkTheme) {
            currentTheme = lightTheme;
        }
        else {
            currentTheme = darkTheme;
        }
        link.setAttribute("href", currentTheme);
        document.cookie = "theme=" + currentTheme;
    });
}

document.addEventListener('keydown', function (event) {
    if (event.code == 'Enter') {
        document.getElementById("sendMessageButton").focus();
    }
});

function changePhoto() {
    document.getElementById("currentImage").src = URL.createObjectURL(document.getElementById("photoInput").files[0]);
}

function changeChatPhoto() {
    document.getElementById("currentChatImage").src = URL.createObjectURL(document.getElementById("chatPhotoInput").files[0]);
}