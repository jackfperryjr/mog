// Loading characters once page is ready
$(document).ready(function () {
    getData();
});

// Using Api to get characters
const uri = '/finalfantasy/api';

// Making list of characters
function getData() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            $('#characters').empty();
            getCount(data.length);
            $.each(data, function (key, character) {
                $('<tr><td>' + character.name + '</td>' +
                    '<td>' + character.origin + '</td>' +
                    '</tr>').appendTo($('#characters'));
            });

            characters = data;
        }
    });
}

// Counting the characters
let characters = null;
function getCount(data) {
    const el = $('#counter');
    let name = 'character.';
    if (data) {
        if (data > 1) {
            name = 'characters.';
        }
        el.text(data + ' ' + name);
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
        $.each(characters, function(key, val) {
            if ((val.name.search(regex) != -1)) {
                output += '<div class="well col-md-12">';
                output += '<code><br/>';
                output += '"Name":' + JSON.stringify(val.name) + ',' + '<br/>';
                output += '"Age":' + JSON.stringify(val.age) + ',' + '<br/>';
                output += '"Race":' + JSON.stringify(val.race) + ',' + '<br/>';
                output += '"Job":' + JSON.stringify(val.job) + ',' + '<br/>';
                output += '"Height":' + JSON.stringify(val.height) + ',' + '<br/>';
                output += '"Weight:' + JSON.stringify(val.weight) + ',' + '<br/>';
                output += '"Origin":' + JSON.stringify(val.origin) + ',' + '<br/>';
                output += '"Description":' + JSON.stringify(val.description) + '<br/>';
                output += '</code><br/>';
                output += '</div>';
                if(count%2 == 0) { 
                    output += '</div>'
                }
                count++;
            }
        });
    $('#result').html(output);
});