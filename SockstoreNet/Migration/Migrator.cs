using System.Text;
using ClosedXML.Excel;
using Vocabulary;

namespace Migration
{
    /// <summary>
    /// Once our new application is ready, we will export everything from
    /// the legacy system and then reimport it all into the shiny new one
    /// </summary>
    public class Migrator
    {
        private readonly string _dataDirectory;
        private readonly ICollection<StockItem> _stock = new List<StockItem>();

        public Migrator(string dataDirectory)
        {
            _dataDirectory = dataDirectory;
        }

        public IEnumerable<Product> Migrate()
        {
            string catalogPath = Path.Combine(_dataDirectory, "socks_catalog.csv");
            if (!File.Exists(catalogPath))
                throw new FileNotFoundException($"Catalog file not found: {catalogPath}");

            ReadStock();
            var combinedProducts = ReadAndCombineCatalogData(catalogPath);

            var products = combinedProducts
                .Where(x => x.Active)
                .GroupBy(x => x.Name)
                .Select((group, index) =>
                {
                    // TODO: Our new system does not yet support the different Sizes (XS->XXL)
                    //       We'll have to update our migration once we have implemented this!
                    var prod = group.First();
                    return new Product(
                        new ProductId(index),
                        new Name(prod.Name.Replace(" Socks", "")),
                        new Category("???"), // TODO: we don't have categories in the legacy system
                        new Price(prod.Price, 0),
                        new Stock(group.Sum(x => x.Stock))
                    );
                })
                .ToArray();

            return products;
        }

        private List<LegacyCombinedProduct> ReadAndCombineCatalogData(string catalogPath)
        {
            var products = new List<LegacyCombinedProduct>();

            using var reader = new StreamReader(catalogPath);
            while (reader.ReadLine() is { } line)
            {
                string[] columns = ParseCsvLine(line);
                Guid productId = Guid.Parse(columns[0]);

                var product = new LegacyCombinedProduct
                {
                    Id = productId,
                    Name = columns[1],
                    Price = decimal.Parse(columns[2]),
                    Color = columns[3],
                    Size = Enum.Parse<Size>(columns[4]),
                    Active = bool.Parse(columns[5]),
                    CreatedOn = DateTime.Parse(columns[6]),
                    UpdatedOn = DateTime.Parse(columns[7]),
                    Material = columns[8],
                    Description = columns[9].Trim('"'),
                    Brand = columns[10],
                    Stock = GetStock(productId),
                };

                products.Add(product);
            }

            return products;
        }

        private record StockItem(Guid ProductId, int Stock);

        private void ReadStock()
        {
            string stockPath = Path.Combine(_dataDirectory, "stock_inventory.xlsx");

            if (!File.Exists(stockPath))
                throw new FileNotFoundException($"Stock file not found: {stockPath}");

            using var workbook = new XLWorkbook(stockPath);
            var worksheet = workbook.Worksheet(1);
            var rowCount = worksheet.LastRowUsed()?.RowNumber();

            for (int row = 2; row <= rowCount; row++)
            {
                var productIdCell = worksheet.Cell(row, 1).GetValue<string>();
                var stockQuantity = worksheet.Cell(row, 2).GetValue<int>();

                if (Guid.TryParse(productIdCell, out var productId))
                {
                    _stock.Add(new StockItem(productId, stockQuantity));
                }
            }
        }

        private int GetStock(Guid productIdToFind)
        {
            return _stock.FirstOrDefault(x => x.ProductId == productIdToFind)?.Stock ?? 0;

            // ATTN: This code would read the Excel every time we need a Stock
            //       But that just took way too long for a single test to run.
            //string stockPath = Path.Combine(_dataDirectory, "stock_inventory.xlsx");

            //if (!File.Exists(stockPath))
            //    throw new FileNotFoundException($"Stock file not found: {stockPath}");

            //using var workbook = new XLWorkbook(stockPath);
            //var worksheet = workbook.Worksheet(1);
            //var rowCount = worksheet.LastRowUsed()?.RowNumber();

            //for (int row = 2; row <= rowCount; row++)
            //{
            //    var productIdCell = worksheet.Cell(row, 1).GetValue<string>();
            //    var stockQuantityCell = worksheet.Cell(row, 2).GetValue<int>();

            //    if (Guid.TryParse(productIdCell, out var productId))
            //    {
            //        if (productId == productIdToFind)
            //        {
            //            return stockQuantityCell;
            //        }
            //    }
            //}

            //return 0;
        }

        private static string[] ParseCsvLine(string line)
        {
            var result = new List<string>();
            var inQuotes = false;
            var currentField = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(c);
                }
            }

            result.Add(currentField.ToString());
            return result.ToArray();
        }
    }

    public class LegacyCombinedProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Color { get; set; } = "";
        public Size Size { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Material { get; set; } = "";
        public string Description { get; set; } = "";
        public string Brand { get; set; } = "";
        public int Stock { get; set; }
    }

    public enum Size
    {
        XSS,
        XS,
        S,
        M,
        L,
        XL,
        XLL
    }
}
