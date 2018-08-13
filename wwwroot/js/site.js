// List for setting a random search example name as the input placeholder
let randomPlaceholderList = [
    "Search ex. ''Lightning'' or ''13''",
    "Search ex. ''Ashe'' or ''12''",
    "Search ex. ''Yuna'' or ''10''",
    "Search ex. ''Firion'' or ''2''",
    "Search ex. ''Aerith'' or ''7''",
    "Search ex. ''Cecil'' or ''4''",
    "Search ex. ''Bartz'' or ''5''",
    "Search ex. ''Tidus'' or ''10''",
    "Search ex. ''Refia'' or ''3''",
    "Search ex. ''Kefka'' or ''6''"
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
        setModal(character) {
            this.modal = character
        }
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
                    || character.name.indexOf(self.search) > -1
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
        modal: {},
    }
  })