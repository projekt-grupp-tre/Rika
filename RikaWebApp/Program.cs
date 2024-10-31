//var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddControllersWithViews();

//var app = builder.Build();


//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");

//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();

using Microsoft.Extensions.DependencyInjection;
using Business.Services.Product;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductServiceEdvin, ProductServiceEdvin>();
builder.Services.AddHttpClient(); // Registrera HttpClient

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


