using personapi_dotnet.Models.Entities;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Repositories;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<persona_dbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<ITelefonoRepository, TelefonoRepository>();
builder.Services.AddScoped<IEstudioRepository, EstudioRepository>();
builder.Services.AddScoped<IProfesionRepository, ProfesionRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();