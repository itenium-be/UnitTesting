using Vocabulary;

namespace Application.Commands;

public record CreateProductCommand(Naam Naam, Categorie Categorie, Prijs Prijs, Voorraad Voorraad);
