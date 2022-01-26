# net6-product-api
test web api on net6 with TDD approach, with mock api data of external api and lazy cache from external calls

I the following url you can find the mock data to retun to the front end
https://mockapi.io/projects/61ef30a5d593d20017dbb36a

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
net 6x
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

mssql
https://www.microsoft.com/en-us/sql-server/sql-server-downloads
```

### Installing

1)clone the repo 

2)change the connection string to your local database on appsetting.json file 

3)go to the root project folder and run the following command

```
dotnet run
```

this will dun de project create database and populate default test users

to access swagger documentation
```
https://localhost:7282/swagger/index.html
```

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
