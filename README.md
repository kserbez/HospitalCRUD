# Hospital Management API

Simple REST API for managing patients and hospital departments, including patient transfers between departments and historical assignment queries.

## Goal of the project

Implement required CRUD operations + transfer functionality + historical query ("which patients were in department X on date Y").


## Quick Start

1. Clone the repo and go to the solution folder
    ```bash
    git clone <repository-url>
    cd ./Hospital/API

2. Restore the project
    ```bash
    dotnet restore

2. Run database migrations and initial data population
    ```bash
	dotnet ef database update

3. Run the project

    ```bash
    dotnet run