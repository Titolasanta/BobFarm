# Project Setup Guide

This guide will help you bring up the project with the required commands for both the backend (ASP.NET) and frontend (React). It also includes steps to run the tests for the backend.

---



### **Step 1: Build and Run Docker Containers**
Run the following command to bring up PostgreSQL with Docker Compose:

```bash
cd .../CornFarmBackend
docker-compose up
```


## 2. **Backend**

Run the following command to run the Server:

```bash
cd .../CornFarmBackend
dotnet restore
dotnet ef database update
dotnet run
```

## 3. **Frontend**

Run the following command to run the frontend:

```bash
cd .../front/front
npm install
npm start
```


## 4. **Tests**

Run the following command to test the Backend:

```bash
cd .../CornFarmBackend.Tests
dotnet tests
```
