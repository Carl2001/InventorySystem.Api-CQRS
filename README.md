# Inventory System API

## Overview
A robust, containerized RESTful API developed in .NET for inventory and product management. The project is strictly designed around **SOLID principles**, **Clean Code** standards, and the **CQRS (Command Query Responsibility Segregation)** architectural pattern to ensure scalability and maintainability.

## Architecture & Tech Stack
*   **Framework:** .NET (C#)
*   **Queries (Read Operations):** Entity Framework Core (AsNoTracking for optimized read performance).
*   **Commands (Write Operations):** Dapper (Utilizing granular SQL transactions for inventory data integrity).
*   **Database:** SQL Server 2022 (Dockerized).
*   **Security:** OAuth2 Authentication (JWT Bearer tokens).
*   **Testing:** xUnit and Moq (Focusing on isolated controller logic and mocked interfaces).

---

## How to Run (Docker Compose)

The environment is fully containerized. The application utilizes Entity Framework Core Migrations to automatically build the database schema upon startup.

1. Open a terminal in the root directory.
2. Execute the following command to build and start the API and SQL Server simultaneously:

    docker-compose up -d --build

3. The API will be available at http://localhost:8080.
4. **API Documentation (Swagger):** Navigate to http://localhost:8080/swagger to explore and test the endpoints.

## Local Debugging & Developer Experience (DX)

To optimize the developer loop and avoid dynamic port obfuscation in Docker, local debugging is handled by running the API natively while keeping the database containerized.

1. Ensure the SQL Server container is running via Docker Desktop (port 1433).
2. Open the solution in **Visual Studio 2022**.
3. Select the `http` profile (not the Container/Docker profile) from the debug dropdown.
4. Run the application. 
5. Use the provided `InventorySystem.Api.http` file to execute end-to-end tests directly from the IDE.

---

## Spec-Driven Development & AI Collaboration

AI tools were leveraged strictly as pair-programming assistants to accelerate boilerplate setup and validate architectural patterns. 
*   **Specification First:** The CQRS contract and interface abstractions (IProductQueries) were conceptually defined prior to prompting to ensure strict adherence to SOLID principles.
*   **Architectural Decisions:** The AI initially suggested running the entire debug loop inside Visual Studio's Container mode. I evaluated this and intentionally modified the approach to run the API natively while keeping only the database in Docker. This strategic decision resolved network routing issues and vastly improved the local HTTP testing workflow.

---

## Decision Making (Q&A)

**1. Describe a recent technical decision with incomplete information. What was missing and how did you proceed?**
When designing the InventoryMovement schema, I lacked explicit business definitions regarding currency precision and inventory rollback rules. I decided to enforce a strict `decimal(18,2)` precision using Entity Framework's Fluent API and handled the inventory stock updates via a strict Dapper SQL transaction. This ensures immediate data integrity and prevents silent truncation warnings, without over-engineering features that haven't been requested yet.

**2. When two technical approaches seem equally valid, what criteria do you use to choose one?**
I evaluate them based on **Maintainability** and **Separation of Concerns**. For example, choosing between a pure EF Core approach versus a Micro-ORM (Dapper). I opted for a hybrid CQRS approach. While EF Core is exceptional for rapid, trackable reads (Queries), Dapper provides the raw execution speed and granular transaction control required for critical write operations (Commands). 

**3. Tell a case where you had to reverse or change your own technical decision. What did you learn?**
Initially, I relied on Docker Desktop's dynamic port allocation for the API container during local debugging. This caused severe instability when running automated `.http` file tests, as the port changed on every build. I reversed this decision by explicitly binding the Kestrel ports and executing the API locally outside the container during development. I learned that pure container isolation should never compromise the local Developer Experience (DX) and debugging speed.