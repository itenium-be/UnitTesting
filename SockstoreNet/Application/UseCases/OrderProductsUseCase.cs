using Application.Query;
using System.Net.Http.Json;
using Vocabulary;

namespace Application.UseCases;

public class Cart
{
    public CartItem[] Items { get; set; } = [];
}

public class CartItem
{
    public int ProductId { get; set; }
    public int Amount { get; set; }
}

public class OrderProductsUseCase(IProductQuery productQuery, HttpClient httpClient)
{
    public async Task Order(Cart cart)
    {
        decimal cost = 0;
        foreach (var item in cart.Items)
        {
            var product = await productQuery.FindById(new ProductId(item.ProductId), CancellationToken.None);
            cost += product!.Price.Value * item.Amount;
        }

        var paymentRequest = new { amount = cost, currency = "usd" };
        var response = await httpClient.PostAsJsonAsync("/v1/payment_intents", paymentRequest);
        response.EnsureSuccessStatusCode();

        // TODO: should probably do something depending on the response
        string responseBody = await response.Content.ReadAsStringAsync();

        // TODO: email client
        // TODO: check if stock is sufficient & update stock
        // TODO: send event to start shipping
    }
}
