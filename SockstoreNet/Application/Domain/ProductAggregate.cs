using Vocabulary;

namespace Application.Domain;

public class ProductAggregate(ProductId id, Naam name, Categorie category, Prijs price, Voorraad stock) {
    public ProductId Id { get; private set; } = id;
    public Naam Naam { get; private set; } = name;
    public Categorie Categorie { get; private set; } = category;
    public Prijs Prijs { get; private set; } = price;
    public Voorraad Voorraad { get; private set; } = stock;

    public void UpdateStock(Voorraad newStock) {
        Voorraad = newStock;
    }

    public void UpdateProduct(Naam name, Categorie category, Prijs price) {
        Naam = name;
        Categorie = category;
        Prijs = price;
    }

    public Product ToProduct() {
        return new Product(
            Id,
            Naam,
            Categorie,
            Prijs,
            Voorraad
        );
    }
}
