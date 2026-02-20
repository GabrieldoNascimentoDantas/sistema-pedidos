using SistemaPedidos.Services;

var service = new PedidoService();
var executando = true;

Console.WriteLine("Bem-vindo à Tempero da Baixinha!");

while (executando)
{
    Console.WriteLine("\n1. Ver cardápio");
    Console.WriteLine("2. Adicionar item");
    Console.WriteLine("3. Aplicar cupom de desconto");
    Console.WriteLine("4. Finalizar pedido");
    Console.WriteLine("0. Sair");
    Console.Write("\nEscolha: ");

    switch (Console.ReadLine())
    {
        case "1":
            service.ExibirCardapio();
            break;

        case "2":
            Console.Write("ID do produto: ");
            int id = int.Parse(Console.ReadLine()!);
            Console.Write("Quantidade: ");
            int qtd = int.Parse(Console.ReadLine()!);
            service.AdicionarItem(id, qtd);
            break;

        case "3":
            Console.Write("Digite o cupom: ");
            service.AplicarDesconto(Console.ReadLine()!);
            break;

        case "4":
            service.FinalizarPedido();
            executando = false;
            break;

        case "0":
            executando = false;
            break;
    }
}