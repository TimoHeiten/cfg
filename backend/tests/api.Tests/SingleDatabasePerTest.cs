using System;
using System.Net.Http;

namespace cfg.api.Tests;

public class SingleDatabasePerTest : IDisposable
{
    protected CfgTestServer TestServer { get; }
    // protected readonly ScenarioDbContext DbContext;
    protected HttpClient Client { get; }
    
    protected SingleDatabasePerTest()
    {
        TestServer = new ();
        Client = TestServer.CreateClient();

        // DbContext = TestServer.Services.GetRequiredService<ScenarioDbContext>();
        // DbContext.Database.EnsureCreated();
    }

    public void Dispose()
        => TestServer.Dispose();
}