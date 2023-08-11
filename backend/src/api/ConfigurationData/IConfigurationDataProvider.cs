namespace cfg.api.ConfigurationData;

public interface IConfigurationDataProvider
{
    Task<IEnumerable<ConfigValue>> GetAllAsync();
    Task<ConfigValue?> GetByKeyAsync(string key);
}