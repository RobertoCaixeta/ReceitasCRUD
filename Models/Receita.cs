namespace ReceitasCRUD.Models;

public class Receita
{
    public Receita(List<IngredienteReceita> ingredientes, string preparo, string nome)
    {
        Ingredientes = ingredientes;
        Preparo = preparo;
        Nome = nome;
        
    }

    public Receita() { }

    public int Id { get; set; }

    public List<IngredienteReceita> Ingredientes { get; set; } = new();
    public string Preparo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}

public record ReceitaRequest(string Nome, string Preparo, List<IngredienteReceitaRequest> Ingredientes);


public record ReceitaResponse(
    int Id,
    string Nome,
    string Preparo,
    List<IngredienteResponse> Ingredientes
);

