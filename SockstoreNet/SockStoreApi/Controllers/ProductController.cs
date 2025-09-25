using Application.Query;
using Microsoft.AspNetCore.Mvc;
using SockStoreApi.Response;
using Vocabulary;

namespace SockStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductQuery productQuery) : ControllerBase
{
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
}
