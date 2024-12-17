using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineStore.Application.Services;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interfaces;
using OnlineStore.Infrastructure.Persistence;
using OnlineStore.Infrastructure.Repositories;

using System;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers(); // activate Controllers
builder.Services.AddMemoryCache(); // to use InMemory DB
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("OnlineStoreDb")); // Installing Microsoft.EntityFrameworkCore.InMemory from Nuget

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();
//---------------------------------------------------------------
//Swagger 
/* Installing Swashbuckle.AspNetCore package from Nuget */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//----------------------------------------------------------------
//Angular UI
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
//--------------------------------------------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Redirect root URL to Swagger UI
    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
}

// Define endpoints
//app.MapGet("/products", async (IProductService service) => Results.Ok(await service.GetAllAsync()));
// Define endpoints
app.MapGet("/products", async (IProductService service, IMemoryCache cache) =>
{
    const string cacheKey = "all_products";
    if (!cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
    {
        var product = await service.GetAllProductAsync();

        // Set cache options and add to cache
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // Cache expires after 5 minutes
        cache.Set(cacheKey, product, cacheOptions);
    }
    return Results.Ok(products);
});


app.MapGet("/products/{id:int}", async (IProductService service, int id) =>
{
    var product = await service.GetProductByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});
//app.MapPost("/products", async (IProductService service, Product product) =>
//{
//    await service.AddAsync(product);
//    return Results.Created($"/products/{product.Id}", product);
//});
app.MapPut("/products/{id:int}", async (IProductService service, int id, Product updatedProduct) =>
{
    var product = await service.GetProductByIdAsync(id);
    if (product is null) return Results.NotFound();
    updatedProduct.Id = id;
    await service.UpdateAsync(updatedProduct);
    return Results.NoContent();
});
app.MapDelete("/products/{id:int}", async (IProductService service, int id) =>
{
    await service.DeleteAsync(id);
    return Results.NoContent();
});
/*Using Specifications Pattern*/
app.MapGet("/products/price-range", async (IProductService service, decimal minPrice, decimal maxPrice, bool isDescending) =>
{
    var products = await service.GetByPriceRangeAsync(minPrice, maxPrice, isDescending);
    return Results.Ok(products);
});

app.MapPost("/add-product", ([FromBody] Product product, [FromServices] IProductService productService) =>
{
    //Console.WriteLine($"Received Product: {product.Name}, {product.Price}");
    productService.AddAsync(product);
    return Results.Ok();
});


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    AppDbContext.SeedDatabase(context);
}
//---------------
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAll"); // Add this line here

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
//-----------------------------
app.Run();
