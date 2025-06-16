using Microsoft.EntityFrameworkCore;
using PredictorTP.Entidades.EF;
using PredictorTP.Repositorios;
using PredictorTP.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PredictorBddContext>();
builder.Services.AddScoped<IServicioPredictorSentimiento, ServicioPredictorSentimiento>();
builder.Services.AddScoped<IServicioPredictorLenguaje, ServicioPredictorLenguaje>();
builder.Services.AddScoped<IServicioEmail, ServicioEmail>();
builder.Services.AddScoped<IServicioUsuario, ServicioUsuario>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

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
    pattern: "{controller=Acceso}/{action=Ingresar}/{id?}");

app.Run();
