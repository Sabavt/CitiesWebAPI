using System.ComponentModel.DataAnnotations;

namespace CitiesManager.Web.Models
{
    public class City
    {
        [Key]
        public Guid CityID { get; set; }
        [Required]
        public string? CityName { get; set;}
    }
}
