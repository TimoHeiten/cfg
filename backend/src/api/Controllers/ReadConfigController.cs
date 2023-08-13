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

        /// <summary>
        /// Get a specific configuration value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get all currently stored configuration values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var values = await _dataProvider.GetAllAsync();
            return Ok(values);
        } 
    }
}