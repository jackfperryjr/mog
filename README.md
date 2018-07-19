# Moogle

Moogle is a search engine and database for all things Final Fantasy.

This app was built with VSCode, .NET Core 2.0, and SQLite. 

If you need to install .NET Core on your machine:<br>
`dotnet add package Microsoft.AspNetCore --version 2.0.4`<br>
`dotnet add package Microsoft.NETCore.App --version 2.0.6`<br>

I have SendGrid setup but I'm not currently using it in the app, but if you need to install it:<br>
`dotnet add package Sendgrid --version 9.9.0`

I've provided a login and password in the `appsettings.json` file. This user will grant you access to modifying Monsters and Games, but not Characters; nor will you be allowed to register a new user. The login is just an empty space at the very top above the social icons.

The home page is an single page app connecting to the database via API controllers. So, even without logging in you'll still be able to use the search bar to search for characters/monsters/games.

More documentation to come!


