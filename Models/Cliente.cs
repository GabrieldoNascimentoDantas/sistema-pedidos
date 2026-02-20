namespace SistemaPedidos.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Endereco { get; set; } = null!;
    public List<Pedido> Pedidos { get; set; } = new();


    public Cliente() { }

    public Cliente(string nome, string telefone, string endereco)
    {
        Nome = nome;
        Telefone = telefone;
        Endereco = endereco;
    }
}