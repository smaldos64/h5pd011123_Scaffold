using Microsoft.EntityFrameworkCore;
using Scaffold;
using Scaffold.Models;
namespace Scaffold.Controllers;

public static class CategoryCOnroller
{
    public static void MapCategoryEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Category", async (DatabaseContext db) =>
        {
            return await db.Categories.ToListAsync();
        })
        .WithName("GetAllCategorys")
        .Produces<List<Category>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Category/{id}", async (int CategoryID, DatabaseContext db) =>
        {
            return await db.Categories.FindAsync(CategoryID)
                is Category model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetCategoryById")
        .Produces<Category>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Category/{id}", async (int CategoryID, Category category, DatabaseContext db) =>
        {
            var foundModel = await db.Categories.FindAsync(CategoryID);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(category);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCategory")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Category/", async (Category category, DatabaseContext db) =>
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return Results.Created($"/Categorys/{category.CategoryID}", category);
        })
        .WithName("CreateCategory")
        .Produces<Category>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Category/{id}", async (int CategoryID, DatabaseContext db) =>
        {
            if (await db.Categories.FindAsync(CategoryID) is Category category)
            {
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
                return Results.Ok(category);
            }

            return Results.NotFound();
        })
        .WithName("DeleteCategory")
        .Produces<Category>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
