## Task List Application

### Overview

Th Task List Application is designed to help users manage their tasks and subtasks efficiently. The application is built using a .NET backend and a React frontend. The backend API provides endpoints for creating, reading, updating, and deleting tasks and subtasks, while the frontend offers a user interface to interact with these functionalities.

### Project Structure

- **Backend**: The backend is developed using ASP.NET, running on port `7059`. It interacts with a SQL database using Entity Framework Core.

- **Frontend**: The frontend is built with React, running on port `5173`. It communicates with the backend API to perform CRUD operations on tasks and subtasks.

- **Unit Tests**: The project includes 24 unit tests to ensure the reliability of the API endpoints.

### Prerequisites

- .NET 8.0 SDK or later
- Node.js and npm

### Installation and Setup

1. **Clone the repository**

2. **Setup Backend**
   ```sh
   cd TaskListApplication.Server
   dotnet restore
   dotnet ef database update
   dotnet run --urls=http://localhost:7059
   ```

3. **Setup Frontend**
   ```sh
   cd ../TaskListApplication.Client
   npm install
   npm start
   ```

### API Endpoints

#### TasksController

- **GET /api/tasks/all**
  - Retrieves all tasks.
  - **Response**: `200 OK`, List of `TaskDto`

- **GET /api/tasks/{taskId}**
  - Retrieves a task by ID.
  - **Response**: `200 OK`, `TaskDto`
  - **Error Responses**: `404 Not Found`, `400 Bad Request`

- **POST /api/tasks/create**
  - Creates a new task.
  - **Request Body**: `TaskCreateDto`
  - **Response**: `201 Created`, Task ID
  - **Error Responses**: `400 Bad Request`

- **PUT /api/tasks/update/{taskId}**
  - Updates an existing task.
  - **Request Body**: `TaskDto`
  - **Response**: `204 No Content`
  - **Error Responses**: `404 Not Found`, `400 Bad Request`

- **DELETE /api/tasks/delete/{taskId}**
  - Deletes a task by ID.
  - **Response**: `204 No Content`
  - **Error Responses**: `404 Not Found`, `400 Bad Request`

#### SubTasksController

- **GET /api/subtasks/all**
  - Retrieves all subtasks.
  - **Response**: `200 OK`, List of `SubTaskDto`

- **GET /api/subtasks/{subTaskId}**
  - Retrieves a subtask by ID.
  - **Response**: `200 OK`, `SubTaskDto`
  - **Error Responses**: `404 Not Found`, `400 Bad Request`

- **POST /api/subtasks/create**
  - Creates a new subtask.
  - **Request Body**: `SubTaskCreateDto`
  - **Response**: `201 Created`, SubTask ID
  - **Error Responses**: `400 Bad Request`, `404 Not Found`

- **PUT /api/subtasks/update/{subTaskId}**
  - Updates an existing subtask.
  - **Request Body**: `SubTaskDto`
  - **Response**: `204 No Content`
  - **Error Responses**: `404 Not Found`, `400 Bad Request`

- **DELETE /api/subtasks/delete/{subTaskId}**
  - Deletes a subtask by ID.
  - **Response**: `204 No Content`
  - **Error Responses**: `404 Not Found`, `400 Bad Request`

### Frontend Features

- **Task Management**
  - View all tasks.
  - Create, update, and delete tasks.
  - Mark tasks as complete/incomplete.

- **Subtask Management**
  - View all subtasks for a task.
  - Create, update, and delete subtasks.
  - Mark subtasks as complete/incomplete.

### Running Unit Tests

The backend includes 24 unit tests to verify the functionality of the API endpoints.

1. **Navigate to the test project directory**
   ```sh
   cd TaskListApplication.Tests
   ```

2. **Run the tests**
   ```sh
   dotnet test
   ```