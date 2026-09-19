using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public string M()
    {
        return "H";
    }
}
