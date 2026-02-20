namespace SistemaPedidos.Models;

public class ItemPedido
{
    public Produto Produto { get; set; }
    public int Quantidade { get; set; } 
    public decimal Subtotal => Produto.Preco * Quantidade; //ex: Produto.Preco = 10, Quantidade = 2 => Subtotal = 20


    public ItemPedido(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
    }
}
