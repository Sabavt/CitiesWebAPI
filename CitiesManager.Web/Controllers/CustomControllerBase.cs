using Microsoft.AspNetCore.Mvc;

[Route("api/v{versionNumber:apiVersion}/[controller]")]
[ApiController]
public class CustomControllerBase : ControllerBase
{

}