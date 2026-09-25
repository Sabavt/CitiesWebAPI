using Asp.Versioning;
using CitiesManager.Core.Identity;
using CitiesManager.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(bld =>
    {
        bld
            .WithOrigins(builder.Configuration
                .GetSection("AllowOrigins")
                .Get<string[]>()! 
            ).WithHeaders("Authorization", "origin", "accept", "content-type").AllowAnyMethod();
    }); 
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<ApplicationUser>()
    .AddRoleStore<ApplicationRole>();

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer(); // Describes endpoints

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CitiesWebApi",
        Version = "1.0"
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "CitiesWebApi",
        Version = "2.0"
    }); 
}); //Generates OpenAPI specification
builder.Services
    .AddApiVersioning(options =>
    {
        options.ApiVersionReader = new UrlSegmentApiVersionReader();

        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build(); 

app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger(); //Generates swagger.json
app.UseSwaggerUI(opt => { 
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");

    opt.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
}); //Swagger testing UI

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
