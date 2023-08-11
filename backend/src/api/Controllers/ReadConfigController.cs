using cfg.api.ConfigurationData;
using Microsoft.AspNetCore.Mvc;

namespace cfg.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ReadConfigController : ControllerBase
    {
        private readonly IConfigurationDataProvider _dataProvider;

        public ReadConfigController(IConfigurationDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        [HttpGet("{key}")]
        public async Task<ActionResult> Get(string key)
        {
            var value = await _dataProvider.GetByKeyAsync(key);
            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }
    }
}