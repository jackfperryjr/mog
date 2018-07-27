# ![Moogle logo](wwwroot/images/ff-moogle.png) Moogle

Moogle is a search engine and database for all things Final Fantasy.

### **Instructions** for mentor/tester

I encourage using the live version [here](https://moogleapi.azurewebsites.net).
The home page of the application is a single page application utilizing the API.
There's an empty space at the very top directly above the social media icons that functions as the login to the MVC. When prompted the username is `test@moogle.com` and the password is `FinalFantasy1!`.<br>
Once logged in you'll be taken to the character index page - on this page you can only browse and view the character data.<br>
Where the empty space was before now lies your name and icon to let you know you're logged in. Your icon is a clickable menu and if you navigate to monsters or games you'll be able to create, read, update, and delete entries.<br>

## Application details

This app was built in development with VSCode, .NET Core 2.0, and SQLite. 
This app is built in production with VSCode, .NET Core 2.0, Azure, and SqlServer.

If you need to **install** .NET Core on your machine:<br>
* `dotnet add package Microsoft.AspNetCore --version 2.0.4`
* `dotnet add package Microsoft.NETCore.App --version 2.0.6`

I have SendGrid setup but I'm not currently using it in the application (coming soon!), but if you need to **install** it:<br>
* `dotnet add package Sendgrid --version 9.9.0`

More documentation to come!


