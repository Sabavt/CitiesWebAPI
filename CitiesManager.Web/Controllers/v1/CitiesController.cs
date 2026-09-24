using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.Web.Models;
using CitiesManager.Web.SqlDbContext;
using Asp.Versioning;

namespace CitiesManager.Web.Controllers.v1;

[ApiVersion("1.0")]
public class CitiesController : CustomControllerBase
{
    private readonly ApplicationDbContext _context;

    public CitiesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/City
    [HttpGet] 
    public async Task<ActionResult<IEnumerable<City>>> GetCity()
    {
        return await _context.Cities.ToListAsync();
    }

    // GET: api/City/5
    [HttpGet("{cityid}")] 
    public async Task<ActionResult<City>> GetCity(System.Guid cityid)
    {
        var city = await _context.Cities.FindAsync(cityid);

        if (city == null)
        {
            return NotFound();
        }

        return city;
    }

    // PUT: api/City/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Updates specific row of countries
    /// </summary>
    /// <param name="cityid">cityid to search</param>
    /// <param name="city">city model to search</param>
    /// <returns>Appropriate Error message or No Content if Successful</returns>
    [HttpPut("{cityid}")] 
    public async Task<IActionResult> PutCity(System.Guid? cityid, [Bind(nameof(city.CityName), nameof(city.CityID))]City city)
    {
        if (cityid != city.CityID)
        {
            return Problem(detail: "City id doesnt exists", statusCode: 400, title:"City Update");
        }

        var matchingCity = await _context.Cities.FindAsync(cityid);

        if(matchingCity == null)
        {
            return NotFound();
        }
         
        matchingCity.CityName = city.CityName;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CityExists(cityid))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/City
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<City>> PostCity([Bind(nameof(city.CityID), nameof(city.CityName))]City city)
    {
        _context.Cities.Add(city);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCity", new { cityid = city.CityID }, city);
    }

    // DELETE: api/City/5
    [HttpDelete("{cityid}")]
    public async Task<IActionResult> DeleteCity(System.Guid? cityid)
    {
        var city = await _context.Cities.FindAsync(cityid);
        if (city == null)
        {
            return NotFound();
        }

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CityExists(System.Guid? cityid)
    {
        return _context.Cities.Any(e => e.CityID == cityid);
    }
}
