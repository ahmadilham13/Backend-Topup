# Backend Application

This is the backend application for a topup platform. It provides the server-side logic for handling user requests and database operations. The application is built using ASP.NET Core, a cross-platform framework for building web applications, and Entity Framework Core, an Object-Relational Mapping (ORM) framework for database access.

## Prerequisites

To run this application, you need to have the following prerequisites installed:

- .NET 8 SDK or later
- Visual Studio 2019 or later
- MySQL or MariaDB
- `dotnet-ef` tools
- Redis
- Git

## Cloning the Repository

To get started, clone the repository from Gitlab:

```bash
git remote add origin https://github.com/ahmadilham13/Backend-Topup.git
```

This will create a local copy of the repository on your computer.

## Installing the Dependencies

To install the required dependencies, open a command prompt and navigate to the root directory of the cloned repository. Then, run the following command:

```bash
dotnet restore
```

This command will download and install the required NuGet packages for the project.

## Setting up the Database

To set up the database, you need to modify the `appsettings.json` file in the root directory of the cloned repository. First, make a copy of the `_appsettings.json_example` file and save it as `appsettings.json`. Then, modify the `ConnectionStrings` section with the actual values for your MySQL or MariaDB server.

For example, if your database credentials are `myuser`, `mypassword`, and `mydatabase`, the `ConnectionStrings` section in the `appsettings.json` file should look like this:

```json
{
	"ConnectionStrings": {
		"TopupDatabase": "server=localhost;user=myuser;password=mypassword;database=mydatabase",
		"Redis": "redis_host:redis_port",
		"TestDatabase": "server=localhost;user=myuser;password=mypassword;database=mydatabase"
	}
}
```

## Setting up Redis

To set up Redis, you need to modify the `appsettings.json` file in the root directory of the cloned repository. First, make a copy of the `_appsettings.json_example` file and save it as `appsettings.json`. Then, modify the `Redis` section with the actual values for your Redis server.

For example, if your Redis credentials are `myaccesskey`, `mypass`, and `myport`, the `Redis` section in the `appsettings.json` file should look like this:

```json
{
    "ConnectionStrings": {
        "(...)",
        "Redis": "localhost:myport,abortConnect=False,ssl=False,password=mypass,allowAdmin=True",
        "(...)"
    }
}
```

Replace `myport` with your Redis port number.


## Building the Application

To build the application, open a command prompt and navigate to the root directory of the cloned repository. Then, run the following command:
```bash
dotnet build
```
This command will compile the source code and output the compiled assemblies to the `bin` directory.

## Running the Application

To run the application, open a command prompt and navigate to the root directory of the cloned repository. Then, run the following command:
```bash
dotnet run
```
This command will start the web server and launch the application. You can access the application in your web browser at <http://localhost:5042>.

## Testing the Application

To test the application, you can use the `dotnet test` command to run the unit tests. Open a command prompt and navigate to the root directory of the cloned repository. Then, run the following command:
```bash
dotnet test
```
This command will run all the unit tests in the project and display the results in the command prompt.