using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using Scaffold;

namespace Scaffold.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}

//public static class ProductEndpoints
//{
//	public static void MapProductEndpoints (this IEndpointRouteBuilder routes)
//    {
//        routes.MapGet("/api/Product", async (DatabaseContext db) =>
//        {
//            return await db.Products.ToListAsync();
//        })
//        .WithName("GetAllProducts")
//        .Produces<List<Product>>(StatusCodes.Status200OK);

//        routes.MapGet("/api/Product/{id}", async (int ProductId, DatabaseContext db) =>
//        {
//            return await db.Products.FindAsync(ProductId)
//                is Product model
//                    ? Results.Ok(model)
//                    : Results.NotFound();
//        })
//        .WithName("GetProductById")
//        .Produces<Product>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status404NotFound);

//        routes.MapPut("/api/Product/{id}", async (int ProductId, Product product, DatabaseContext db) =>
//        {
//            var foundModel = await db.Products.FindAsync(ProductId);

//            if (foundModel is null)
//            {
//                return Results.NotFound();
//            }
            
//            db.Update(product);

//            await db.SaveChangesAsync();

//            return Results.NoContent();
//        })   
//        .WithName("UpdateProduct")
//        .Produces(StatusCodes.Status404NotFound)
//        .Produces(StatusCodes.Status204NoContent);

//        routes.MapPost("/api/Product/", async (Product product, DatabaseContext db) =>
//        {
//            db.Products.Add(product);
//            await db.SaveChangesAsync();
//            return Results.Created($"/Products/{product.ProductId}", product);
//        })
//        .WithName("CreateProduct")
//        .Produces<Product>(StatusCodes.Status201Created);


//        routes.MapDelete("/api/Product/{id}", async (int ProductId, DatabaseContext db) =>
//        {
//            if (await db.Products.FindAsync(ProductId) is Product product)
//            {
//                db.Products.Remove(product);
//                await db.SaveChangesAsync();
//                return Results.Ok(product);
//            }

//            return Results.NotFound();

//        })
//        .WithName("DeleteProduct")
//        .Produces<Product>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status404NotFound);
//    }
//}}
