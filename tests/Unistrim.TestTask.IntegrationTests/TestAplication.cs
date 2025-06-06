using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Unistrim.TestTask.IntegrationTests;

public class TestApplication: WebApplicationFactory<Program>
{
    protected override IHostBuilder? CreateHostBuilder()
    {
        Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection", "Host=localhost;Database=MyDatabase;Username=postgres;Password=postgres");
        
        return base.CreateHostBuilder();
    }
}