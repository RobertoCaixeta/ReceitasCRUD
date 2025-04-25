using Microsoft.EntityFrameworkCore;
using ReceitasCRUD.Data;
using ReceitasCRUD.Models;

namespace ReceitasCRUD.Routes;

public static class ReceitasRoute
{
    public static void ReceitasRoutes(this WebApplication app)
    {
        var route = app.MapGroup("receita");

        // POST - Criar Receita
        route.MapPost("", async (ReceitaRequest req, Context context) =>
        {
            var ingredientesDoBanco = await context.Ingredientes
                .Where(i => req.Ingredientes.Select(ir => ir.IngredienteId).Contains(i.Id))
                .ToListAsync();
            
            if (ingredientesDoBanco.Count != req.Ingredientes.Count)
            {
                return Results.BadRequest("Um ou mais ingredientes não foram encontrados.");
            }

            var ingredientesReceita = req.Ingredientes.Select(ir =>
            {
                var ingrediente = ingredientesDoBanco.First(i => i.Id == ir.IngredienteId);
                return new IngredienteReceita
                {
                    IngredienteId = ir.IngredienteId,
                    Ingrediente = ingrediente,
                    Quantidade = ir.Quantidade
                };
            }).ToList();

            var receita = new Receita
            {
                Nome = req.Nome,
                Preparo = req.Preparo,
                Ingredientes = ingredientesReceita
            };

            await context.Receitas.AddAsync(receita);
            await context.SaveChangesAsync();
            return Results.Created($"/receita/{receita.Id}", receita.Id);
        });

        // GET - Listar Receitas
        route.MapGet("", async (Context context) =>
        {
            var receitas = await context.Receitas
                .Include(r => r.Ingredientes)
                .ThenInclude(ir => ir.Ingrediente)
                .ToListAsync();

            var response = receitas.Select(r => new ReceitaResponse(
                r.Id,
                r.Nome,
                r.Preparo,
                r.Ingredientes.Select(ir => new IngredienteResponse(
                    ir.IngredienteId,
                    ir.Ingrediente.Nome,
                    ir.Quantidade
                )).ToList()
            ));

            return Results.Ok(response);
        });
        
        route.MapGet("{id:int}", async (int id, Context context) =>
        {
            var receita = await context.Receitas
                .Include(r => r.Ingredientes)
                .ThenInclude(ir => ir.Ingrediente)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receita is null)
                return Results.NotFound("Receita não encontrada.");

            var response = new ReceitaResponse(
                receita.Id,
                receita.Nome,
                receita.Preparo,
                receita.Ingredientes.Select(ir => new IngredienteResponse(
                    ir.IngredienteId,
                    ir.Ingrediente.Nome,
                    ir.Quantidade
                )).ToList()
            );

            return Results.Ok(response);
        });

        
        route.MapPut("{id:int}", async (int id, ReceitaRequest req, Context context) =>
        {
            var receitaExistente = await context.Receitas
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receitaExistente is null)
                return Results.NotFound("Receita não encontrada.");

            var ingredientesDoBanco = await context.Ingredientes
                .Where(i => req.Ingredientes.Select(ir => ir.IngredienteId).Contains(i.Id))
                .ToListAsync();

            if (ingredientesDoBanco.Count != req.Ingredientes.Count)
                return Results.BadRequest("Um ou mais ingredientes não foram encontrados.");

            // Remove ingredientes antigos
            context.IngredientesReceita.RemoveRange(receitaExistente.Ingredientes);

            // Cria nova lista de ingredientes
            var novosIngredientes = req.Ingredientes.Select(ir =>
            {
                var ingrediente = ingredientesDoBanco.First(i => i.Id == ir.IngredienteId);
                return new IngredienteReceita
                {
                    IngredienteId = ir.IngredienteId,
                    Ingrediente = ingrediente,
                    Quantidade = ir.Quantidade
                };
            }).ToList();

            receitaExistente.Nome = req.Nome;
            receitaExistente.Preparo = req.Preparo;
            receitaExistente.Ingredientes = novosIngredientes;

            await context.SaveChangesAsync();

            return Results.Ok("Receita atualizada com sucesso.");
        });
        
        route.MapDelete("{id:int}", async (int id, Context context) =>
        {
            var receita = await context.Receitas
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receita is null)
                return Results.NotFound("Receita não encontrada.");

            context.IngredientesReceita.RemoveRange(receita.Ingredientes); // remove vínculos
            context.Receitas.Remove(receita);

            await context.SaveChangesAsync();

            return Results.Ok("Receita removida com sucesso.");
        });

    }
}
