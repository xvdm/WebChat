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
tumbler.addEventListener("change", function () {
    var link = document.getElementById("theme-link")
    let lightTheme = "/css/light.css";
    let darkTheme = "/css/dark.css";
    var currentTheme = link.getAttribute("href");
    if (currentTheme === lightTheme) {
        currentTheme = darkTheme;
    }
    else {
        currentTheme = lightTheme;
    }
    link.setAttribute("href", currentTheme);
});