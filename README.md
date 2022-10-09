# Server-Side Log In Implementation for ASP.NET and MongoDB

### By: Jonathan Mantello

This is an implementation of a backend system in .NET that allows for a user to create credentails with a email/username and password, log in to the system which generates a session token, and log out of the system which deletes the token. All of the data is stored and interfaced with a cloud instance of MongoDB.

#### Purpose
The purpose of this project is to act as a reference or an example of this kind of system where distinct users are created and managed with a database on the backend. 

#### Storing Credentials
Passwords are scrambled with the SHA256 hashing algorithm along with a randomly generated GUID salt and are stored in the database under the Credentails table, along with the user's email/username.

#### Logging In
When a user logs in, the credentials are checked against the ones stored in the database, and if there's a match, then a GUID session token is created, stored in the database in the Sessions table along with the users email/username, and then is returned by the API. This allows the user to remain logged in until they decide to log out.

#### Logging Out
When a user logs out, their session token is given to the API and then their session is deleted from the database.

