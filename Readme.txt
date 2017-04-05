API - 
In order to properly use the API, you may need to change the connection string in your appsettings.json to match
the location of your project, it MUST end in your //wwwroot//db//dotnetbc.sqlite
Once you have adjusted your connection string to point to the proper location of your sqlite file, you must
run 'dotnet ef database update' in the root of your project. Your API should work now.
  - Authorization
    OpenIdDict has been implemented, the controllers may or may not be authorized. To authorize send a 
	x-www-form-urlencoded request to 'PROJECTURL + /connect/token' and use data

	username: m@m.m
	password: P@$$w0rd
	grant_type: password

	This should give you the necessary token to use to get the data.