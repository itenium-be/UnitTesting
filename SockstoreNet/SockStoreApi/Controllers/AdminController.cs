using Application.Commands;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SockStoreApi.Response;

namespace SockStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController(ICreateProduct createProduct, IUpdateProduct updateProduct, IUpdateStock updateStock) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create(CreateProductCommand command)
    {
        var result = await createProduct.Create(command);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponse>> Update(int id, UpdateProductCommand command)
    {
        if (id != command.Id.Value) return BadRequest("Id mismatch");
        var result = await updateProduct.Update(command);
        return Ok(ProductResponse.FromProduct(result));
    }

    [HttpPatch("{id:int}/stock")]
    public async Task<ActionResult<ProductResponse>> UpdateStock(int id, UpdateStockCommand command)
    {
        if (id != command.Id.Value) return BadRequest("Id mismatch");
        var result = await updateStock.UpdateStock(command);
        return Ok(ProductResponse.FromProduct(result));
    }
}
