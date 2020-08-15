$(document).ready(function() {
    if (halfmoon.readCookie("darkModeOn") == "yes") {
        halfmoon.toggleDarkMode();
    } 
});

$(this).delay(1000).queue(function() {
    var search = $('#search').offset().top;

    var stickySearch = function(){
        var scrollTop = $(window).scrollTop();
    
        if (scrollTop > search) { 
            $("#search").addClass("sticky-search-bar");
            $("#search-icon").addClass("sticky-search-icon");
            $("#search-logo").show();
        } else {
            $('#search').removeClass("sticky-search-bar"); 
            $("#search-icon").removeClass("sticky-search-icon");
            $("#search-logo").hide();
        }
    };

    stickySearch();
    $(window).scroll(function() {
        stickySearch();
    });
});

// List for setting a random search example name as the input placeholder
var randomPlaceholderList = [
    "Search ex. ''Lightning'' or ''13''",
    "Search ex. ''Ashe'' or ''12''",
    "Search ex. ''Yuna'' or ''10''",
    "Search ex. ''Firion'' or ''02''",
    "Search ex. ''Aerith'' or ''07''",
    "Search ex. ''Cecil'' or ''04''",
    "Search ex. ''Bartz'' or ''05''",
    "Search ex. ''Tidus'' or ''10''",
    "Search ex. ''Refia'' or ''03''",
    "Search ex. ''Kefka'' or ''06''"
];

// Randomly choosing from the list
var randomPlaceholder = randomPlaceholderList[Math.floor(Math.random()*randomPlaceholderList.length)];

$("#search").attr("placeholder", randomPlaceholder);

// Implementing VueJs
new Vue({
    el: "#vue-app",
    mounted: function() {
        this.getCharacters()
    },
    methods: {
        getCharacters() {
            axios.get("https://www.moogleapi.com/api/v1/characters")
            .then(response => {this.character = response.data})
        },
        setModal(character) {
            this.modal = character
        },
        scrollTop() {
            $("html, body").animate({scrollTop:"0"}, 500)
        }
    },
    computed: {
        filtered: function() {
            var filtered = this.character;
            var empty = ""

            if (!this.search) {
                return empty;          
            }

            if (this.search) {
                var self = this;
                filtered = this.character
                .filter(function(character) {
                    return character.name.toLowerCase().indexOf(self.search) > -1
                    || character.name.indexOf(self.search) > -1
                    || character.origin.toLowerCase().indexOf(self.search) > -1; 
                });  
            }
            $(".search-results").show()
            return filtered;
        },
    },
    data: {
        character: [],
        search: "",
        modal: {}
    }
  })