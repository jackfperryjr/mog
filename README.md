# M<img src="wwwroot/icons/icon-ff-moogle.png" width="20">ogleApi

MoogleApi is a database for all things Final Fantasy. Currently live at www.moogleapi.com! The homepage is a single page application/search engine using VueJs! Check it out.

### Easy to use!

Use your favorite front-end framework to send a request:
```
https://www.moogleapi.com/api/v1/characters
https://www.moogleapi.com/api/v1/monsters
https://www.moogleapi.com/api/v1/games
https://www.moogleapi.com/api/v1/characters/random
```

### New! ` /search ` route has been added to the Monster and Character controllers! ###
```
https://www.moogleapi.com/api/v1/characters/search?name=lightning
https://www.moogleapi.com/api/v1/characters/search?gender=female
https://www.moogleapi.com/api/v1/characters/search?job=l'cie
https://www.moogleapi.com/api/v1/characters/search?race=human
https://www.moogleapi.com/api/v1/characters/search?origin=13
```
```
https://www.moogleapi.com/api/v1/monsters/search?name=chocobo
```
##### *For now the search routes only handle the ` name ` parameter on the Monster controller. More to come!

Then use that data in your website, webpage, or application!

### Examples

#### JavaScript (super simple example using VueJs and Axios)

```javascript
new Vue({
    el: "#vue-app",
    mounted: function() {
        this.getCharacters()
    },
    methods: {
        getCharacters() {
            axios.get("https://www.moogleapi.com/api/v1/characters")
            .then(response => {this.characters = response.data})
        },
        getRandom() {
            axios.get("https://www.moogleapi.com/api/v1/characters/random")
            .then(response => {this.random = response.data})
        }
    data: {
        random: {},
        characters: [],
    }
})
```

#### HTML (super simple example)

```html
<div id="vue-app">
    <div>
        <button v-on:click="getRandom()">Click to get random character</button>
        <div>
            <h1 style="color:#cc0000">Name: {{random.name}}</h1>
            <h3 style="color:#cc0000">Origin: {{random.origin}}</h3>
            <h3 style="color:#cc0000">Job/Class: {{random.job}}</h3>
        </div>
        <!-- The above is styled in red to signify it's random -->
    </div>
    <div v-for="character in characters">
        <h1>{{character.name}}</h1>
        <h3>{{character.origin}}</h3>
        <h3>{{character.gender}}</h3>
        <!-- ***NOTE*** The above would just produce a list of *ALL* the characters -->
    </div>
</div>
```

##### CDNs I used for VueJs and Axios
```html
<script src="https://cdn.jsdelivr.net/npm/vue@2.5.16/dist/vue.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/axios/0.18.0/axios.min.js"></script>
```
    
### Character properties

* Name (First and last, if they have one)
* Origin (The game they originated in)
* Race
* Gender
* Age
* Job
* Height
* Weight
* Picture
* Description (Sometimes short, sometimes lengthy)

If I wasn't able to find the data for the character a set of question marks (??) is substituted.<br>
This is a very simple data set only consisting of the games in the main series (1-15). Although, I'll look into supporting spin-offs as time permits. The data typically refers to the origin of the character, however, more info related to the character's other appearances is usually mentioned in the description.

### Monster properties

* Name
* Strength
* Weakness
* Picture
* Description

Currently there are very few monsters and very simplistic data. If you're interested in helping, let's talk!

### Game properties

* Title
* Release Date
* Picture
* Description

Titles are simple, just 01-15 as of now. Release date refers to the Japanese release, I believe. 

More documentation to come!


