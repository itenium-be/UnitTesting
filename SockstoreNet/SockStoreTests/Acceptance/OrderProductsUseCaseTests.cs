using Application.Domain;
using Application.Query;
using Application.UseCases;
using SockStoreTests.Mock;
using Vocabulary;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace SockStoreTests.Acceptance;

public class OrderProductsUseCaseTests
{
    [Test]
    public async Task StripeSuccessResponse()
    {
        var server = WireMockServer.Start();
        server
            .Given(
                Request.Create()
                    .WithPath("/v1/payment_intents")
                    .UsingPost()
                )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithBodyAsJson(new { id = "pi_test123", status = "succeeded" })
            );

        var httpClient = new HttpClient { BaseAddress = new Uri(server.Urls[0]) };

        var mockRepo = new MockProductRepository([
            new ProductAggregate(new ProductId(1), new Name("Product1"), new Category("Shoes"), new Price(100), new Stock(10)),
        ]);
        var productQuery = new ProductQuery(mockRepo);

        var useCase = new OrderProductsUseCase(productQuery, httpClient);
        var cart = new Cart
        {
            Items = new[] { new CartItem { ProductId = 1, Amount = 2 } }
        };

        await useCase.Order(cart);

        server.Stop();
    }

    [Test]
    public void StripeFailure()
    {
        var server = WireMockServer.Start();
        server
            .Given(Request.Create().WithPath("/v1/payment_intents").UsingPost())
            .RespondWith(Response.Create().WithStatusCode(500));

        var httpClient = new HttpClient { BaseAddress = new Uri(server.Urls[0]) };

        var mockRepo = new MockProductRepository([
            new ProductAggregate(new ProductId(1), new Name("Product1"), new Category("Shoes"), new Price(100), new Stock(10)),
        ]);
        var productQuery = new ProductQuery(mockRepo);

        var useCase = new OrderProductsUseCase(productQuery, httpClient);
        var cart = new Cart
        {
            Items = new[] { new CartItem { ProductId = 1, Amount = 2 } }
        };

        Assert.ThrowsAsync<HttpRequestException>(() => useCase.Order(cart));

        server.Stop();
    }
}