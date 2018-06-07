// Setting a random character name as the input placeholder
let randomPlaceholderList = [
    'Search ex. "Lightning"',
    'Search ex. "Cloud"',
    'Search ex. "Yuna"',
    'Search ex. "Firion"',
    'Search ex. "Aerith"',
    'Search ex. "Cecil"',
    'Search ex. "Rosa"',
    'Search ex. "Tidus"',
    'Search ex. "Rikku"',
    'Search ex. "Cid"'
];

let randomPlaceholder = randomPlaceholderList[Math.floor(Math.random()*randomPlaceholderList.length)];

$("#search").attr("placeholder", randomPlaceholder);

// Loading characters once page is ready
$(document).ready(function () {
    timer();
    getCharacters();
    getMonsters();
    getGames();
});

// Using Api to get characters
const uriCharacters = 'http://localhost:5000/api/characters';
const uriMonsters = 'http://localhost:5000/api/monsters';
const uriGames = 'http://localhost:5000/api/games';
let characters = null;
let monsters = null;
let games = null;

// Making list of characters
function getCharacters() {
    $.ajax({
        type: 'GET',
        url: uriCharacters,
        success: function (characterData) {
            getCountC(characterData.length);
            characters = characterData;
        }
    });
}
function getMonsters() {
    $.ajax({
        type: 'GET',
        url: uriMonsters,
        success: function (monsterData) {
            getCountM(monsterData.length);
            monsters = monsterData;
        }
    });
}
function getGames() {
    $.ajax({
        type: 'GET',
        url: uriGames,
        success: function (gameData) {
            getCountG(gameData.length);
            games = gameData;
        }
    });
}

// Counting the characters
function getCountC(characterData) {
    const el = $('#counterC');
    let name = ' character.';
    if (characterData) {
        if (characterData > 1) {
            name = ' characters.';
        }
        el.text(characterData + name);
    } else {
        el.html('No ' + name);
    }
}
function getCountM(monsterData) {
    const el = $('#counterM');
    let name = ' monster.';
    if (monsterData) {
        if (monsterData > 1) {
            name = ' monsters.';
        }
        el.text(monsterData + name);
    } else {
        el.html('No ' + name);
    }
}
function getCountG(gameData) {
    const el = $('#counterG');
    let name = ' game.';
    if (gameData) {
        if (gameData > 1) {
            name = ' games.';
        }
        el.text(gameData + name);
    } else {
        el.html('No ' + name);
    }
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
                output += '</table><br/></div>';
                output += '<button type="button" style="background:transparent; padding: 10px; border: 1px solid #e0e0e0; margin: 20px 0; float: right;" data-toggle="modal" data-target="#imageModal">';
                output += '<img style="width: 200px;" src=' + character.picture + ' alt="Picture of ' + character.name  + '." title="Picture of ' + character.name  + '."></button>';
                output += '<div id="imageModal" class="modal fade" role="dialog">';
                output += '<div class="modal-dialog">';
                output += '<div class="modal-content">';
                output += '<div class="modal-header">';
                output += '<button type="button" class="close" data-dismiss="modal">X</button>';
                output += '</div>';
                output += '<div class="modal-body">';
                output += '<img style="width: 100%;" src=' + character.picture + ' alt="Picture of ' + character.name  + '." title="Picture of ' + character.name  + '."></div>';
                output += '</div></div></div>';
                output += '<br/></div></div>';
                if(count%2 == 0) { 
                    output += '</div>'
                }
                count++;
            }
        });
        $.each(monsters, function(key, monster) {
            if ((monster.name.search(regex) != -1)) {
                output += '<div class="row" style="margin-right: 0;margin-left:0;"><div class="col-md-8">';
                output += '<h4 style="color: #2962ff;margin-bottom: -.5px;"><strong>'+ monster.name + '</strong></h4>';
                output += '<h7 class="search-result">To learn more visit <a href="https://www.google.com/search?q=final+fantasy+' + monster.name.toLowerCase() + '" target="_blank">https://www.google.com/search?q=' + monster.name.toLowerCase() + '</a></h7><br/>';
                output += '<p style="color: #37474f">' + monster.description + '</p>';
                output += '<table style="color: #37474f; width:50%;"><tr>';
                output += '<td><strong>Strength:</strong> ' + monster.strength + '</td>';
                output += '<td><strong>Weakness:</strong> ' + monster.weakness + '</td></tr></table>';
                output += '</div><div class="col-md-4">';
                output += '<button type="button" style="background:transparent; padding: 10px; border: 1px solid #e0e0e0; margin: 20px 0; float: right;" data-toggle="modal" data-target="#imageModal">';
                output += '<img style="width: 200px;" src=' + monster.picture + ' alt="Picture of ' + monster.name  + '." title="Picture of ' + monster.name  + '."></button>';
                output += '<div id="imageModal" class="modal fade" role="dialog">';
                output += '<div class="modal-dialog">';
                output += '<div class="modal-content">';
                output += '<div class="modal-header">';
                output += '<button type="button" class="close" data-dismiss="modal">X</button>';
                output += '</div>';
                output += '<div class="modal-body">';
                output += '<img style="width: 100%;" src=' + monster.picture + ' alt="Picture of ' + monster.name  + '." title="Picture of ' + monster.name  + '."></div>';
                output += '</div></div></div>';
                output += '<br/></div></div>';
                if(count%2 == 0) { 
                    output += '</div>'
                }
                count++;
            }
        });
        $.each(games, function(key, game) {
            if ((game.title.search(regex) != -1)) {
                output += '<div class="row" style="margin-right: 0;margin-left:0;"><div class="col-md-8">';
                output += '<h4 style="color: #2962ff;margin-bottom: -.5px;"><strong>'+ game.title + '</strong></h4>';
                output += '<h7 class="search-result">To learn more visit <a href="https://www.google.com/search?q=final+fantasy+' + game.title.toLowerCase() + '" target="_blank">https://www.google.com/search?q=' + game.title.toLowerCase() + '</a></h7><br/>';
                //output += '<p style="color: #37474f">' + game.description + '</p>';
                output += '</div><div class="col-md-4">';
                output += '<img style="width: 200px; padding: 10px; border: 1px solid #e0e0e0; margin: 20px 0; float: right;" src=' + game.picture + ' alt="Logo for ' + game.title + '." title="Picture of ' + game.title + '.">';
                output += '<br/></div></div>';
                if(count%2 == 0) { 
                    output += '</div>'
                }
                count++;
            }
        });
    $('#result').html(output);
});

// Loading bar while counting
function timer() {
    let timed;
    timed = setTimeout(showTotals, 3000);
}
function showTotals() {
    $('#loader').fadeOut();
    $('#totals').delay(700).fadeIn();
}