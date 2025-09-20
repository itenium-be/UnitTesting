using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace SockStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartnerController(CreateProductListingUseCase productListing) : ControllerBase
{
    private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    [HttpPost(nameof(DownloadProductListing))]
    public async Task<FileStreamResult> DownloadProductListing()
    {
        var excel = await productListing.Create();

        var excelStream = new MemoryStream();
        excel.SaveAs(excelStream);
        excelStream.Seek(0, SeekOrigin.Begin);
        return new FileStreamResult(excelStream, ExcelContentType) { FileDownloadName = "products.xlsx" };
    }
}
