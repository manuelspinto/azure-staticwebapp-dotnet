# .NET Azure Static Web App Application with Blazor and C# functions using Shared Authentication logic

This application demonstrates the use .NET authentication shared between the frontend (Blazor) and the backend functions (C#). This is the base repository for the [Medium article]() explaining the different aspects and challenges of using .NET authentication in Azure Static WebApps.

The starting template for this project was [blazor-starter](https://github.com/staticwebdev/blazor-starter).


## Folder Structure

- **Client**: The Blazor WebAssembly sample application
- **Api**: A C# Azure Functions API, which the Blazor application will call
- **Shared**: A C# class library with a shared data model between the Blazor and Functions application. Contains Authentication models and logic shared between Client and Api.

## Start API server on a specific port

```bash
\Api> func start --cors "*" --port 7071
```

## Start Blazor Wasm on default port: 5000

```bash
\Client> dotnet run
```

The Static Web Apps CLI (`swa`) starts a proxy on port 4280 that will forward static site requests to the Blazor server on port 5000 and requests to the `/api` endpoint to the Functions server. 

## Start Proxy server pointing to existing ports

```bash
swa start http://localhost:5000 --api-location http://localhost:7071
```

## Alternative start of Proxy server triggering API server

```bash
swa start http://localhost:5000 --api-location /api
```
