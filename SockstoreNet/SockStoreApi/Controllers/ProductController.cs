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
    public ActionResult<ProductResponse> Create(CreateProductCommand command)
    {
        var result = createProduct.Create(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public ActionResult<ProductResponse> GetById(string id)
    {
        var product = productQuery.FindById(new ProductId(id));
        if (product is null) return NotFound();
        return Ok(ProductResponse.FromProduct(product));
    }

    [HttpGet]
    public IEnumerable<ProductResponse> GetAll()
    {
        return productQuery.FindAll().Select(ProductResponse.FromProduct);
    }

    [HttpPut("{id}")]
    public ActionResult<ProductResponse> Update(string id, UpdateProductCommand command)
    {
        if (id != command.Id.Value) return BadRequest("Id mismatch");
        var result = updateProduct.Update(command);
        return Ok(ProductResponse.FromProduct(result));
    }

    [HttpPatch("{id}/voorraad")]
    public ActionResult<ProductResponse> UpdateVoorraad(string id, UpdateVoorraadCommand command)
    {
        if (id != command.Id.Value) return BadRequest("Id mismatch");
        var result = updateVoorraad.UpdateVoorraad(command);
        return Ok(ProductResponse.FromProduct(result));
    }
}
