using Microsoft.EntityFrameworkCore;
using PredictorTP.Entidades.EF;
using PredictorTP.Repositorios;
using PredictorTP.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PredictorBddContext>();
builder.Services.AddScoped<IServicioPredictorPolaridad, ServicioPredictorPolaridad>();

builder.Services.AddScoped<IServicioPredictorIdioma, ServicioPredictorIdioma>();

builder.Services.AddScoped<IServicioPredictorSentimiento, ServicioPredictorSentimiento>();

builder.Services.AddScoped<IServicioProcesarImagen, ServicioProcesarImagen>();
builder.Services.AddScoped<IRepositorioProcesarImagen, RepositorioProcesarImagen>();

builder.Services.AddScoped<IServicioEmail, ServicioEmail>();

builder.Services.AddScoped<IServicioUsuario, ServicioUsuario>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {
        context.Response.Redirect("/Acceso/Ingresar");
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Predictor}/{action=ProcesarImagen}/{id?}");

app.Run();
