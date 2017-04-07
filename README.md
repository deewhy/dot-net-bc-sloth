# :bowtie: Project Sloth: .NET BC Backend :bowtie: 

### API :kissing_heart: 
In order to properly use the API with a localhost MVC project, you need to change the connection 
string in your appsettings.json to match the location of your project, it MUST end in your //wwwroot//db//dotnetbc.sqlite
For example: "C:\\Users\\user\\git\\dot-net-bc-sloth\\DotNetBcBackend\\wwwroot\\db\\dotnetbc.sqlite;"

### Authorization :no_entry: 
OpenIdDict has been implemented, the controllers may or may not be authorized. To authorize send a 
x-www-form-urlencoded POST request to 'PROJECTURL + /connect/token' and use data

Key | Value
------------ | -------------
username | mem
password | P@$$w0rd
grant_type | password

This should give you the necessary token to use to get the data. To get the data, send a GET
request with the key "Authorization" and value "Bearer C9098BAD987BC..." or whatever the token is.