using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace AspNet5Configuration.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Demo showing the about for this controller" };
        }

   

      

    }
}
