using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace NetCore21.Controllers
{
  [Route("api/[controller]")]
  public class ValuesController : ControllerBase
  {
    [Authorize(Policy = "ApiUser")]
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
      return "value";
    }
  }
}
