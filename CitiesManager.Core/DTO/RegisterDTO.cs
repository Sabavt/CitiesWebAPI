using System.ComponentModel.DataAnnotations; 
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Core.DTO;

public class RegisterDTO
{
    [Required(ErrorMessage = "Name can't be blank")]
    public string PersonName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email should be in proper format")]
    [Remote("EmailAlreadyExists", "Account", ErrorMessage = "Email already exists")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Phone number is invalid, please try again")]
    [Remote("PhoneAlreadyExists", "Account", ErrorMessage = "Phone already exists")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
   
    [Compare("Password", ErrorMessage = "Password must match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}