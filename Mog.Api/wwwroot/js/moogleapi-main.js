$(document).ready(function() {
    if (halfmoon.readCookie("darkModeOn") == "yes") {
        halfmoon.toggleDarkMode();
    } 

    getCount();
});

function getCount() {
    fetch("https://www.moogleapi.com/api/v1/characters")
        .then(function(response) {
            return response.json();
      }).then(function(data) {
            console.log(data.length);
            $("#character-count").text(data.length);
      });
}