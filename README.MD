# Income Tax Calculator

## Overview

This Income Tax Calculator web application is designed to automate the process of calculating income taxes. It features an intuitive Angular TypeScript frontend for a smooth and responsive user experience, coupled with a powerful C# ASP.NET backend for secure and reliable data processing. The application leverages a PostgreSQL database to store and manage all tax-related data efficiently.

The frontend enables users to input relevant financial data and view calculated tax results dynamically. The backend handles the business logic, including tax computation algorithms and data validation, ensuring accurate and up-to-date tax assessments. The PostgreSQL database provides a solid foundation for storing tax rules, ensuring data integrity and scalability.

To ensure seamless integration between the frontend and backend, NSwag is employed to generate and manage the API client code automatically. This guarantees type-safe communication and minimizes manual work for maintaining API contracts.

This modular architecture allows the application to be easily maintained, updated, and expanded to meet evolving tax regulations and user needs.

---

## Technologies used

### Database
* **PostgreSQL 17.4.2** – Database for persisting all tax-related data.
* **C# objects** – Used to map and interact with database entities.
* **Microsoft.EntityFrameworkCore.Design 9.0.5** and **Microsoft.EntityFrameworkCore** – ORM for database management.

### Backend
* **C# ASP.NET** – Main backend framework to handle API requests, business logic, and data processing.
* **Serilog** – For structured logging to track down errors and improve observability.
* **Command, Command Handler, Controller, Services** – Clean architecture pattern for organizing and processing API requests.
* **FluentValidation 12.0.0** – For model and input validation.
* **Scrutor 6.0.1** – Used for registering services via assembly scanning and fluent API.
* **NSwag** – Automatically generates TypeScript client code for the frontend, ensuring type-safe API communication.
* **Custom Wrappers** – Created for testing and to improve code readability.

### Frontend
* **Angular (TypeScript)** – Modern web framework for building dynamic and reactive UIs.
    * **Angular CLI 18.2.11** – Project scaffolding and management.
    * **Angular 18.2.0** – Core framework, along with modules for animations, forms, and routing.
    * **Reactive Forms Module** – Powerful form handling with reactive APIs.
    * **Zone.js 0.14.10** – For change detection and async tracking.
    * **Prettier** – Code formatting for improved readability and consistency.
* **Typescript & Tooling**
    * **Tslib 2.3.0** – Runtime library for TypeScript helpers.

### Unit Testing
* **Moq 4.20.72** and Moq.AutoMock 3.5.0 – Mocking frameworks to isolate and test C# components.
* **NUnit 4.3.2** – Unit testing framework for C#.
* **FluentAssertions 8.2.0** – Improves readability and expressiveness of unit tests in C#.
* **Jasmine 4.6.1** – Unit testing framework for Angular.
    * **Jasmine-auto-spies 8.0.1** – For streamlined creation of spies in tests.
    * **Jasmine Spies** – Used for unit testing Angular services in isolation by spying on method calls, their arguments, and return values. These tests focus on verifying that the service calls methods and transforms responses correctly, not on full workflows or integration tests (which would require more extensive HTTP testing controllers).
* **Karma 6.4.0** – Test runner for Angular.
* **Karma Chrome Launcher, Coverage, and Jasmine HTML Reporter** – Tools to run tests in browsers and get coverage reports.

### Other Tools and Utilities
* **Powershell 3rd-party plugin** – Additional tooling and automation scripts.
* **Mono.TextTemplating (3.0.0)** – for code generation tasks.
* **Swashbuckle.AspNetCore (6.4.0)** – provides Swagger UI for API exploration and documentation.
* **System.Text.Json (9.0.5)** – for efficient JSON serialization/deserialization.

---

## Technology Choices and Architectural Overview

### **Database: PostgreSQL**

The project uses PostgreSQL as its database due to its open-source nature, lack of licensing fees, and strong community support. It offers advanced SQL compliance with powerful features such as complex queries, window functions, full-text search, and native JSON support—ideal for an enteprise scaqle application.

PostgreSQL supports large databases, offers a wide variety of client libraries and management tools, and integrates well with frameworks via ORMs like Entity Framework Core, which is used here with the C# ASP.NET backend.

Security-wise, PostgreSQL provides robust authentication, SSL encryption, and auditing capabilities. While these were not implemented in this project (as per the specifications), they represent potential areas for future enhancement.

The ability to create custom functions and extensions in PostgreSQL allows encoding complex business logic close to the data, which is particularly useful for an enterprise scale application that may require custom calculations or stored procedures.

If user authentication is added to support multiple concurrent users, PostgreSQL’s MVCC (Multi-Version Concurrency Control) and strong transaction handling ensure consistent, concurrent access without performance bottlenecks.

### Why PostgreSQL over other databases?
* MySql:
    * While MySQL is popular and fast, its SQL feature set and JSON handling are less advanced than PostgreSQL’s, limiting flexibility for complex queries.
* Microsoft SQL server:
    * A robust option in Microsoft environments but comes with licensing costs and may be excessive for small to mid-sized projects. PostgreSQL delivers comparable enterprise features without fees.
* MongoDB:
    * Excellent for flexible, schema-less data, but less suited for this app’s structured, relational data needs. Complex joins and transactions are more difficult and less efficient in MongoDB.

I chose to store the tax band data in the database to demonstrate practical data usage. The data is seeded using a data seeder, with initial values loaded from a config.json file. This approach allows the tax band values to be updated easily without requiring code changes or redeployment. The `IncomeTaxCalculatorContext.cs` class centralizes the creation of the database context for all models, rather than instantiating separate contexts for each model. This design promotes scalability and maintainability, making it well-suited for enterprise-level applications.

While I considered storing user input, such as income salary, in the database, I decided against it due to security concerns. For an enterprise-scale application, storing sensitive user data requires strict security measures, and as this was outside the project scope, I chose to avoid potential risks by not persisting such information.

To view and manage my PostgreSQL database tables, I used pgAdmin 4, a powerful and user-friendly open-source administration and development platform for PostgreSQL.

![alt text](income-tax-frontend/media/images/pgAdmin4.png)

### Migrations 
I developed a PowerShell script (`manage-migrations.ps1`) to streamline the process of adding, removing, and updating database migrations. This script simplifies managing schema changes, making it easier to maintain consistency across development and deployment environments.

The migration files themselves are included within the repository to provide a clear history of the database schema evolution. Retaining these migration files offers valuable transparency into when models were created, modified, or deleted. This historical record not only aids developers in tracking changes over time but also supports business stakeholders by documenting the data structure’s progression.

Maintaining this migration history is especially beneficial for enterprise-scale applications, where rigorous version control and auditability of database changes are essential for long-term maintainability, compliance, and collaboration across teams.

### How to use the migration script:
* Run `manage-migrations.ps1` which will provide you with a menu of selectable options
    * Create a new migration
    * Remove the last migration
    * Update database
    * Exit
* The script handles interaction with Entity Framework Core tools to create or apply migrations seamlessly.
* This approach simplifies database version control for developers and helps maintain synchronization between the application and its database schema.

![alt text](income-tax-frontend/media/images/manage-migration-script.png)

---

### **Backend: C# ASP.NET Core**

The backend is built with C# ASP.NET Core, meeting spec requirements. The application follows the MVC (Model-View-Controller) architectural pattern, which separates concerns clearly:

* **Model:** Represents data structures and business logic (e.g., `TaxBand.cs`)
* **Controller:** Handles HTTP requests and coordinates interactions (e.g., `TaxController.cs`)
* **View:** The user interface, implemented in Angular TypeScript

This architecture promotes clean, scalable, and maintainable code—ideal for enterprise-grade applications like this tax calculator.

### Custom Components

To improve maintainability and scalability, I designed custom interfaces such as IRequestHandler and IConverter. These abstractions help standardize how requests are processed and how data transformations are handled throughout the project, ensuring consistent coding patterns and reducing duplication. By defining clear contracts, these interfaces make the codebase more modular and easier to extend or refactor.

For example, IRequestHandler abstracts the logic for handling various types of requests, allowing different implementations to be swapped in or enhanced without affecting the overall system. Similarly, IConverter centralizes data transformation logic, facilitating consistent conversions between different object types such as entities, DTOs, and view models.

Additionally, these interfaces are designed to be flexible and extensible. If new architectural patterns or requirements arise—such as adding pipeline behaviors, validation, or logging—they can be easily integrated by expanding or implementing these interfaces differently. This forward-thinking design supports long-term growth and adaptation of the application as business needs evolve.

### Swagger UI

For initial testing and exploration of the API, I used Swagger UI. This interactive tool is incredibly valuable for quickly verifying endpoints, request formats, and responses without needing a client application. It provides immediate feedback on the API’s behavior, allowing me to spot early mistakes, inconsistencies, or unexpected responses during development.

Using Swagger UI early in the process helps ensure that the API is well-designed and functional before moving on to automate client generation with NSwag. By validating the API manually first, I can catch issues such as incorrect parameter bindings, missing data models, or improper response types, which saves time and effort that would otherwise be spent debugging generated client code.

Once the API is confirmed to work correctly via Swagger UI, NSwag is used to generate strongly typed client code automatically based on the OpenAPI specification. This seamless integration reduces manual coding errors and ensures that the frontend API clients stay in sync with backend changes. Together, Swagger UI and NSwag streamline the API development lifecycle—from manual testing and validation to automated client generation—resulting in a more robust and maintainable codebase.

Overall, Swagger UI acts as a convenient sandbox for iterative development, enabling faster troubleshooting and higher confidence that the API specifications are accurate and ready for consumption by generated clients.

![alt text](income-tax-frontend/media/images/swagger-ui.png)

---
### **Frontend AngularTS**

For the frontend, I utilized NSwag to automatically generate strongly-typed API client code based on the backend ASP.NET API. This approach streamlines integration by ensuring that the frontend clients remain synchronized with the backend API definitions, reducing manual coding errors and improving development efficiency. The Angular application itself is organized using component-based architecture, following Angular’s best practices for modularity and maintainability.

On the data flow side, data retrieved from the database is first mapped from entity models to Data Transfer Objects (DTOs) on the backend. These DTOs encapsulate only the required information, enforcing a clear boundary between the database schema and what is exposed to the frontend. This separation improves security, reduces payload size, and allows backend internal changes without impacting the frontend.

Once the DTOs reach the frontend, they are transformed again into client-specific models or view models tailored for UI rendering and interaction. This additional mapping layer enhances flexibility in how data is displayed or manipulated within the Angular components.

The frontend code is structured with dedicated services and repositories. This separation of concerns promotes maintainability, testability, and clean architecture by isolating data access logic from UI components. Services handle the API calls and business logic, while repositories abstract data management, making the codebase scalable and easier to manage, especially for enterprise-level applications.

### Web Page
The web application consists of two primary pages: a welcoming landing page and a calculation page. The landing page serves as the initial point of contact, providing a friendly introduction to users. From there, users can navigate to the calculation page, which includes a clear and accessible button allowing them to return to the main landing screen at any time.

On the calculation page, users can enter their salary into an input box designed with robust validation. This validation ensures that users cannot submit empty values or non-decimal inputs, preventing errors and enhancing the user experience by providing immediate feedback.

Calculation results are only displayed after the user submits a valid input and the computation completes successfully, maintaining a clean and uncluttered interface. Additionally, users have the option to clear the input and results, allowing for multiple calculations without page refreshes.

### Styling

From a design perspective, I adopted a mobile-first approach to styling. This ensures the interface is fully responsive and renders appropriately across a wide range of screen sizes and devices, from smartphones to desktop monitors. The color scheme was carefully selected to match the branding of the Commify website, helping maintain a consistent business identity. Furthermore, I designed a custom icon inspired by the same visual style, adding a cohesive and professional touch to the overall look and feel of the application.

To promote consistency and reduce repetitive code throughout the application, I implemented a `app-main-content-wrapper` component. This wrapper serves as a reusable container that standardizes the layout and structure of the primary content area across all pages, minimizing boilerplate and ensuring a unified user experience.

### UI: Web

![alt text](income-tax-frontend/media/images/landing-page.png)
![alt text](income-tax-frontend/media/images/calculator-page.png)
![alt text](income-tax-frontend/media/images/calculator-page-result.png)
![alt text](income-tax-frontend/media/images/calculator-page-null-value.png)

### UI: Mobile

![alt text](income-tax-frontend/media/images/landing-page-mobile.png) ![alt text](income-tax-frontend/media/images/calculator-page-mobile.png)


---

### Testing

Throughout the development process, I employed both manual and automated testing to ensure the application functioned correctly and met the specified requirements. Manual testing involved interacting with the application directly, simulating user actions to verify features such as form validations, navigation, and data calculations behaved as expected.

In parallel, I implemented unit tests to automatically validate the functionality of individual components, services, and business logic in isolation. This helped catch regressions early and ensured that each part of the codebase maintained its intended behavior as the project evolved.

Whenever issues arose during testing or development, I utilized a combination of debugging tools to diagnose and resolve them efficiently. For frontend issues, I inspected the browser’s developer console to check for errors, warnings, or unexpected behavior in real time. Additionally, I leveraged breakpoints and step-through debugging within the integrated development environment (IDE) to closely examine the flow of execution and the state of variables at critical points in the code. This comprehensive approach to testing and debugging helped maintain a high level of code quality and reliability throughout the project.

---

## Conclusion

This project demonstrates a well-structured and thoughtfully implemented Income Tax Calculator application, built with scalability, maintainability, and enterprise readiness in mind. By leveraging PostgreSQL as the database backend, the application benefits from advanced SQL capabilities, robust transaction handling, and seamless integration with C# ASP.NET through Entity Framework Core. The use of migration scripts and data seeding further enhances maintainability and supports smooth evolution of the database schema over time.

On the backend, clear separation between entity models and Data Transfer Objects (DTOs) ensures a clean API contract and safeguards internal data structures. Custom interfaces like IRequestHandler and IConverter promote consistent patterns, improve code organization, and prepare the system for future expansions or architectural changes.

The frontend follows modern Angular practices with component-based design, strict input validation, and a mobile-first approach to styling, ensuring a responsive and user-friendly experience aligned with business branding. The use of NSwag to generate API clients streamlines communication between frontend and backend, reducing manual effort and minimizing errors.

Rigorous testing—combining manual exploratory testing and automated unit tests—alongside effective debugging practices, ensures the application functions reliably and meets its functional requirements.

Overall, this project not only meets its functional goals but is built on solid architectural principles and best practices, making it well-suited for growth, maintenance, and adaptation in an enterprise context.

