using Application.Commands;
using Vocabulary;

namespace Application.Interfaces;

public interface ICreateProduct
{
    Task<Product> Create(CreateProductCommand command);
}
