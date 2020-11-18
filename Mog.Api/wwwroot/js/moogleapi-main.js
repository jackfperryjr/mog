$(document).ready(function() {
    if (halfmoon.readCookie("darkModeOn") == "yes") {
        halfmoon.toggleDarkMode();
    } 

    getCount();
    setInterval(getCount, 300000);
});

function getCount() {
    fetch("https://www.moogleapi.com/api/v1/characters")
        .then(function(response) {
            return response.json();
      }).then(function(data) {
            console.log("Character count: " + data.length);
            $("#character-count").text(data.length);
      });
}