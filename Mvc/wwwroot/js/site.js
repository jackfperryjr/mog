// List for setting a random search example name as the input placeholder
let randomPlaceholderList = [
    "Search ex. ''Lightning'' or ''13''",
    "Search ex. ''Cloud'' or ''07''",
    "Search ex. ''Yuna'' or ''10''",
    "Search ex. ''Firion'' or ''02''",
    "Search ex. ''Aerith'' or ''07''",
    "Search ex. ''Cecil'' or ''04''",
    "Search ex. ''Rosa'' or ''04''",
    "Search ex. ''Tidus'' or ''10''",
    "Search ex. ''Paine'' or ''10-2''",
    "Search ex. ''Cid'' or, actually, there's a Cid in most games"
];

// Randomly choosing from the list
let randomPlaceholder = randomPlaceholderList[Math.floor(Math.random()*randomPlaceholderList.length)];

$("#search").attr("placeholder", randomPlaceholder);

// Loading data once page is ready
$(document).ready(function () {
    timer();
    getCharacters();
    getMonsters();
    getGames();
});

// Using APIs to get data
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

// Making list of monsters
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

// Making list of games
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

// Counting the monsters
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

// Counting the games
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

// Finding data upon search
$('#search').keyup(function() {
    let searchField = $(this).val();
    if(searchField === '') {
        $('#result').html('');
        return;
    }
    let regex = new RegExp(searchField, "i");
    let output = '';
    let count = 1;

        // Format for character display
        $.each(characters, function(key, character) {
            if ((character.name.search(regex) != -1)) {
                output += '<div class="row moogle-row"><div class="col-md-8">';
                output += '<h4 class="moogle-h4"><strong>'+ character.name + '</strong></h4>';
                output += '<h7 class="search-result">To learn more visit <a href="https://www.google.com/search?q=final+fantasy+' + character.name.toLowerCase() + '" target="_blank">https://www.google.com/search?q=' + character.name.toLowerCase() + '</a></h7><br/>';
                output += '<p class="dark-bluish-gray">' + character.description + '</p>';
                output += '<table class="moogle-table"><tr>';
                output += '<td><strong>Age:</strong> ' + character.age + '</td>';
                output += '<td><strong>Race:</strong> ' + character.race + '</td></tr><tr>';
                output += '<td><strong>Job:</strong> ' + character.job + '</td>';
                output += '<td><strong>Height:</strong> ' + character.height + 'm</td></tr><tr>';
                output += '<td><strong>Weight:</strong> ' + character.weight + 'kg</td>';
                output += '<td><strong>Origin:</strong> ' + character.origin + '</td></tr>';
                output += '</table><br/></div>';
                output += '<div class="col-md-4">';
                output += '<button type="button" class="moogle-modal-button" data-toggle="modal" data-target="#imageModal">';
/*Click img*/   output += '<img class="moogle-img" src=' + character.picture + ' alt="Picture of ' + character.name  + '." title="Picture of ' + character.name  +                              '."><br/><caption>Click to view</caption></button>';
                output += '<div id="imageModal" class="modal fade" role="dialog">';
                output += '<div class="modal-dialog">';
                output += '<div class="modal-content">';
                output += '<div class="modal-header">';
                output += '<button type="button" class="close" data-dismiss="modal">X</button>';
                output += '</div>';
                output += '<div class="modal-body">';
/*Full img*/    output += '<img class="moogle-modal-img" src=' + character.picture + ' alt="Picture of ' + character.name  + '." title="Picture of ' +                                          character.name + '."></div>';
                output += '</div></div></div>';
                output += '<br/></div></div>';
                if(count%2 == 0) { 
                    output += '</div>'
                }
                count++;
            }
        });

        // Format for monster display
        $.each(monsters, function(key, monster) {
            if ((monster.name.search(regex) != -1)) {
                output += '<div class="row moogle-row"><div class="col-md-8">';
                output += '<h4 class="moogle-h4"><strong>'+ monster.name + '</strong></h4>';
                output += '<h7 class="search-result">To learn more visit <a href="https://www.google.com/search?q=final+fantasy+' + monster.name.toLowerCase() + '" target="_blank">https://www.google.com/search?q=' + monster.name.toLowerCase() + '</a></h7><br/>';
                output += '<p class="dark-bluish-gray">' + monster.description + '</p>';
                output += '<table class="moogle-table"><tr>';
                output += '<td><strong>Strength:</strong> ' + monster.strength + '</td>';
                output += '<td><strong>Weakness:</strong> ' + monster.weakness + '</td></tr></table><br/>';
                output += '</div><div class="col-md-4">';
                output += '<button type="button" class="moogle-modal-button" data-toggle="modal" data-target="#imageModal">';
                output += '<img class="moogle-img" src=' + monster.picture + ' alt="Picture of ' + monster.name  + '." title="Picture of ' + monster.name  + '."></button>';
                output += '<div id="imageModal" class="modal fade" role="dialog">';
                output += '<div class="modal-dialog">';
                output += '<div class="modal-content">';
                output += '<div class="modal-header">';
                output += '<button type="button" class="close" data-dismiss="modal">X</button>';
                output += '</div>';
                output += '<div class="modal-body">';
                output += '<img class="moogle-modal-img" src=' + monster.picture + ' alt="Picture of ' + monster.name  + '." title="Picture of ' + monster.name  + '."></div>';
                output += '</div></div></div>';
                output += '<br/></div></div>';
                if(count%2 == 0) { 
                    output += '</div>'
                }
                count++;
            }
        });

        // Format for game display
        $.each(games, function(key, game) {
            if ((game.title.search(regex) != -1)) {
                output += '<div class="row moogle-row"><div class="col-md-8">';
                output += '<h4 class="moogle-h4"><strong>'+ game.title + '</strong></h4>';
                output += '<h7 class="search-result">To learn more visit <a href="https://www.google.com/search?q=final+fantasy+' + game.title.toLowerCase() + '" target="_blank">https://www.google.com/search?q=' + game.title.toLowerCase() + '</a></h7><br/>';
                output += '<p class="dark-bluish-gray">' + game.description + '</p>';
                output += '</div><br/><div class="col-md-4">';
                output += '<img class="moogle-img" src=' + game.picture + ' alt="Logo for ' + game.title + '." title="Picture of ' + game.title + '.">';
                output += '<br/></div><br/></div>';
                if(count%2 == 0) { 
                    output += '</div>'
                }
                count++;
            }
        });
    $('#result').html(output);
});

// Loading bar while fetching data
function timer() {
    let timed;
    timed = setTimeout(showTotals, 3000);
}

// Displaying totals
function showTotals() {
    $('#loader').fadeOut();
    $('#totals').delay(700).fadeIn();
}