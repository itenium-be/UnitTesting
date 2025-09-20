using Application.Ports;
using ClosedXML.Excel;

namespace Application.UseCases;

public class CreateProductListingUseCase(IProductPort port)
{
    public async Task<IXLWorkbook> Create()
    {
        var products = await port.FindAll(CancellationToken.None);

        var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Products");

        SetHeaderCell(ws.Cell("A1"), "Id");
        SetHeaderCell(ws.Cell("B1"), "Name");
        SetHeaderCell(ws.Cell("C1"), "Category");
        SetHeaderCell(ws.Cell("D1"), "Price");
        SetHeaderCell(ws.Cell("E1"), "Stock");

        int rowIndex = 2;
        foreach (var product in products)
        {
            ws.Cell(rowIndex, "A").Value = product.Id.Value;
            ws.Cell(rowIndex, "B").Value = product.Name.Value;
            ws.Cell(rowIndex, "C").Value = product.Category.Value;
            ws.Cell(rowIndex, "D").Value = product.Price.Value;
            ws.Cell(rowIndex, "E").Value = product.Stock.Value;

            rowIndex++;
        }

        ws.Range(1, 1, rowIndex, 5).SetAutoFilter();
        ws.ColumnsUsed().AdjustToContents();

        return wb;
    }

    private static void SetHeaderCell(IXLCell cell, string header)
    {
        cell.Value = header;
        cell.Style.Font.Bold = true;
        cell.Style.Fill.BackgroundColor = XLColor.LightGray;
    }
}