# Project Setup Guide

This guide will help you bring up the project with the required commands for both the backend (ASP.NET) and frontend (React). It also includes steps to run the tests for the backend.

---

## 1. **Start PostgreSQL Backend with Docker Compose**

To start the PostgreSQL database for your backend using Docker Compose, follow these steps:

### **Step 1: Build and Run Docker Containers**
Run the following command in the project root directory to bring up PostgreSQL with Docker Compose:

```bash
docker-compose up
bash```


## 2. **Backend**

```bash
cd .../CornFarmBackend
dotnet restore
dotnet ef database update
dotnet run
bash```

## 3. **Frontend**

```bash
cd .../front/front
npm install
npm start
bash```


## 4. **Tests**

```bash
cd .../CornFarmBackend.Tests
dotnet tests
bash```
