using Infrastructure.Db;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using Vocabulary;

namespace SockStoreTests.First;

/// <summary>
/// FIRST: Independent
/// Tests should not interact with one another.
///
/// When running these tests separately they are green.
/// But when we run them together, some of them fail :(
/// </summary>
public class FirstTestsIndependent
{
    private MsSqlContainer _msSqlContainer;
    private ProductDbContext _dbContext;
    private ProductRepository _repository;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("Strong_Password_123!")
            .Build();

        await _msSqlContainer.StartAsync();
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseSqlServer(_msSqlContainer.GetConnectionString())
            .Options;

        _dbContext = new ProductDbContext(options);
        await _dbContext.Database.EnsureCreatedAsync();
        _repository = new ProductRepository(_dbContext);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _dbContext.DisposeAsync();
        await _msSqlContainer.DisposeAsync();
    }

    [Test]
    public async Task SockPrice_HappyPath()
    {
        var productEntity = new ProductEntity
        {
            Name = "React Socks",
            Price = 99.99m,
        };
        await _dbContext.Products.AddAsync(productEntity);
        await _dbContext.SaveChangesAsync();

        var result = await _repository.FindById(new ProductId(productEntity.Id), CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id.Value, Is.EqualTo(productEntity.Id));
        Assert.That(result.Price.Value, Is.EqualTo(99.99m));
    }

    [Test]
    public async Task GlobalDiscount_IsAppliedToTheSockPrice()
    {
        var productEntity = new ProductEntity
        {
            Name = "Angular Socks",
            Price = 100m,
        };
        await _dbContext.Products.AddAsync(productEntity);

        var parameterEntity = new ParameterEntity()
        {
            Key = "GlobalDiscount",
            Value = "5",
        };
        await _dbContext.Parameters.AddAsync(parameterEntity);
        await _dbContext.SaveChangesAsync();

        var result = await _repository.FindById(new ProductId(productEntity.Id), CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Price.Value, Is.EqualTo(95m));
    }

    [Test]
    public async Task SockPrice_IsRoundedToTwoDecimals()
    {
        var productEntity = new ProductEntity
        {
            Name = "Vue Socks",
            Price = 2.999m,
        };
        await _dbContext.Products.AddAsync(productEntity);
        await _dbContext.SaveChangesAsync();

        var result = await _repository.FindById(new ProductId(productEntity.Id), CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Price.Value, Is.EqualTo(3m));
    }
}
