using Migration;
using Vocabulary;

namespace SockStoreTests.First;

/// <summary>
/// FIRST: Fast
/// To retain our fast feedback loop, tests need to execute very fast.
///
/// This is the test suite for the migration of our legacy data into
/// the new system.
/// </summary>
public class FirstTestsFast
{
    [Test]
    public void InactiveSocks_AreNotImported()
    {
        var migrator = new Migrator(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data"));
        var result = migrator.Migrate();
        Assert.That(result, Is.All.Matches<Product>(x => x.Name.Value != "Vue.js Reactive Comfort"));
    }

    [Test]
    public void SocksSuffix_IsRemovedFromTheName()
    {
        var migrator = new Migrator(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data"));
        var result = migrator.Migrate();
        Assert.That(result, Has.None.Matches<Product>(x => x.Name.Value.EndsWith(" Socks")));
    }

    [Test]
    public void Socks_AreGroupedByName()
    {
        // TODO: We'll have to add the "Sizes" to our Socks in the new system at some point
        var migrator = new Migrator(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data"));
        var result = migrator.Migrate();
        Assert.That(result.Select(x => x.Name.Value), Is.Unique);
    }

    [Test]
    public void SocksStock_IsAggregatedForAllSockSizes()
    {
        var migrator = new Migrator(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data"));
        var result = migrator.Migrate();

        var cssGrid = result.Single(x => x.Name.Value == "CSS Grid Layout");
        Assert.That(cssGrid.Stock.Value, Is.EqualTo(1141));
    }

    [Test]
    public void Category_IsHardCodedDummyValue()
    {
        // For now...
        var migrator = new Migrator(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data"));
        var result = migrator.Migrate();
        Assert.That(result, Is.All.Matches<Product>(x => x.Category.Value == "???"));
    }

    /// <summary>
    /// ATTN: While this test is running correctly on the CI, we've had reports that
    /// the test fails on some developer machines!?
    /// </summary>
    [Test]
    public void Price_IsImportedCorrectly()
    {
        var migrator = new Migrator(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data"));
        var result = migrator.Migrate();

        var reactHooks = result.Single(x => x.Name.Value == "React Hooks Premium");
        Assert.That(reactHooks.Price.Value, Is.EqualTo(16.66m));
    }
}
