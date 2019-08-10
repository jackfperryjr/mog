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
    created: function() {
        this.getLoad()
    },
    methods: {
        getCharacters() {
            axios.get("api/v1/characters")
            .then(response => {this.character = response.data})
        },
        setModal(character) {
            this.modal = character
        },
        scrollTop() {
            $("html, body").animate({scrollTop:"0"}, 500)
        },
        getRandom() {
            $(".search-results").hide()
            $("#random-character").show()
            axios.get("https://www.moogleapi.com/api/v1/characters/random")
            .then(response => {this.random = response.data})
        },
        getLoad() {
            $(".search-results").hide()
            $("#random-character").show()

            // axios.get("https://www.moogleapi.com/api/v1/characters/75c054fe-d022-44d4-102f-08d6afcab3e2") // Using id (returns single entity)
            axios.get("https://www.moogleapi.com/api/v1/characters/search?name=lightning") // Using search route (returns array so rendering would take a different approach)
            .then(response => {this.random = response.data})
        }
    },
    computed: {
        filtered: function() {
            let filtered = this.character;

            if (!this.search) {
                return this.getLoad();          
            }

            if (this.search) {
                let self = this;
                filtered = this.character
                .filter(function(character) {
                    return character.name.toLowerCase().indexOf(self.search) > -1
                    || character.name.indexOf(self.search) > -1
                    || character.origin.indexOf(self.search) > -1; 
                });    
                
                //filtered = this.character
                //.filter(c => c.name.toLowerCase().indexOf(self.search) > -1 || c.origin.toLowerCase().indexOf(self.search) > -1);
                // The above is an alternate arrow function.
                // However, it doesn't work in some mobile browsers.
                // Maybe it works now, I'm too lazy to find out.
            }
            $("#random-character").hide()
            $(".search-results").show()
            return filtered;
        },
    },
    data: {
        random: {},
        character: [],
        search: "",
        modal: {},
    }
  })