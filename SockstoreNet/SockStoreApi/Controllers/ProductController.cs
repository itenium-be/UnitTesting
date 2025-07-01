using Application.Commands;
using Application.Interfaces;
using Application.Query;
using Microsoft.AspNetCore.Mvc;
using SockStoreApi.Response;
using Vocabulary;

namespace SockStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ICreateProduct createProduct, IUpdateProduct updateProduct, IUpdateStock updateStock, IProductQuery productQuery) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create(CreateProductCommand command)
    {
        var result = await createProduct.Create(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await productQuery.FindById(new ProductId(id), cancellationToken);
        if (product is null) return NotFound();
        return Ok(ProductResponse.FromProduct(product));
    }

    [HttpGet]
    public async Task<IEnumerable<ProductResponse>> GetAll(CancellationToken cancellationToken)
    {
        var products = await productQuery.FindAll(cancellationToken);
        return products.Select(ProductResponse.FromProduct);
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
