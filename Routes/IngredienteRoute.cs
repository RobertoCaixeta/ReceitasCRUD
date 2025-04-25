using Microsoft.EntityFrameworkCore;
using ReceitasCRUD.Data;
using ReceitasCRUD.Models;

namespace ReceitasCRUD.Routes;

public static class IngredienteRoute
{
    public static void IngredienteRoutes(this WebApplication app)
    {
        var route = app.MapGroup("ingrediente");

        route.MapPost("", async (IngredienteRequest req, Context context) =>
        {
            var ingrediente = new Ingrediente(req.Nome, req.Peso);
            await context.AddAsync(ingrediente);
            await context.SaveChangesAsync();
            return Results.Created($"/ingrediente/{ingrediente.Id}", ingrediente.Id);
        });

        route.MapGet("", async (Context context) =>
        {
            var ingredientes = await context.Ingredientes.ToListAsync();
            return Results.Ok(ingredientes);
        });

        route.MapGet("{id:int}", async (int id,Context context) =>
        {
            var ingrediente = await context.Ingredientes.FirstOrDefaultAsync(i => i.Id == id);
            
            if (ingrediente is null)
                return Results.NotFound("Ingrediente não encontrado.");
            
            return Results.Ok(ingrediente);
        });
        
        route.MapPut("{id:int}", async (int id, IngredienteRequest req, Context context) =>
        {
            var ingrediente = await context.Ingredientes.FindAsync(id);
            
            if(ingrediente == null)
                return Results.NotFound();
            
            ingrediente.MudarNome(req.Nome);
            ingrediente.MudarPeso(req.Peso);
            await context.SaveChangesAsync();
            return Results.Ok();
            
        });

        route.MapDelete("{id:int}", async (int id, Context context) =>
        {
            var ingrediente = await context.Ingredientes.FindAsync(id);
            if(ingrediente == null)
                return Results.NotFound();
            
            context.Ingredientes.Remove(ingrediente);
            await context.SaveChangesAsync();
            return Results.Ok();
        });
    }
}