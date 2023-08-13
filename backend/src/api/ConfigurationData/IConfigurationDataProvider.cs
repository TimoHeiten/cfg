namespace cfg.api.ConfigurationData;

public interface IConfigurationDataProvider
{
    Task<IEnumerable<ConfigValue>> GetAllAsync();
    Task<ConfigValue?> GetByKeyAsync(string key);
    
    Task<ConfigValue> CreateAsync(ConfigValue value);
    Task<ConfigValue> UpdateAsync(ConfigValue value);
    Task DeleteAsync(string key);
}