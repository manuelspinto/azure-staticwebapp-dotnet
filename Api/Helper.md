## Start API server on a specific port

```bash
func start --cors "*" --port 7071
```

## Start Blazor Wasm

```bash
dotnet run
```

## Start Proxy server triggering API server

```bash
swa start http://localhost:5000 --api-location /api
```

## Start Proxy server pointing to existing ports

```bash
swa start http://localhost:5000 --api-location http://localhost:7071
```
