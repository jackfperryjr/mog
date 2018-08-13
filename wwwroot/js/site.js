// List for setting a random search example name as the input placeholder
let randomPlaceholderList = [
    "Search ex. ''Lightning'''",
    "Search ex. ''Cloud''",
    "Search ex. ''Yuna''",
    "Search ex. ''Firion''",
    "Search ex. ''Aerith''",
    "Search ex. ''Cecil''",
    "Search ex. ''Rosa''",
    "Search ex. ''Tidus''",
    "Search ex. ''Paine''",
    "Search ex. ''Kefka''"
];

// Randomly choosing from the list
let randomPlaceholder = randomPlaceholderList[Math.floor(Math.random()*randomPlaceholderList.length)];

$("#search").attr("placeholder", randomPlaceholder);

// Implementing VueJs

new Vue({
    el: "#vue-app",
    mounted() {
        this.getCharacters()
    },
    methods: {
        getCharacters() {
            axios.get("https://www.moogleapi.com/api/characters")
            .then(response => {this.character = response.data})
        },
    },
    computed: {
        filtered: function() {
            let filtered = this.character;
            let empty = "";

            if (!this.search) {
                return empty;
            }

            if (this.search) {
                let self = this;
                filtered = this.character
                .filter(function(character) {
                    return character.name.toLowerCase().indexOf(self.search) > -1
                    || character.origin.toLowerCase().indexOf(self.search) > -1; 
                });    
                
                //.filter(c => c.name.toLowerCase().indexOf(this.search) > -1 || c.origin.toLowerCase().indexOf(this.search) > -1);
                // The above is an alternate arrow function.
                // However, it doesn't work in some mobile browsers.
            }
            return filtered;
        },
    },
    data: {
        character: "",
        search: "",
    }
  })