using Microsoft.Extensions.DependencyInjection;
using Patterns.Account;
using Patterns.InventoryManagement;
using Patterns;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure business services
var context = new InventoryContext();
builder.Services.AddSingleton<IInventoryContext, InventoryContext>(c => context);
builder.Services.AddSingleton<IInventoryContextWrite, InventoryContext>(c => context);
builder.Services.AddSingleton<IInventoryContextRead, InventoryContext>(c => context);
builder.Services.AddTransient<InventoryCommand, HelpCommand>();
builder.Services.AddTransient<InventoryCommand, AddInventoryCommand>();
builder.Services.AddTransient<InventoryCommand, GetInventoryCommand>();
builder.Services.AddTransient<InventoryCommand, UpdateQuantityCommand>();
builder.Services.AddTransient<InventoryCommand, QuitCommand>();
builder.Services.AddTransient<InventoryCommand, UnknownCommand>();
builder.Services.AddTransient<IInventoryCommandFactory, InventoryCommandFactory>();
builder.Services.AddTransient<ICatalogService, CatalogService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/index.html"));
app.MapGet("/api", () => new { message = "Patterns Web API", version = "1.0" });

app.Run();
