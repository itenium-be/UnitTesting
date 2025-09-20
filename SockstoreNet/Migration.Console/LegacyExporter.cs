using Bogus;
using Migration.Legacy;
using System.Drawing;
using System.Text;
using ClosedXML.Excel;

namespace Migration.Console;

public class LegacyExporter
{
    /// <summary>
    /// Export our data from our legacy system to CSV
    /// </summary>
    public void ExportSocks()
    {
        var faker = new Faker();
        var brands = new[] { "DevWear", "CodeComfort", "TechThreads", "GridGear", "ResponsiveFit", "StateWear", "UtilityThreads", "DataFeet", "LightThreads", "FullStackFeet", "TypeSafe", "ContainerWear", "VersionFeet", "ProgressiveWear", "QualityThreads", "FormatFeet" };
        var materials = new[] { "Cotton", "Cotton Blend", "Merino Wool", "Bamboo Fiber", "Polyester", "Polyester Blend", "Compression Material", "Wool Blend" };
        var allSizes = Enum.GetValues<SockSize>();
        var colors = new[] { "Blue", "Green", "Red", "Yellow", "Purple", "Black", "Navy Blue", "Orange", "Gray", "Teal", "Pink", "Light Blue", "White", "Steel Blue", "Blue Gray", "Forest Green", "Purple Blue", "Red Orange", "Lavender", "Magenta", "Dark Green" };

        const string filePath = "socks_catalog.csv";
        using var writer = new StreamWriter(filePath);

        foreach (var name in LegacyData.GetNames())
        {
            var availableSizes = faker.Random.Bool(0.7f)
                ? allSizes
                : GetRandomSizeSubset(faker, allSizes);

            foreach (var size in availableSizes)
            {
                string line = CreateLine(
                    name,
                    faker.Random.Guid(),
                    faker.Random.Decimal(15.00m, 35.00m).ToString("F2"),
                    faker.PickRandom(colors),
                    size,
                    faker.Random.Bool(0.85f), // 85% chance of being active
                    faker.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now.AddMonths(-3)),
                    faker.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now),
                    faker.PickRandom(materials),
                    faker.Lorem.Sentence(faker.Random.Int(8, 20)),
                    faker.PickRandom(brands)
                );

                writer.WriteLine(line);
            }
        }
    }

    private static SockSize[] GetRandomSizeSubset(Faker faker, SockSize[] allSizes)
    {
        int minSizes = faker.Random.Int(2, 4);
        int maxSizes = faker.Random.Int(minSizes, allSizes.Length);
        return faker.PickRandom(allSizes, maxSizes).ToArray();
    }

    private static string CreateLine(string name, Guid id, string price, string color, SockSize size, bool active, DateTime createdOn, DateTime updatedOn, string material, string description, string brand)
    {
        return $"{id},{name},{price},{color},{size},{active},{createdOn:yyyy-MM-ddTHH:mm:ss},{updatedOn:yyyy-MM-ddTHH:mm:ss},{material},\"{description}\",{brand}";
    }

    /// <summary>
    /// Export stock details from legacy system 2 (Excel)
    /// </summary>
    public void ExportStock()
    {
        const string stockFilePath = "stock_inventory.xlsx";
        const string catalogFilePath = "socks_catalog.csv";

        var faker = new Faker();
        var guids = new List<Guid>();

        using var reader = new StreamReader(catalogFilePath);

        while (reader.ReadLine() is { } line)
        {
            string guidString = line.Split(',').First();
            if (Guid.TryParse(guidString, out var guid))
            {
                guids.Add(guid);
            }
        }

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Stock Inventory");

            worksheet.Cell(1, 1).Value = "ProductId";
            worksheet.Cell(1, 2).Value = "StockQuantity";
            worksheet.Cell(1, 3).Value = "LastStockUpdate";
            worksheet.Cell(1, 4).Value = "MinimumThreshold";
            worksheet.Cell(1, 5).Value = "MaximumCapacity";

            var headerRange = worksheet.Range(1, 1, 1, 5);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

            // Add stock data for each product
            for (int i = 0; i < guids.Count; i++)
            {
                int row = i + 2;

                worksheet.Cell(row, 1).Value = guids[i].ToString();
                worksheet.Cell(row, 2).Value = faker.Random.Int(0, 500);
                worksheet.Cell(row, 3).Value = faker.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now);
                worksheet.Cell(row, 4).Value = faker.Random.Int(5, 25);
                worksheet.Cell(row, 5).Value = faker.Random.Int(200, 1000);
            }

            worksheet.Columns().AdjustToContents();
            worksheet.Column(3).Style.DateFormat.Format = "yyyy-mm-dd hh:mm:ss";

            // Add some conditional formatting for low stock
            var stockRange = worksheet.Range(2, 2, guids.Count + 1, 2);
            stockRange.AddConditionalFormat()
                      .WhenLessThan(10)
                      .Fill.SetBackgroundColor(XLColor.Red)
                      .Font.SetFontColor(XLColor.White);

            stockRange.AddConditionalFormat()
                      .WhenBetween(10, 25)
                      .Fill.SetBackgroundColor(XLColor.Yellow);

            workbook.SaveAs(stockFilePath);
        }
    }
}