using Business.Services.Product;
using Business.Interfaces.OrderInterfaces;
using Business.Services.OrderServices;
using RikaWebApp.Middleware;
using Business.Services.Product.Backoffice;
using Business.Services.AuthServices;
using RikaWebApp.Helpers.AuthHelpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductServiceCategory, ProductServiceCategory>();
builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
builder.Services.AddScoped<ProductBackofficeService>();
//builder.Services.AddHttpClient();



builder.Services.AddHttpClient("AzureFunctionClient", client =>
{
    client.BaseAddress = new Uri("https://productprovidergraphql.azurewebsites.net/api/"); 
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<ProductService>();

builder.Services.AddSingleton<IDictionary<string, object>>(new Dictionary<string, object>());


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenManagerService>();
builder.Services.AddTransient<HttpClientAuthorizationHandler>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Eftersom vi inte kontrollerar utgivaren
            ValidateAudience = false, // Eftersom vi inte kontrollerar m�lgruppen
            ValidateLifetime = true, // S�kerst�ll att tokenen inte har g�tt ut
            ValidateIssuerSigningKey = true, // S�tt till `true` om du har den externa signeringsnyckeln
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b215a3db-7f30-4584-a2a2-de476e4de617")) // Ange din signeringsnyckel h�r
        };

        // Extra: Om du vill l�sa accessToken fr�n en cookie i st�llet f�r Authorization-header
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // H�mta JWT-token fr�n cookies
                var token = context.HttpContext.Request.Cookies["AccessToken"];

                // Logga tokenen f�r fels�kning
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("AccessToken not found in cookies.");
                }
                else
                {
                    Console.WriteLine("AccessToken found in cookies.");
                }

                // S�tt token om den finns
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7286");
})
.AddHttpMessageHandler<HttpClientAuthorizationHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseMiddleware<JwtSlidingExpirationMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();


