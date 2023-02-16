using Mapster;
using Microsoft.EntityFrameworkCore;
using Scaffold;
using Scaffold.DTO;
using Scaffold.Extensions;
using Scaffold.Models;
namespace Scaffold.Controllers;

public static class CategoryController
{
    public static void MapCategoryEndpoints (this IEndpointRouteBuilder routes)
    {
        //routes.MapGet("/api/Category", async (DatabaseContext db) =>
        //{
        //    db.ChangeTracker.LazyLoadingEnabled = false; // LTPE
        //    return await db.Categories.ToListAsync();
        //})
        //.WithName("GetAllCategorys");

        routes.MapGet("/api/Category", async (DatabaseContext db) =>
        {
            var CategoriesFromRepository = await db.Categories.ToListAsync();

            var CategoriesToClient = CategoriesFromRepository.Adapt<CategoryDTO[]>().ToList();
            return CategoriesToClient;
        })
        .WithName("GetAllCategorys");

        //routes.MapGet("/api/Category/{id}", async (int CategoryID, DatabaseContext db) =>
        //{
        //    return await db.Categories.FindAsync(CategoryID)
        //        is Category model
        //            ? Results.Ok(model)
        //            : Results.NotFound();
        //})
        //.WithName("GetCategoryById");

        routes.MapGet("/api/Category/{id}", async (int CategoryID, DatabaseContext db) =>
        {
            var CategoryFromRepository = await db.Categories.FindAsync(CategoryID);
            var CategoryToClient = CategoryFromRepository.Adapt<CategoryDTO>();
            return CategoryToClient;
        })
        .WithName("GetCategoryById");

        //routes.MapPut("/api/Category/{id}", async (int CategoryID, Category category, DatabaseContext db) =>
        //{
        //    var foundModel = await db.Categories.FindAsync(CategoryID);

        //    if (foundModel is null)
        //    {
        //        return Results.NotFound();
        //    }
            
        //    db.Update(category);

        //    await db.SaveChangesAsync();

        //    return Results.NoContent();
        //})
        //.WithName("UpdateCategory");

        routes.MapPut("/api/Category/{id}", async (int CategoryID, CategoryDTOForUpdate category, DatabaseContext db) =>
        {
            var foundModel = await db.Categories.FindAsync(CategoryID);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            if (foundModel.CloneData<Category>(category))
            {
                db.Update(foundModel);
            }

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCategory");

        //routes.MapPost("/api/Category/", async (Category category, DatabaseContext db) =>
        //{
        //    db.Categories.Add(category);
        //    await db.SaveChangesAsync();
        //    return Results.Created($"/Categorys/{category.CategoryID}", category);
        //})
        //.WithName("CreateCategory");

        routes.MapPost("/api/Category/", async (CategoryDTOForSave category, DatabaseContext db) =>
        {
            var CategoryToStoreInRepository = category.Adapt<Category>();
            db.Categories.Add(CategoryToStoreInRepository);

            await db.SaveChangesAsync();
            return Results.Created($"/Categorys/{CategoryToStoreInRepository.CategoryID}", category);
        })
        .WithName("CreateCategory");

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
        .WithName("DeleteCategory");
    }
}
