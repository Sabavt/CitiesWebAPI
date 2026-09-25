using Asp.Versioning;
using CitiesManager.Core.DTO;
using CitiesManager.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Web.Controllers.v1;

[AllowAnonymous]
[ApiVersion("1.0")]
public class AccountController : CustomControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationUser>> PostRegister(RegisterDTO userToRegister)
    {
        var user = new ApplicationUser()
        {
            Email = userToRegister.Email,
            UserName = userToRegister.Email,
            PersonName = userToRegister.PersonName,
            PhoneNumber = userToRegister.Phone
        };

        var result = await _userManager.CreateAsync(user, userToRegister.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, true);
            return Ok(user);
        }
        else
        {
            string? errors = result.Errors.Select(v => v.Description).ToString();
            return Problem(errors); 
        }
    }

    public async Task<IActionResult> EmailAlreadyExists(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);

        if(result == null)
        {
            return new JsonResult("true");
        }
        else
        {
            return new JsonResult("false");  
        }
    }
}