using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using zefffood.Data;
using Microsoft.Extensions.ML;
using SentimentAnalysis;
//DRIVER de conexion 
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.OpenApi.Models;
using zefffood.Service;
using zefffood.Integration.jsonplaceholder;
using zefffood.Integration.currencyexchange;
//using zefffood.Integration.jsonplaceholder;
//using zefffood.Integration.currencyexchange;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services postgress
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddPredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput>()
    .FromFile("MLModel1.mlnet");
    builder.Services.AddHttpClient("ClimaService", client =>
{
    client.BaseAddress = new Uri("https://api.climate.example.com/");
});
builder.Services.AddEndpointsApiExplorer();

//Registro mi logica customizada y reuzable
builder.Services.AddScoped<IClimaService, ClimaService>();

builder.Services.AddScoped<PagoService, PagoService>();

builder.Services.AddScoped<ProductoService, ProductoService>();

builder.Services.AddScoped<JsonplaceholderAPIIntegration, JsonplaceholderAPIIntegration>();

builder.Services.AddScoped<CurrencyExchangeApiIntegration, CurrencyExchangeApiIntegration>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1500);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1",
        Description = "Descripción de la API"
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    
});

app.MapPost("/predict",
    async (PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool, MLModel1.ModelInput input) =>
        await Task.FromResult(predictionEnginePool.Predict(input)));


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
