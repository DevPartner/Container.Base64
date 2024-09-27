# Base64 Encoding SPA - Task Description

## Project Overview

A simple SPA with a .NET 8 backend and Angular frontend to encode user input to Base64 in real-time. Encoding is done character by character with random delays (1-5 seconds). The client receives and displays each character as it's processed, and users can cancel the operation at any time.


## Core Features
- **Angular SPA**: Frontend interacting with a .NET 8 API for Base64 encoding.
- **Backend API**: Encodes text with random delays, streamed to the frontend in real-time.
- **Real-Time Updates**: UI shows each encoded character as it’s received.
- **Cancelable**: Users can cancel the encoding process.
- **Dockerized Setup**: Frontend, backend, and Nginx run in Docker containers.
- **Basic Auth**: Nginx handles authentication as a reverse proxy.

---

## Key Implementation Details

- **Clean Architecture**: Separated API, Core, and Infrastructure layers.
- **Async Task Handling**: Backend simulates long processing with random delays.
- **SignalR Streaming**: (Optional) Real-time updates via SignalR.
- **Unit Testing**: Backend tested using NUnit.
- **Unit Testing**: 
  - **Frontend**: Angular components and services tested using Karma and Jasmine for unit tests.
  - **Backend**: NUnit.
- **Best Practices**: Dependency Injection, ILogger for logging, and Docker for containerization.

---

## Technical Stack
- **Frontend**: Angular 16 with Bootstrap 5 for UI design and real-time updates.
- **Backend**: .NET 8 Web API, SignalR, MediatR.
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
