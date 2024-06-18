# Digital Table Management

## The proposal

Introducing Digital Table Management, an innovative solution to optimize table management in restaurants, bars, cafes, and other establishments.

Through an intuitive and easy-to-use web platform, our system offers entrepreneurs a complete and real-time view of their table occupancy, facilitating space organization, reducing customer waiting times, and increasing service efficiency.

## Project Layers
### Controllers
- Purpose: Acts as the central point for handling incoming user requests (e.g., from a web browser or API call).
- Responsibilities:
	- Receives user requests (typically through HTTP verbs like GET, POST, PUT, DELETE).
	- Extracts data from the request (e.g., form data, URL parameters).
	- Interacts with other layers like Services or DataAccess to retrieve or manipulate data according to the request.
	- Selects the appropriate View model (containing data formatted for presentation) based on the request.
	- Returns the response, which can be:

		-	A complete HTML page (using Razor Views) for user interaction.
		- JSON data for consumption by other applications (APIs).
		- Redirects to a different URL within your application.
 
### Services
- Contains business logic that is independent of the UI or data access layer.
- Handles complex operations that might involve multiple data sources or calculations.
-	Promotes loose coupling and testability.

### Models
- Represents the data model of your application.
-	Often includes classes for:
  
	- Entities: Represent data objects stored in the database (e.g., User, Product).
	- Data Transfer Objects (DTOs): Used to transfer data between different layers without exposing the full entity structure.
	- View Models: Contain data formatted specifically for presentation in Razor Views.

### DataAccess
- Handles interactions with the data source (e.g., database).
- Might use technologies like Entity Framework or Dapper to simplify data access.
- Encapsulates logic for connecting, CRUD (Create, Read, Update, Delete) operations, and queries.

### Web
- Often refers to the Razor Views in ASP.NET MVC.
- These files define the user interface (UI) by combining HTML, CSS, and Razor syntax.
- Controllers interact with Views to render the appropriate UI based on the request and data.

## Technologies + Librarys + Desing Patterns
- .Net 8
- ASP.NET MVC 
- Entity Frawework (ORM) + Migrations
- Mysql for database
- Dependency Injection (default recommend by Microsoft)