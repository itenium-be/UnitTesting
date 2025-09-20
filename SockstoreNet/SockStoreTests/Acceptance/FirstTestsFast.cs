using Migration;

namespace SockStoreTests.Acceptance;

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

        bool inactiveVueSocksExist = result.Any(x => x.Name.Value == "Vue.js Reactive Comfort");
        Assert.That(inactiveVueSocksExist, Is.False);
    }

    [Test]
    public void SocksSuffix_IsRemovedFromTheName()
    {
        var migrator = new Migrator(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data"));
        var result = migrator.Migrate();

        var namesWithSocks = result.Where(x => x.Name.Value.EndsWith(" Socks")).ToArray();
        Assert.That(namesWithSocks.Length, Is.EqualTo(0));
    }

    [Test]
    public void Socks_AreGroupedByName()
    {
        // TODO: We'll have to add the "Sizes" to our Socks in the new system at some point
        var migrator = new Migrator(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data"));
        var result = migrator.Migrate();

        var duplicatedNames = result
            .GroupBy(x => x.Name.Value)
            .Where(x => x.Count() > 1)
            .ToArray();
        Assert.That(duplicatedNames.Length, Is.EqualTo(0));
    }

    [Test]
    public void SocksStock_IsAggregatedForAllSockSizes()
    {
        var migrator = new Migrator(Path.Combine(TestContext.CurrentContext.TestDirectory, "Data"));
        var result = migrator.Migrate();

        var cssGrid = result.Single(x => x.Name.Value == "CSS Grid Layout");
        Assert.That(cssGrid.Stock.Value, Is.EqualTo(1141));
    }
}
