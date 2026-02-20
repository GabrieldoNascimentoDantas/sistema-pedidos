using SistemaPedidos.Services;

var pedidoService = new PedidoService();
var funcionarioService = new FuncionarioService();

// Cria funcionários padrão na primeira execução
funcionarioService.CriarFuncionarioPadrao();

var executando = true;

Console.WriteLine("╔═══════════════════════════════════════╗");
Console.WriteLine("║   Tempero da Baixinha - Sistema      ║");
Console.WriteLine("╚═══════════════════════════════════════╝");

while (executando)
{
    Console.WriteLine("\n┌─ MENU PRINCIPAL");
    Console.WriteLine("├─ 1. Fazer Pedido (Cliente)");
    Console.WriteLine("├─ 2. Acesso de Funcionário");
    Console.WriteLine("└─ 0. Sair");
    Console.Write("\nEscolha: ");

    switch (Console.ReadLine())
    {
        case "1":
            MenuCliente(pedidoService);
            break;

        case "2":
            MenuFuncionario(funcionarioService, pedidoService);
            break;

        case "0":
            executando = false;
            Console.WriteLine("\n✔ Até logo!");
            break;

        default:
            Console.WriteLine("✘ Opção inválida!");
            break;
    }
}

void MenuCliente(PedidoService service)
{
    var pedidoEmAndamento = true;

    Console.WriteLine("\n╔═══════════════════════════════════════╗");
    Console.WriteLine("║        REALIZAR PEDIDO                ║");
    Console.WriteLine("╚═══════════════════════════════════════╝");

    while (pedidoEmAndamento)
    {
        Console.WriteLine("\n1. Ver cardápio");
        Console.WriteLine("2. Adicionar item");
        Console.WriteLine("3. Aplicar cupom de desconto");
        Console.WriteLine("4. Finalizar pedido");
        Console.WriteLine("0. Voltar ao menu principal");
        Console.Write("\nEscolha: ");

        switch (Console.ReadLine())
        {
            case "1":
                service.ExibirCardapio();
                break;

            case "2":
                Console.Write("ID do produto: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.Write("Quantidade: ");
                    if (int.TryParse(Console.ReadLine(), out int qtd))
                    {
                        service.AdicionarItem(id, qtd);
                    }
                    else
                        Console.WriteLine("✘ Quantidade inválida.");
                }
                else
                    Console.WriteLine("✘ ID inválido.");
                break;

            case "3":
                Console.Write("Digite o cupom: ");
                service.AplicarDesconto(Console.ReadLine()!);
                break;

            case "4":
                if (service.FinalizarPedido())
                    pedidoEmAndamento = false;
                break;

            case "0":
                pedidoEmAndamento = false;
                break;

            default:
                Console.WriteLine("✘ Opção inválida!");
                break;
        }
    }
}

void MenuFuncionario(FuncionarioService funcService, PedidoService pedService)
{
    Console.WriteLine("\n╔═══════════════════════════════════════╗");
    Console.WriteLine("║     ACESSO DE FUNCIONÁRIO            ║");
    Console.WriteLine("╚═══════════════════════════════════════╝");

    Console.Write("\nUsuário: ");
    string usuario = Console.ReadLine()!;

    Console.Write("Senha: ");
    string senha = Console.ReadLine()!;

    if (!funcService.Autenticar(usuario, senha))
    {
        Console.WriteLine("✘ Acesso negado!");
        return;
    }

    var logado = true;
    while (logado)
    {
        var funcionario = funcService.GetFuncionarioLogado();
        Console.WriteLine($"\n╔═══════════════════════════════════════╗");
        Console.WriteLine($"║  {funcionario?.Cargo?.ToUpper().PadRight(33)} ║");
        Console.WriteLine("╚═══════════════════════════════════════╝");

        Console.WriteLine("\n1. Ver pedidos do dia");
        Console.WriteLine("2. Procurar pedidos de cliente");
        Console.WriteLine("3. Atualizar status de pedido");
        Console.WriteLine("4. Ver cardápio");
        Console.WriteLine("0. Deslogar");
        Console.Write("\nEscolha: ");

        switch (Console.ReadLine())
        {
            case "1":
                funcService.ExibirPedidosDoDia();
                break;

            case "2":
                Console.Write("Digite o telefone do cliente: ");
                funcService.ExibirPedidosPorCliente(Console.ReadLine()!);
                break;

            case "3":
                Console.Write("ID do pedido: ");
                if (int.TryParse(Console.ReadLine(), out int pedidoId))
                {
                    funcService.AtualizarStatusPedido(pedidoId);
                }
                else
                    Console.WriteLine("✘ ID inválido.");
                break;

            case "4":
                pedService.ExibirCardapio();
                break;

            case "0":
                funcService.Deslogar();
                logado = false;
                break;

            default:
                Console.WriteLine("✘ Opção inválida!");
                break;
        }
    }
}