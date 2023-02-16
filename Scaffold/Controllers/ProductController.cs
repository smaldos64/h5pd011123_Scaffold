using Microsoft.EntityFrameworkCore;
using Scaffold.Models;
using Scaffold.DTO;
using Mapster;
using Scaffold.Extensions;

namespace Scaffold.Controllers;

public static class ProductController
{
    //static IMapper? mapper;


    //static ProductController()
    //{
        
    //}
    public static void MapProductEndpoints (this IEndpointRouteBuilder routes)
    {
        //routes.MapGet("/api/Product", async (DatabaseContext db) =>
        //{
        //    //db.ChangeTracker.LazyLoadingEnabled = false;
        //    db.ChangeTracker.LazyLoadingEnabled = true;
        //    return await db.Products.ToListAsync();
        //})
        //.WithName("GetAllProducts");

        routes.MapGet("/api/Product", async (DatabaseContext db) =>
        {
            db.ChangeTracker.LazyLoadingEnabled = true; // LTPE
            // Lazyloading er enabled pr. default. Kode er kun
            // medtaget for at vise, hvordan man kan slå 
            // Lazyloading til og fra i de enkelte requests. 

            var ProductsFromRepository = await db.Products.ToListAsync();
            try
            {
                var ProductsToClient = ProductsFromRepository.Adapt<ProductDTOPlusCategory[]>().ToList();
                return ProductsToClient;
            }
            catch (Exception Error)
            {
                string Test = Error.ToString();
                return null;
            }
        })
        .WithName("GetAllProducts");

        //routes.MapGet("/api/Product/{id}", async (int ProductId, DatabaseContext db) =>
        //{
        //    db.ChangeTracker.LazyLoadingEnabled = false; // LTPE

        //    return await db.Products.FindAsync(ProductId)
        //        is Product model
        //            ? Results.Ok(model)
        //            : Results.NotFound();
        //})
        //.WithName("GetProductById");

        routes.MapGet("/api/Product/{id}", async (int ProductId, DatabaseContext db) =>
        {
            var ProductFromRepository = await db.Products.FindAsync(ProductId);
            var ProductToClient = ProductFromRepository.Adapt<ProductDTOPlusCategory>();
            return ProductToClient;
        })
        .WithName("GetProductById");

        //routes.MapPut("/api/Product/{id}", async (int ProductId, Product product, DatabaseContext db) =>
        //{
        //    var foundModel = await db.Products.FindAsync(ProductId);

        //    if (foundModel is null)
        //    {
        //        return Results.NotFound();
        //    }

        //    db.Update(product);

        //    await db.SaveChangesAsync();

        //    return Results.NoContent();
        //})
        //.WithName("UpdateProduct");

        routes.MapPut("/api/Product/{id}", async (int ProductId, ProductDTOForUpdate product, DatabaseContext db) =>
        {
            var foundModel = await db.Products.FindAsync(ProductId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            if (foundModel.CloneData<Product>(product))
            {
                //foundModel.ProductId = product.ProductID;
                // Ikke nødvendig med koden herover. Men da jeg har dummet mig
                // og har kaldt feltet ProductId i Product.cs og kaldt feltet
                // ProductID i DTO klasserne beder jeg selv om problemer !!!
                db.Update(foundModel);
            }

            //foundModel = product.Adapt<Product>();
            //db.Update(foundModel);
            // Giver instance fejl !!!

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateProduct");

        //routes.MapPost("/api/Product/", async (Product product, DatabaseContext db) =>
        //{
        //    db.Products.Add(product);
        //    await db.SaveChangesAsync();
        //    return Results.Created($"/Products/{product.ProductId}", product);
        //})
        //.WithName("CreateProduct");

        routes.MapPost("/api/Product/", async (ProductDTOForSave product, DatabaseContext db) =>
        {
            var ProductToStoreInRepository = product.Adapt<Product>();
            db.Products.Add(ProductToStoreInRepository);
            await db.SaveChangesAsync();
            return Results.Created($"/Products/{ProductToStoreInRepository.ProductId}", product);
        })
        .WithName("CreateProduct");

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
        .WithName("DeleteProduct");
    }
}
