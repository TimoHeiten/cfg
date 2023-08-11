using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace cfg.api.Tests
{
    public sealed class CfgTestServer : WebApplicationFactory<Program>
    {
        private readonly SqliteConnection _connection = new("Data Source=:memory:");
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _connection.Open();
            builder.UseEnvironment("Integration");

            // _ = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.Integration.json")
            //     .Build();
        }

        protected override void Dispose(bool disposing)
        {
            _connection.Dispose();
            base.Dispose(disposing);
        }
    }
}