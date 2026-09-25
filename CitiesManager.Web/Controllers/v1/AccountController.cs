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

        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            return user;
        }
        else
        {
            string? errors = ModelState.Values.SelectMany(e => e.Errors).Select(v => v.ErrorMessage).ToString();
            return Problem(errors); 
        }
    }
}