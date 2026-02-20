namespace SistemaPedidos.Models;

public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Cargo { get; set; } = null!; // Gerente, Cozinheiro, Garcom, etc
    public bool Ativo { get; set; } = true;

    public Funcionario() { }

    public Funcionario(string nome, string senha, string cargo)
    {
        Nome = nome;
        Senha = senha;
        Cargo = cargo;
    }
}
