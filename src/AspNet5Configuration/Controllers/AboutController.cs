using System.Collections.Generic;
using AspNet5Configuration.Configurations;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;

namespace AspNet5Configuration.Controllers
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
            return new string[] { _optionsApplicationConfiguration.Options.AboutMessage };
        }



    }
}
