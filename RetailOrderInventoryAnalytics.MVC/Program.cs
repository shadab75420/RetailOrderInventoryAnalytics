var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("ApiClient", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7063/api/";
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<RetailOrderInventoryAnalytics.MVC.Services.ApiService>();
builder.Services.AddScoped<RetailOrderInventoryAnalytics.MVC.Services.AuthApiService>();
builder.Services.AddScoped<RetailOrderInventoryAnalytics.MVC.Services.CategoryApiService>();
builder.Services.AddScoped<RetailOrderInventoryAnalytics.MVC.Services.SupplierApiService>();
builder.Services.AddScoped<RetailOrderInventoryAnalytics.MVC.Services.ProductApiService>();
builder.Services.AddScoped<RetailOrderInventoryAnalytics.MVC.Services.InventoryApiService>();
builder.Services.AddScoped<RetailOrderInventoryAnalytics.MVC.Services.OrderApiService>();
builder.Services.AddScoped<RetailOrderInventoryAnalytics.MVC.Services.ReportApiService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
