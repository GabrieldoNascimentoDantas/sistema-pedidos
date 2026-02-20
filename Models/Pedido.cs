namespace SistemaPedidos.Models;

public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }  
    public Cliente? Cliente { get; set; }  
    public List<ItemPedido> Itens { get; set; } = new();
    public string? CupomDesconto { get; set; }
    public decimal PercentualDesconto { get; private set; }
    public DateTime DataHora { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Pendente";

    private static readonly Dictionary<string, decimal> Cupons = new()
    {
        { "MARMITA10", 10m },
        { "MARMITA20", 20m },
        { "MARMITA50", 50m }
    };

    public decimal Subtotal => Itens.Sum(i => i.Subtotal);
    public decimal ValorDesconto => Subtotal * (PercentualDesconto / 100);
    public decimal Total => Subtotal - ValorDesconto;

    public bool AplicarCupom(string cupom)
    {
        cupom = cupom.ToUpper().Trim();
        if (Cupons.TryGetValue(cupom, out decimal desconto))
        {
            CupomDesconto = cupom;
            PercentualDesconto = desconto;
            return true;
        }
        return false;
    }

    public void ExibirResumo()
    {
        Console.WriteLine("\n===== RESUMO DO PEDIDO =====");
        Console.WriteLine($"  Pedido ID: {Id}");
        Console.WriteLine($"  Data/Hora: {DataHora:dd/MM/yyyy HH:mm}");
        if (Cliente != null)
        {
            Console.WriteLine($"  Cliente:  {Cliente.Nome}");
            Console.WriteLine($"  Endereço: {Cliente.Endereco}");
            Console.WriteLine($"  Telefone: {Cliente.Telefone}");
            Console.WriteLine();
        }

        foreach (var item in Itens)
            Console.WriteLine($"  {item.Quantidade}x {item.NomeProduto} = {item.Subtotal:C}");

        Console.WriteLine($"\n  Subtotal:  {Subtotal:C}");

        if (PercentualDesconto > 0)
            Console.WriteLine($"  Desconto ({CupomDesconto} -{PercentualDesconto}%): -{ValorDesconto:C}");

        Console.WriteLine($"  TOTAL:     {Total:C}");
        Console.WriteLine("============================\n");
    }
}
