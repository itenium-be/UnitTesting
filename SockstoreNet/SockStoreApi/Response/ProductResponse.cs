using Vocabulary;

namespace SockstoreApi.Response;

public record ProductResponse(string Id, string Naam, string Categorie, decimal Prijs, int Voorraad) {
    public static ProductResponse FromProduct(Product product) {
        return new ProductResponse(product.Id.Value, product.Naam.Value, product.Categorie.Value, product.Prijs.Value, product.Voorraad.Value);
    }
}