using Microsoft.EntityFrameworkCore;
using ReceitasCRUD.Models;

namespace ReceitasCRUD.Data;

public class Context : DbContext
{
    public DbSet<Receita> Receitas { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }
    public DbSet<IngredienteReceita> IngredientesReceita { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Receitas.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
    
}