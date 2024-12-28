using BooksData;
using BooksWeb.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Generar Vistas Razor
builder.Services.AddRazorPages();

//Middleware que define el comportamiento de Autenticacion y cookies
builder.Services.AddAuthentication("BooksWeb").AddCookie("BooksWeb", options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20); //Duracion de la Autenticacion
    options.LoginPath = "/Account/LogIn"; //Redireccionamiento de Autenticacion
});

//Control de politicas de vistas solo con Autenticacion
var mvc = builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

//Control de duracion de Sesion en caso de inactividad
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

//Ejecucion de la cadena de conexion con la base de datos, requiere generar 'options' en Services.Authentication
var connectionStr = builder.Configuration.GetConnectionString("DbConnectionBooks");
builder.Services.AddDbContext<DbContextBooks>(options => options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr)));

//Ejecucion de Servicios
builder.Services.AddScoped<IBooksService, IBooksService.BooksService>();

//Ejecucion de Mapeado de las vistas
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.Use
    (async (context, next) =>
    {
        context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        await next();
    });
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Account}/{action=LogIn}/{id?}");
app.Run();