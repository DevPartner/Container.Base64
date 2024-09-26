# Base64 Encoding SPA - Task Description

## Project Overview

A simple Single Page Application (SPA) with .NET 8 backend and Angular frontend to encode user input into Base64, simulating a long-running job. The encoding is performed character by character with random pauses on the backend (1-5 seconds per character) and streamed back to the client in real-time. The client receives and displays each character in a text box in real-time. The user can cancel the encoding process if required.

## Core Features
- **SPA (Single Page Application)**: Angular frontend that interacts with a .NET 8 API backend for encoding text to Base64.
- **Backend (API)**: ASP.NET Core 8 for encoding the input string to Base64, sent one character at a time with random delays (1-5 seconds).
- **Real-Time Updates**: UI dynamically updates as the API returns one encoded character at a time.
- **Cancelable Operations**: The user can cancel the encoding process if it’s in progress.
- **Dockerized Setup**: Each component (frontend, backend, Nginx) runs in separate Docker containers.
- **Nginx for Basic Authentication**: Nginx container is used as a reverse proxy to handle basic authentication.

---

## Key Implementation Details

- [x] **Clean Architecture**:
  - **Separated layers for API, Core, Contracts, Infrastructure** to adhere to SOLID principles.
  - Clear distinction between application logic and domain logic for maintainability and scalability.

- [x] **Backend in .NET 8**:
  - **API Endpoints** for accepting text input and returning Base64 encoded characters.
  - **Asynchronous Task Management** to simulate heavy processing with random delays for each character.
  - **Cancellation Tokens** used to gracefully handle cancellation requests.
  - **Error Handling & Resilience**: Handles multiple users and misuse scenarios, ensuring system resilience under load.

- [x] **Frontend in Angular**:
  - **User Input Form** with real-time feedback as each character is encoded.
  - **SignalR Streaming** to handle character-by-character updates from the backend.
  - **Responsive UI** with Bootstrap to ensure neat and clean design.

- [x] **Docker Configuration**:
  - **Dockerfiles** for each component (API, Angular, Nginx) to streamline containerization.
  - **Docker Compose** for running all services (frontend, backend, Nginx) in separate containers.


- [x] **Basic Authentication via Nginx**:
  - Nginx container serves as a reverse proxy and manages authentication, restricting access to the backend API.
  - Example credentials for testing:
Login: ki
Password: ki

- [x] **Real-Time Streaming via SignalR (Optional Bonus)**:
  - The use of SignalR to stream each character asynchronously to the client.
  
- [x] **Unit Testing**:
  - Unit tests implemented for backend logic using NUnit.
  
- [x] **Industry Best Practices**:
  - **Dependency Injection**: Leverage default IoC in .NET for handling service dependencies.
  - **Logging via ILogger** for tracking encoding requests, errors, and cancellation.


---

## Technical Stack
- **Frontend**: Angular 16 with Bootstrap 5 for UI design and real-time updates.
- **Backend**: .NET 8 Web API.
- **Containerization**: Docker for all services with Docker Compose for orchestration.
- **Nginx**: For reverse proxy and basic authentication.

---

## Running the Application

### Prerequisites
- Docker and Docker Compose installed on your local machine.

### Steps to Run
1. Clone the repository.
2. Build and run the containers:
   ```bash
   docker-compose up --build
3. Once the containers start successfully navigate to http://localhost

# Default User:
```
User: ki; Password: ki
```

# Future Enhancements
- Implement alternative JWT authorization methods to support advanced authentication scenarios.
- Integrate distributed caching to further optimize.
