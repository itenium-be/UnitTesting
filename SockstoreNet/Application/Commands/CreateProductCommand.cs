using Vocabulary;

namespace Application.Commands;

public record CreateProductCommand(Name Name, Category Category, Price Price, Stock Stock);
