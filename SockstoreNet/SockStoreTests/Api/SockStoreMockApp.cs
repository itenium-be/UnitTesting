using Application.Ports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SockStoreTests.Mock;

namespace SockStoreTests.Api;

public class SockStoreMockApp: WebApplicationFactory<Program> {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IProductPort>();
            services.AddScoped<IProductPort, MockProductRepository>();
        });
    }
}
