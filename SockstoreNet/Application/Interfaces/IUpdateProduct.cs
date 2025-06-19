using Application.Commands;
using Vocabulary;

namespace Application.Interfaces;

public interface IUpdateProduct
{
    Task<Product> Update(UpdateProductCommand command);
}
