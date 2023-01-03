using CursosAJAX.Dominio.IRepositories;
using CursosAJAX.Dominio.IServices;
using CursosAJAX.Dominio.Models;
using CursosAJAX.Persistencia.Repositories;
using CursosAJAX.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CursosCrudContext>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));

builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IProfesorRepository, ProfesorRepository>();
builder.Services.AddScoped<ICursoProfesorRepository, CursoProfesorRepository>();

builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<ICursoProfesorService, CursoProfesorService>();

builder.Services.AddMvc()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy =
    null;
    //JsonNamingPolicy.CamelCase;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
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
