namespace SistemaPedidos.Models;

public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
    public string NomeProduto { get; set; } = null!;
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public decimal Subtotal => Preco * Quantidade;

    public ItemPedido() { }

    public ItemPedido(Produto produto, int quantidade)
    {
        NomeProduto = produto.Nome;
        Preco = produto.Preco;
        Quantidade = quantidade;
    }
}