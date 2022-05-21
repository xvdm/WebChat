// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//const input = document.querySelector('input');
//input.addEventListener('change', updateImageDisplay());
//alert("sd");
//input.style.opacity = 0;

//function updateImageDisplay() {
//    alert("success1");
//    const file = input.files[0];
//    if (validFileType(file)) {
//        image.src = URL.createObjectURL(file);
//        alert("success2");
//    }
//    document.getElementById("temp").innerHTML = image.src;
//}

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
        alert("hi");
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