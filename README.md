# Moogle

Moogle is a search engine and database for all things Final Fantasy.

More documention to come!

This app was built with VSCode, .NET Core 2.0, and SQLite. 

I have SendGrid setup but I'm not currently using it in the app, but if you need to install it:
`dotnet add package Sendgrid --version 9.9.0`

I've provided a login and password in the `appsettings.json` file. This user will grant you access to modifying Monsters and Games, but not Characters; nor will you be allowed to register a new user. The login is just an empty space at the very top above the social icons.

The home page is an single page app connecting to the database via API controllers. So, even without logging in you'll still be able to use the search bar to search for characters.



