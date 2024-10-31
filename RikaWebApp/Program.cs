
using Business.Services.Product;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient("AzureFunctionClient", client =>
{
    client.BaseAddress = new Uri("https://productprovidergraphql.azurewebsites.net/api/"); 
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<ProductService>();


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
