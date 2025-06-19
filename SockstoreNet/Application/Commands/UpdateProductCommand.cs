using Vocabulary;

namespace Application.Commands;

public record UpdateProductCommand(ProductId Id, Naam Naam, Categorie Categorie, Prijs Prijs, Voorraad Voorraad);
