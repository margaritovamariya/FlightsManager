# FlightManager
School Work

The Flight Manager is a system used to book airline tickets as well as to manage flights. It allows you to track past flights, along with information about people who have traveled.
Flight Manager consists of two main components - a database and a web application with a layer that communicates with the database.

In order to start the system on our pc, first you need to change the Connection string which appears in two places. The first one is on FlightManager.Common/DbConfiguration. and the second one is on FlightManager.Web/appsettings.json. You need ot change the server name which is already there with the server name of your pc. Then you need to use the command: update-database.

Now you are ready to start the system.

## License 
[MIT](https://choosealicense.com/licenses/mit/)
