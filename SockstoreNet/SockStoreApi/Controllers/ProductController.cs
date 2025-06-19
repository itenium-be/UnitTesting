using Application.Commands;
using Application.Interfaces;
using Application.Query;
using Microsoft.AspNetCore.Mvc;
using SockstoreApi.Response;
using Vocabulary;

namespace SockstoreNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ICreateProduct createProduct, IUpdateProduct updateProduct, IUpdateVoorraad updateVoorraad, IProductQuery productQuery) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create(CreateProductCommand command)
    {
        var result = await createProduct.Create(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetById(string id, CancellationToken cancellationToken)
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

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductResponse>> Update(string id, UpdateProductCommand command)
    {
        if (id != command.Id.Value) return BadRequest("Id mismatch");
        var result = await updateProduct.Update(command);
        return Ok(ProductResponse.FromProduct(result));
    }

    [HttpPatch("{id}/voorraad")]
    public async Task<ActionResult<ProductResponse>> UpdateVoorraad(string id, UpdateVoorraadCommand command)
    {
        if (id != command.Id.Value) return BadRequest("Id mismatch");
        var result = await updateVoorraad.UpdateVoorraad(command);
        return Ok(ProductResponse.FromProduct(result));
    }
}
