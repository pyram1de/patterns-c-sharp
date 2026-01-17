# Web Application Setup

Your C# Patterns console application has been elevated to a web application! 🎉

## What's New

### New Project: `Patterns.Web`
An ASP.NET Core web application that exposes your business logic through a REST API and provides a modern web UI.

## Project Structure

```
src/
├── Patterns/              # Core business logic (unchanged)
│   ├── Account/
│   └── InventoryManagement/
├── Patterns.Tests/        # Unit tests (unchanged)
└── Patterns.Web/          # NEW: Web application
    ├── Controllers/       # API endpoints
    ├── wwwroot/          # Static files (HTML, CSS, JS)
    ├── Program.cs        # Web app configuration
    └── Patterns.Web.csproj
```

## Running the Web Application

### Option 1: Using dotnet CLI

```bash
cd src/Patterns.Web
dotnet run
```

The app will start at `https://localhost:5001` (or `http://localhost:5000` in development)

### Option 2: Using Visual Studio

1. Open `Patterns.sln`
2. Right-click on `Patterns.Web` project
3. Select "Set as Startup Project"
4. Press F5 to run

## Features

### Web UI (`/index.html`)
- **Items Tab**: View all inventory items
- **Add Item Tab**: Add new items to inventory
- **Help Tab**: View API documentation

### REST API (`/api/inventory`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/inventory/items` | Get all items |
| GET | `/api/inventory/items/{id}` | Get specific item |
| POST | `/api/inventory/items` | Add new item |
| PUT | `/api/inventory/items/{id}` | Update item quantity |
| DELETE | `/api/inventory/items/{id}` | Delete item |
| GET | `/api/inventory/help` | Get API documentation |

### API Request Examples

**Add Item:**
```bash
curl -X POST http://localhost:5000/api/inventory/items \
  -H "Content-Type: application/json" \
  -d '{"itemId":"ITEM001","itemName":"Widget","quantity":100}'
```

**Get All Items:**
```bash
curl http://localhost:5000/api/inventory/items
```

**Update Quantity:**
```bash
curl -X PUT http://localhost:5000/api/inventory/items/ITEM001 \
  -H "Content-Type: application/json" \
  -d '{"quantity":150}'
```

**Delete Item:**
```bash
curl -X DELETE http://localhost:5000/api/inventory/items/ITEM001
```

## Architecture

### Dependency Injection
- The web app uses the same DI container as the console app
- All business services are registered in `Program.cs`
- Controllers inject required services

### Code Reuse
- The `Patterns.Web` project references the `Patterns` project
- All existing business logic remains unchanged
- The same `InventoryContext`, commands, and services are used

### API Design
- RESTful API following HTTP standards
- JSON request/response bodies
- Proper HTTP status codes (200, 201, 400, 404, etc.)
- CORS enabled for cross-origin requests

## Configuration

### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### CORS Policy
The web app allows requests from all origins. Update `Program.cs` to restrict if needed:

```csharp
options.AddPolicy("AllowAll", policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});
```

## Development Notes

1. **Static Files**: Place HTML/CSS/JS files in `wwwroot/` directory
2. **Controllers**: Add new API endpoints in `Controllers/` folder
3. **Hot Reload**: `dotnet watch run` for auto-reload during development
4. **Testing**: Run existing tests with `dotnet test src/Patterns.Tests/`

## Next Steps

1. Add authentication/authorization if needed
2. Add database persistence layer
3. Expand API with more business logic
4. Deploy to cloud (Azure, AWS, etc.)
5. Add logging and monitoring
6. Implement caching

## Troubleshooting

### Port Already in Use
If port 5000/5001 is in use:
```bash
dotnet run -- --urls="http://localhost:7000"
```

### CORS Issues
If you see CORS errors in the browser console, ensure the API URL in `app.js` matches your running port.

### Build Errors
Ensure you have .NET 8.0 SDK installed:
```bash
dotnet --version
```

---

**Both your console app and web app can coexist!** You can still run the console version from the `Patterns` project while developing the web version.
