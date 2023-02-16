using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Scaffold;
using Scaffold.Models;
namespace Scaffold.Controllers;

public static class ProductControllerLazy
{
    public static void MapProductEndpointsLazy (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Product", async (DatabaseContext db) =>
        {
            db.ChangeTracker.LazyLoadingEnabled = false;
            return await db.Products.ToListAsync();
        })
        .WithName("GetAllProductsLazyDisable");

        routes.MapGet("/api/Product", async (DatabaseContext db) =>
        {
            db.ChangeTracker.LazyLoadingEnabled = true;
            return await db.Products.ToListAsync();
        })
        .WithName("GetAllProductsLazyEnable");

        routes.MapGet("/api/Product/{id}", async (int ProductId, DatabaseContext db) =>
        {
            return await db.Products.FindAsync(ProductId)
                is Product model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductByIdLazy");

        routes.MapPut("/api/Product/{id}", async (int ProductId, Product product, DatabaseContext db) =>
        {
            var foundModel = await db.Products.FindAsync(ProductId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(product);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateProductLazy");

        routes.MapPost("/api/Product/", async (Product product, DatabaseContext db) =>
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/Products/{product.ProductId}", product);
        })
        .WithName("CreateProductLazy");

        routes.MapDelete("/api/Product/{id}", async (int ProductId, DatabaseContext db) =>
        {
            if (await db.Products.FindAsync(ProductId) is Product product)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Results.Ok(product);
            }

            return Results.NotFound();
        })
        .WithName("DeleteProductLazy");
    }
}
