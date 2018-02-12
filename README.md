# Users
Prototype website SPA that allows a user to be added to a database. Implemented in MVC Razor Pages Dotnet Core. Emphasis is more on the functionality, rather than the style.

User should know whether the email address is valid and the password is valid based on a regular expression and min length. Password and password confirmation should match as well. Validation occurs both client side and sever side - client side validation could be 'switched off' in the user's browser if jquery is disabled, in which case server side validation is used.

The user is then stored in a database data table called User, i.e. the user's email address and password are stored in the table. Email address is the primary key on the table. The password is stored hashed with salt to reduce the chances of the passwords decrypted. 

A nice advantage of using ASP.NET Core is that it's protected against Cross-Site Request Forgery attacks by default (https://docs.microsoft.com/en-us/aspnet/core/security/anti-request-forgery).
