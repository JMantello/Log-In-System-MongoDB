# Server-Side Log In Implementation for ASP.NET and MongoDB

### By: Jonathan Mantello

This is an implementation of a backend system in .NET that allows for a user to create credentials with a email/username and password, log in to the system which generates a session token, and log out of the system which deletes the token. All of the data is stored and interfaced with a cloud instance of MongoDB.

#### Purpose
The purpose of this project is to act as a reference or an example of this kind of system where distinct users are created and managed with a database on the backend. 

#### Storing Credentials
Passwords are scrambled with the SHA256 hashing algorithm along with a randomly generated GUID salt and are stored in the database under the Credentails table, along with the user's email/username.

#### Logging In
When a user logs in, the credentials are checked against the ones stored in the database, and if there's a match, then a GUID session token is created, stored in the database in the Sessions table along with the users email/username, and then is returned by the API. This allows the user to remain logged in until they decide to log out.

#### Logging Out
When a user logs out, their session token is given to the API and then their session is deleted from the database.

#### The Database's Design 
<table>
<tr>
<td> Table </td> <td> Document </td>
</tr>
<tr>
<td> Credentials </td>
<td>

```json
{
  "_id": {
    "$oid": "6340d144f15757f4ac04f97d"
  },
  "Email": "userexample@gmail.com",
  "Password": "A+qcsss69wa4VabyUm4YTc2RafJJehnCC1sE+xZaM2g=",
  "Salt": "3b7508f6-53e8-4bfa-bf4b-36551ecbd285"
}
```

</td>
</tr>
<tr>
<td> Sessions </td>
<td>

```json
{
  "_id": {
    "$oid": "63421ebf2f6ae53c435d4cba"
  },
  "Email": "userexample@gmail.com",
  "Token": "19570544-9d29-4362-b416-dc7d1233efec"
}
```

</td>
</tr>
</table>
