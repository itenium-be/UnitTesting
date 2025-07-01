using Application.Commands;
using Vocabulary;

namespace Application.Interfaces;

public interface IUpdateStock
{
    Task<Product> UpdateStock(UpdateStockCommand command);
}
