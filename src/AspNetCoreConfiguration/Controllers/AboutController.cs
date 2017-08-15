using System.Collections.Generic;
using AspNetCoreConfiguration.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreConfiguration.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private IOptions<ApplicationConfiguration> _optionsApplicationConfiguration;
        public AboutController(IOptions<ApplicationConfiguration> o)
        {
            _optionsApplicationConfiguration = o;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { _optionsApplicationConfiguration.Value.AboutMessage };
        }
    }
}
