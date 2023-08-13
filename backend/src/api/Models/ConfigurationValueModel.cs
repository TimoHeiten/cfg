using cfg.api.ConfigurationData;

namespace cfg.api.Models;

public sealed record ConfigurationValueModel(string Key, string Value)
{
    public ConfigValue ToValue()
    {
        return new(Key, Value);
    }
}