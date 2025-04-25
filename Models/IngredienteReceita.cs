namespace ReceitasCRUD.Models;

public class IngredienteReceita
{
    public IngredienteReceita(Receita receita, Ingrediente ingrediente, string quantidade)
    {
        Receita = receita;
        ReceitaId = receita.Id;

        Ingrediente = ingrediente;
        IngredienteId = ingrediente.Id;

        Quantidade = quantidade;
    }

    public IngredienteReceita() { }

    public int Id { get; set; } 

    public int ReceitaId { get; set; }
    public Receita Receita { get; set; } = null!;

    public int IngredienteId { get; set; }
    public Ingrediente Ingrediente { get; set; } = null!;

    public string Quantidade { get; set; } = string.Empty;
}

public record IngredienteReceitaRequest(int IngredienteId, string Quantidade);