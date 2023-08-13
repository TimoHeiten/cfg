using cfg.api.Models;
using cfg.api.ConfigurationData;
using Microsoft.AspNetCore.Mvc;

namespace cfg.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MutateConfigController : ControllerBase
{
    private readonly IConfigurationDataProvider _provider;

    public MutateConfigController(IConfigurationDataProvider provider)
    {
        _provider = provider;
    }

    // create, update, delete
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ConfigurationValueModel value)
    {
        var created = await _provider.CreateAsync(value.ToValue());
        return Ok(created);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ConfigurationValueModel value)
    {
        var updated = await _provider.UpdateAsync(value.ToValue());
        return Ok(updated);
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete(string key)
    {
        await _provider.DeleteAsync(key);
        return Ok(new { message = $"Deleted {key}"});
    }
}