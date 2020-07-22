// List for setting a random search example name as the input placeholder
let randomPlaceholderList = [
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
let randomPlaceholder = randomPlaceholderList[Math.floor(Math.random()*randomPlaceholderList.length)];

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
            let filtered = this.character;
            let empty = ""

            if (!this.search) {
                return empty;          
            }

            if (this.search) {
                let self = this;
                filtered = this.character
                .filter(function(character) {
                    return character.name.toLowerCase().indexOf(self.search) > -1
                    || character.name.indexOf(self.search) > -1
                    || character.origin.toLowerCase().indexOf(self.search) > -1; 
                });  
                
                //filtered = this.character
                //.filter(c => c.name.toLowerCase().indexOf(self.search) > -1 || c.origin.toLowerCase().indexOf(self.search) > -1);
                // The above is an alternate arrow function.
                // However, it doesn't work in some mobile browsers.
                // Maybe it works now, I'm too lazy to find out.
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