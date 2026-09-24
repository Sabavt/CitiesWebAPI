using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.Core.Entities;
using CitiesManager.Infrastructure.DatabaseContext;
using Asp.Versioning;

namespace CitiesManager.Web.Controllers.v2;

[ApiVersion("2.0")]
public class CitiesController : CustomControllerBase
{
    private readonly ApplicationDbContext _context;

    public CitiesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/City
    [HttpGet]
    [Produces("application/xml")]
    public async Task<ActionResult<IEnumerable<string?>>> GetCity()
    {
        return await _context.Cities.Select(c => c.CityName).ToListAsync();
    } 
}