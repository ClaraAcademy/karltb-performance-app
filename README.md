# PerformanceApp

This project is an implementation of the _Clara Academy_ assignment **Övningsuppgift - Applikationsutveckling**.

The project is largely divided into a backend and a frontend.

The backend is implemented as an ASP.NET Core Web API application written in C# and using Entity Framework Core for data access.

The frontend is implemented as a React application written in TypeScript.

## Prerequisites

Download the file `Priser - portföljberäkning.xlsx` from SharePoint and place it in `C:\Data`, as the project expects to find it there when seeding the database.

This means that the full path to the file should be `C:\Data\Priser - portföljberäkning.xlsx`.

## Subprojects

The project is divided into six subprojects, which are described in the below table:

| Name                         | Description                                       |
| ---------------------------- | ------------------------------------------------- |
| `performanceapp.client`      | React frontend application.                       |
| `performanceapp.Data`        | Data-access layer using Entity Framework Core.    |
| `performanceapp.Data.Test`   | Unit tests for the data-access layer.             |
| `performanceapp.Seeder`      | Seeder for the database in the data-access layer. |
| `performanceapp.Server`      | Backend server application.                       |
| `performanceapp.Server.Test` | Unit tests for the backend server application.    |

## Running the Project

The project has been verified to run in Visual Studio and Visual Studio Code.

Running the project will start both the backend and the frontend applications.

The frontend is executed with a Vite development server with an associated debug terminal.

Running the project also creates and seeds the database with sample data from the assignment.

### Instructions

Below are instructions for running the project in both Visual Studio and Visual Studio Code.

| Environment        | Instructions                                   |
| ------------------ | ---------------------------------------------- |
| Visual Studio      | Open the project and press the "Start" button. |
| Visual Studio Code | Open the project and press `CTRL+F5`.          |

## Notes

As the project is a work in progress, it might be a bit finicky.

You might need to rebuild the solution or restart the development servers a couple of times for everything to work as expected.
