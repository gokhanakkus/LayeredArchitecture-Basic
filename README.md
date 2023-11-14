# LayeredArchitecture-Basic

This project represents the next stage of the assignment from Task 13. In this stage, we enhance the MVC project by moving the existing structure to a layered architecture, making the project more organized and manageable.

# Usage
For example CRUD operations, you can follow the steps below:

List existing items on the homepage.
Click "Add to Cart" to add new items.
Go to the "My Carts" page to edit or delete items.

# SUMMARY

Users can create multiple carts.
Users can delete the carts they added or the items in the cart.
Desired quantity of items can be added to the desired cart.
Users cannot view carts of different users.

# DETAILS

This project includes fundamental techniques such as layered architecture, dependency injection, authorization, and authentication.
It represents an ASP.NET Core-based application shaped by modern software development approaches. Key focus areas of the application include effectively managing secure user authentication and authorization mechanisms, as well as providing dynamic and customizable shopping cart management.

Data access operations are conducted on an abstract structure using the ORM capabilities provided by Entity Framework Core, adopting an approach that ensures effective and organized integration with the database.

Designed according to the MVC design pattern, the components stand out in handling authentication processes such as user login, registration, and logout, as well as effectively managing the user's cart content.

The IRepository and DataRepository structure abstracts database operations by separating interfaces and implementations, reducing the coupling between business logic and data access.
Similarly, controllers like CartController and AuthController represent the presentation layer, making the business logic independent of the user interface.

Services added to the "Services" collection are accessible throughout the entire application and automatically inject necessary dependencies. Dependencies like "AppDbContext" are passed to controllers through constructor injection. Additionally, "Scoped" scope is used to configure services according to their lifetimes.

While configuring cookie-based authentication with methods like AddAuthentication and AddCookie, access permissions to specific areas are controlled in controllers using [Authorize] and [AllowAnonymous] attributes.
