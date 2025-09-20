using System.Diagnostics;
using Application.Domain;
using Application.Query;
using Application.UseCases;
using SockStoreTests.Mock;
using Vocabulary;

namespace SockStoreTests.Acceptance;

/// <summary>
/// Fast
/// Independent
/// Repeatable
/// Self-Validating
/// Thorough (or Timely)
/// </summary>
public class FirstTests
{
    /// <summary>
    /// Repeatable: Without code changes test results do not change
    ///
    /// During a production outage last Saturday we needed to do an
    /// emergency release. Our CI/CD build was blocked by this test
    /// </summary>
    [Test]
    public async Task Repeatable()
    {
        var mockRepo = new MockProductRepository([
            new ProductAggregate(new ProductId(1), new Name("Product1"), new Category("Shoes"), new Price(100), new Stock(10)),
        ]);
        var query = new ProductQuery(mockRepo);

        var result = await query.FindById(new ProductId(1), CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Price.Value, Is.EqualTo(100));
    }

    /// <summary>
    /// Self-Validating: No manual steps allowed!
    ///
    /// But... We're testing the generation of an
    /// Excel here, how can this be automated!!??
    /// </summary>
    [Test]
    public async Task SelfValidating()
    {
        var mockRepo = new MockProductRepository([
            new ProductAggregate(new ProductId(1), new Name("React Socks"), new Category("Frontend"), new Price(100), new Stock(10)),
            new ProductAggregate(new ProductId(2), new Name("Vue Socks"), new Category("Frontend"), new Price(50), new Stock(2)),
            new ProductAggregate(new ProductId(3), new Name("Angular Socks"), new Category("Frontend"), new Price(350), new Stock(5)),
        ]);
        var excelCreator = new CreateProductListingUseCase(mockRepo);

        var result = await excelCreator.Create();

        string fileName = $"_products-{DateTime.Now:yyyy-MM-dd.HHumm}.xlsx";
        result.SaveAs(fileName);

        // Check if the Excel looks as expected
        var startInfo = new ProcessStartInfo()
        {
            FileName = fileName,
            UseShellExecute = true
        };
        Process.Start(startInfo);
    }
}
