using Business.Services.Product;
using Business.Interfaces.OrderInterfaces;
using Business.Services.OrderServices;
using RikaWebApp.Middleware;
using Business.Services.Product.Backoffice;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();


// L�gg till tj�nster
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductServiceCategory, ProductServiceCategory>();
builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
builder.Services.AddScoped<ProductBackofficeService>();
builder.Services.AddHttpClient(); // Registrera HttpClient



builder.Services.AddHttpClient("AzureFunctionClient", client =>
{
    client.BaseAddress = new Uri("https://productprovidergraphql.azurewebsites.net/api/"); 
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<ProductService>();
builder.Services.AddSingleton<IDictionary<string, object>>(new Dictionary<string, object>());
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<JwtSlidingExpirationMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();


