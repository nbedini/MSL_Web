﻿function Logout(username) {
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

function ServerSettings(serverName) {
    var url = "https://localhost:7172/SelectServer/" + serverName
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == true) {
                window.location = "ServerManagement/" + serverName
            }
            else {
                alert("Wrong");
            }

        }
    });
}