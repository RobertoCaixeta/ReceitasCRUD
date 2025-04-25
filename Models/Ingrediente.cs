namespace ReceitasCRUD.Models;

public class Ingrediente
{
    public Ingrediente(string nome, float peso)
    {
        Nome = nome;
        Peso = peso;
    }

    public Ingrediente() { }

    public int Id { get; set; } // Alterado para int, auto-incremento

    public string Nome { get; private set; } = string.Empty;
    public float Peso { get; private set; }

    // Métodos criados para evitar erros no desenvolvimento
    public void MudarNome(string nome)
    {
        Nome = nome;
    }

    public void MudarPeso(float peso)
    {
        Peso = peso;
    }
}

public record IngredienteRequest( string Nome, float Peso);

public record IngredienteResponse(
    int IngredienteId,
    string Nome,
    string Quantidade
);
