# Appointment Booking System - Deployment Guide

## Introduction

This document provides step-by-step instructions on how to set up, deploy, and use the Appointment Booking System. The project consists of a REST API that connects to a PostgreSQL database to return available appointment slots for customers.

## Prerequisites

Before running the project, ensure you have the following installed:

- **Docker & Docker Compose**
- **.NET SDK 8.0+** (for local execution using Visual Studio)
- **PostgreSQL client** (such as pgAdmin or DBeaver for database inspection)
- **Node.js & npm** (for running tests in `test-app`)
- **Command-Line Interface (CMD, Terminal, or PowerShell) for executing API requests**

## Getting Started

### **1. Running the project using Visual Studio**
If you prefer running the API directly in Visual Studio:
1. Open `AppointmentBooking.sln` in **Visual Studio**.
2. Set `AppointmentBooking.API` as the **Startup Project**.
3. Run the project (F5 or Ctrl+F5).
4. The API will start on `http://localhost:3000`.
5. Open `http://localhost:3000/swagger/index.html` to view and test the API using **Swagger UI**.

### **2. Running the project using Docker**
For a containerized setup, follow these steps:

1. Navigate to the project root directory.
2. Run the following command to start both the API and the database:
   ```sh
   docker-compose up --build
   ```
   This will:
   - Build the .NET API image.
   - Start a PostgreSQL database preloaded with sample data.
   - Run the API on `http://localhost:3000`.

3. Verify that the API is running.
   - **For Linux/macOS users (Terminal):**
     ```sh
     curl -X POST http://localhost:3000/calendar/query -H "Content-Type: application/json" -d '{
         "date": "2024-05-03",
         "products": ["SolarPanels", "Heatpumps"],
         "language": "German",
         "rating": "Gold"
     }'
     ```
   - **For Windows CMD users:**
     ```cmd
     curl -X POST http://localhost:3000/calendar/query -H "Content-Type: application/json" -d "{ \"date\": \"2024-05-03\", \"products\": [\"SolarPanels\", \"Heatpumps\"], \"language\": \"German\", \"rating\": \"Gold\" }"
     ```
   - **For Windows PowerShell users:**
     ```powershell
     $body = @{
         date = "2024-05-03"
         products = @("SolarPanels", "Heatpumps")
         language = "German"
         rating = "Gold"
     } | ConvertTo-Json -Depth 10

     Invoke-RestMethod -Uri "http://localhost:3000/calendar/query" -Method Post -Headers @{ "Content-Type" = "application/json" } -Body $body
     ```
   
   Expected response:
   ```json
   [
       {
           "available_count": 1,
           "start_date": "2024-05-03T10:30:00.000Z"
       },
       {
           "available_count": 1,
           "start_date": "2024-05-03T11:00:00.000Z"
       },
       {
           "available_count": 1,
           "start_date": "2024-05-03T11:30:00.000Z"
       }
   ]
   ```

### **3. Running Tests**
To verify the implementation, tests must be executed manually:
1. Ensure the API is running on `http://localhost:3000`.
2. Navigate to the `test-app` directory (located next to the solution file `AppointmentBooking.sln`).
3. Install dependencies:
   ```sh
   npm install
   ```
4. Run tests:
   ```sh
   npm run test
   ```

The test script connects to `http://localhost:3000/calendar/query` and runs predefined test cases. All **6 tests have passed successfully.**

### **4. Stopping and Cleaning Up**
To stop all running containers:
```sh
docker-compose down
```

To remove all associated volumes and clean up data:
```sh
docker volume prune -f
```

### **5. Additional Notes**
- **Database Connection:** The API connects to the PostgreSQL database inside Docker. If needed, you can access the database using tools like **pgAdmin** or **DBeaver**.
- **Custom Data:** The test database supports adding additional sample data to validate different scenarios.
- **Test Environment:** The `test-app` directory contains a Node.js script (`test.js`) that executes API requests and validates expected responses.
- **Windows Users:** If using CMD, use the `curl` format for Windows as specified above. If using PowerShell, prefer `Invoke-RestMethod`.

## Conclusion
By following this guide, you will be able to set up, run, and test the appointment booking API seamlessly. The system is designed to handle dynamic appointment scheduling and has been tested for various edge cases. If needed, additional data can be inserted into the database for further validation.

