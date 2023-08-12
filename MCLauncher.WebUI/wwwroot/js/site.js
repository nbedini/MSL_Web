// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Logout(username) {
    var url = "https://localhost:7172/Auth/Logout/" + username
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == true) {
                window.location = "Home"
            }
            else {
                alert("Wrong");
            }

        }
    });
}