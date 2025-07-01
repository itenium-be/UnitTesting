using Application.Commands;
using Application.Interfaces;
using Application.Ports;
using Vocabulary;

namespace Application.UseCases;

public class UpdateStockUseCase(IProductPort productPort) : IUpdateStock
{
    public async Task<Product> UpdateStock(UpdateStockCommand command)
    {
        if (command.Stock.Value < 0)
        {
            throw new InvalidOperationException("Stock cannot be negative.");
        }
        var product = await productPort.FindById(command.Id, CancellationToken.None);
        if (product == null)
            throw new ArgumentException($"Product with id {command.Id} was not found");

        product.UpdateStock(command.Stock);
        await productPort.Save(product);
        return product.ToProduct();
    }
}
