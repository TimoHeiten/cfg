namespace cfg.api.ConfigurationData;

public sealed class ConfigurationDataProvider : IConfigurationDataProvider
{
    public Task<IEnumerable<ConfigValue>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<ConfigValue>>(TempConfigStore.All);
    }

    public Task<ConfigValue?> GetByKeyAsync(string key)
    {
        var value = TempConfigStore.All.FirstOrDefault(x => x.Key == key);
        return Task.FromResult(value);
    }

    private static class TempConfigStore
    {
        public static readonly List<ConfigValue> All = new()
        {
            new("apiSecret", "abcaffeschnee"),
            new("authUrl", "https://localhost:5001"),
            new("dbConnectionString", "Server=.;Database=master;Trusted_Connection=True;")
        };
    }
}