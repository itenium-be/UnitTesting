using Vocabulary;

namespace Application.Commands;

public record UpdateProductCommand(ProductId Id, Name Naam, Category Category, Price Price, Stock Stock);
