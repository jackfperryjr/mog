// Setting a random character name as the input placeholder
let randomCharacterList = [
    'Search ex. "Lightning"',
    'Search ex. "Cloud"',
    'Search ex. "Yuna"',
    'Search ex. "Firion"',
    'Search ex. "Aerith"',
    'Search ex. "Cecil"',
    'Search ex. "Rosa"',
    'Search ex. "Tidus"',
    'Search ex. "Rikku"',
    'Search ex. "Noctis"'
];

let randomCharacterName = randomCharacterList[Math.floor(Math.random()*randomCharacterList.length)];

$("#search").attr("placeholder", randomCharacterName);

// Loading characters once page is ready
$(document).ready(function () {
    getData();
});

// Using Api to get characters
const uri = 'http://localhost:5000/finalfantasy/api';
let characters = null;

// Making list of characters
function getData() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            characters = data;
        }
    });
}

// Finding characters upon search
$('#search').keyup(function() {
    let searchField = $(this).val();
    if(searchField === '') {
        $('#result').html('');
        return;
    }
    let regex = new RegExp(searchField, "i");
    let output = '';
    let count = 1;

        // Display character format
        $.each(characters, function(key, character) {
            if ((character.name.search(regex) != -1)) {
                output += '<div class="row" style="margin-right: 0;margin-left:0;"><div class="col-md-8">';
                output += '<h4 style="color: #2962ff;margin-bottom: -.5px;"><strong>'+ character.name + '</strong></h4>';
                output += '<h7 class="search-result">To learn more visit <a href="https://www.google.com/search?q=final+fantasy+' + character.name.toLowerCase() + '" target="_blank">https://www.google.com/search?q=' + character.name.toLowerCase() + '</a></h7><br/>';
                output += '<p style="color: #37474f">' + character.description + '</p>';
                output += '<table style="color: #37474f; width:50%;"><tr>';
                output += '<td><strong>Age:</strong> ' + character.age + '</td>';
                output += '<td><strong>Race:</strong> ' + character.race + '</td></tr><tr>';
                output += '<td><strong>Job:</strong> ' + character.job + '</td>';
                output += '<td><strong>Height:</strong> ' + character.height + 'm</td></tr><tr>';
                output += '<td><strong>Weight:</strong> ' + character.weight + 'kg</td>';
                output += '<td><strong>Origin:</strong> ' + character.origin + '</td></tr>';
                output += '</table><br/>';
                output += '</div><div class="col-md-4">';
                output += '<img style="width: 200px; padding: 10px; border: 1px solid #e0e0e0; margin: 20px 0; float: right;" src=' + character.picture + ' alt="Picture of ' + character.name  + '." title="Picture of ' + character.name  + '.">';
                output += '<br/></div>';
                if(count%2 == 0) { 
                    output += '</div>'
                }
                count++;
            }
        });
    $('#result').html(output);
});