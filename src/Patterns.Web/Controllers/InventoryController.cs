using Microsoft.AspNetCore.Mvc;
using Patterns.InventoryManagement;

namespace Patterns.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryCommandFactory _commandFactory;
    private readonly IInventoryContext _context;

    public InventoryController(IInventoryCommandFactory commandFactory, IInventoryContext context)
    {
        _commandFactory = commandFactory;
        _context = context;
    }

    [HttpGet("items")]
    public IActionResult GetItems()
    {
        var items = _context.GetBooks();
        return Ok(new { success = true, items });
    }

    [HttpGet("items/{id}")]
    public IActionResult GetItem(string id)
    {
        var item = _context.GetBooks()[0];
        if (item == null)
            return NotFound(new { success = false, message = "Item not found" });
        
        return Ok(new { success = true, item });
    }

    [HttpPost("items")]
    public IActionResult AddItem([FromBody] AddItemRequest request)
    {
        if (string.IsNullOrEmpty(request.ItemId) || string.IsNullOrEmpty(request.ItemName) || request.Quantity < 0)
            return BadRequest(new { success = false, message = "Invalid item data" });

        _context.AddBook("test");
        return Ok();
    }

    [HttpPut("items/{id}")]
    public IActionResult UpdateQuantity(string id, [FromBody] UpdateQuantityRequest request)
    {
        var item = _context.UpdateQuantity("test", request.Quantity);
        if (item == null)
            return NotFound(new { success = false, message = "Item not found" });

        if (request.Quantity < 0)
            return BadRequest(new { success = false, message = "Quantity cannot be negative" });
        return Ok(new { success = true, item });
    }

    [HttpGet("help")]
    public IActionResult GetHelp()
    {
        return Ok(new 
        { 
            success = true,
            commands = new[] 
            {
                new { command = "add", description = "Add a new inventory item" },
                new { command = "get", description = "Get an inventory item" },
                new { command = "update", description = "Update item quantity" },
                new { command = "list", description = "List all items" },
                new { command = "help", description = "Show help" }
            }
        });
    }
}

public class AddItemRequest
{
    public string ItemId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

public class UpdateQuantityRequest
{
    public int Quantity { get; set; }
}
