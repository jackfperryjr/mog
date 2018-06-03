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
                output += '<div class="row" style="margin-right: 0;margin-left:0;"><div class="col-md-8">';
                output += '<h4 style="color: #2962ff;margin-bottom: -3px;">'+ val.name + '</h4>';
                output += '<h7 class="search-result">To learn more visit <a href="https://www.google.com/search?q=final+fantasy+' + val.name.toLowerCase() + '" target="_blank">https://www.google.com/search?q=' + val.name.toLowerCase() + '</a></h7><br/>';
                output += '<p style="color: #37474f">' + val.description + '</p>';
                output += '<table style="color: #37474f; width:50%;"><tr>';
                output += '<td><strong>Age:</strong> ' + val.age + '</td>';
                output += '<td><strong>Race:</strong> ' + val.race + '</td></tr><tr>';
                output += '<td><strong>Job:</strong> ' + val.job + '</td>';
                output += '<td><strong>Height:</strong> ' + val.height + 'm</td></tr><tr>';
                output += '<td><strong>Weight:</strong> ' + val.weight + 'kg</td>';
                output += '<td><strong>Origin:</strong> ' + val.origin + '</td></tr>';
                output += '</table><br/>';
                output += '</div><div class="col-md-4">';
                output += '<img style="width: 200px; padding: 7px; border: 1px solid #e0e0e0; margin: 20px 0; float: right;" src=' + val.picture + ' alt="Picture of ' + val.name  + '." title="Picture of ' + val.name  + '.">';
                output += '<br/></div>';
                if(count%2 == 0) { 
                    output += '</div>'
                }
                count++;
            }
        });
    $('#result').html(output);
});