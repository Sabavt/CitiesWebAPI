using CitiesManager.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Web.SqlDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public ApplicationDbContext()
        {
            
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<City>().HasData(
                new City() {CityID = Guid.Parse("3E7A2BE9-C4B6-4D01-964D-F8B53800B1A3"), CityName = "Tbilisi" }, 
                new City() { CityID = Guid.Parse("77AB2718-9F30-4658-BB30-265DBDBC5E0E"), CityName = "Geneva" }, 
                new City() { CityID = Guid.Parse("8CFF2AF3-87FD-4586-A395-CA914E2BED55"), CityName = "New York" }
            );
        }
    } 
}
