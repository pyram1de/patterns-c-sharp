# C# Design Patterns

A playground for learning and experimenting with design patterns in C#.

## Getting Started

### Local Development

```bash
dotnet run --project src/Patterns
```

### GitHub Codespaces

Open this repo in a Codespace - it's preconfigured with .NET 8 and the C# Dev Kit.

## Database

Uses SQLite for persistence - no setup required. The database is created automatically as a local file.

```csharp
using Microsoft.Data.Sqlite;

using var connection = new SqliteConnection("Data Source=patterns.db");
connection.Open();
```