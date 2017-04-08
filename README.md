:bowtie:

# Project Sloth: .NET BC Backend


### Importing the Project :kissing_heart: 
To import the project in Visual Studio 2017, follow the next steps. Sounds stupid, but it works!

1. After pulling from GitHub, go to the folder in the command line.
1. Run dotnet restore on the project from the command line.
1. Open the .sln solution file with Visual Studio 2017.
1. OpenIdDict won't be recognized and that's okay. Just go to the Solution Explorer on the top-right corner and build the solution by right-clicking on the project's name and clicking on the Build option. (DO NOT BUILD FROM THE COMMAND LINE.)
1. Test it. If it doesn't work, go to appsettings.json and change the connection string to the full URI of the SQLite database. Something like:
"C:\\Users\\user\\git\\dot-net-bc-sloth\\DotNetBcBackend\\wwwroot\\db\\dotnetbc.sqlite;"

### Authentication and Authorization :no_entry: 

Some Web APIs need authentication. To authenticate via the Web API send a x-www-form-urlencoded POST request
to 'PROJECTURL + /connect/token' and use the following data:

Key | Value
------------ | -------------
username | mem
password | P@$$w0rd
grant_type | password

This should give you the necessary token to use to get the data. To get the data, send a GET
request with the key "Authorization" and value "Bearer C9098BAD987BC..." or whatever the token is.