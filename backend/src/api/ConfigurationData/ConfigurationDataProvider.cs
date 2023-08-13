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

    public Task<ConfigValue> CreateAsync(ConfigValue value)
    {
        if (TempConfigStore.All.Any(x => x.Key == value.Key))
            throw new ArgumentException($"[CREATE] Key {value.Key} already exists");

        TempConfigStore.All.Add(value);
        return Task.FromResult(value);
    }

    public Task<ConfigValue> UpdateAsync(ConfigValue value)
    {
        var index = TempConfigStore.All.FindIndex(x => x.Key == value.Key);
        if (index == -1)
            throw new ArgumentException($"[UPDATE] Key {value.Key} does not exist");
        TempConfigStore.All[index] = value;

        return Task.FromResult(value);
    }

    public Task DeleteAsync(string key)
    {
        var index = TempConfigStore.All.FindIndex(x => x.Key == key);
        if (index == -1)
            throw new ArgumentException($"[DELETE] Key {key} does not exist");

        TempConfigStore.All.RemoveAt(index);

        return Task.CompletedTask;
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