using SistemaPedidos.Models;
using SistemaPedidos.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaPedidos.Services;

public class FuncionarioService
{
    private Funcionario? _funcionarioLogado = null;

    public bool Autenticar(string nome, string senha)
    {
        using var db = new AppDbContext();
        var funcionario = db.Funcionarios.FirstOrDefault(f => f.Nome == nome && f.Ativo);

        if (funcionario == null)
        {
            Console.WriteLine("✘ Funcionário não encontrado ou inativo.");
            return false;
        }

        if (funcionario.Senha != senha)
        {
            Console.WriteLine("✘ Senha incorreta.");
            return false;
        }

        _funcionarioLogado = funcionario;
        Console.WriteLine($"\n✔ Bem-vindo, {funcionario.Nome}!");
        return true;
    }

    public void Deslogar()
    {
        _funcionarioLogado = null;
        Console.WriteLine("✔ Deslogado com sucesso!");
    }

    public bool EstaLogado() => _funcionarioLogado != null;

    public Funcionario? GetFuncionarioLogado() => _funcionarioLogado;

    public void ExibirPedidosDoDia()
    {
        if (_funcionarioLogado == null)
        {
            Console.WriteLine("✘ Você precisa estar logado para acessar essa funcionalidade.");
            return;
        }

        using var db = new AppDbContext();
        var hoje = DateTime.Today;
        var pedidosHoje = db.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Itens)
            .Where(p => p.DataHora.Date == hoje)
            .OrderByDescending(p => p.DataHora)
            .ToList();

        if (pedidosHoje.Count == 0)
        {
            Console.WriteLine("\n═══════════════════════════════════");
            Console.WriteLine("   NENHUM PEDIDO PARA HOJE");
            Console.WriteLine("═══════════════════════════════════\n");
            return;
        }

        Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
        Console.WriteLine($"              PEDIDOS DO DIA - {DateTime.Now:dd/MM/yyyy}");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");

        decimal totalDia = 0;
        int totalItens = 0;

        foreach (var pedido in pedidosHoje)
        {
            Console.WriteLine($"\n┌─ Pedido #{pedido.Id}");
            Console.WriteLine($"├─ Hora: {pedido.DataHora:HH:mm}");
            Console.WriteLine($"├─ Cliente: {pedido.Cliente?.Nome ?? "N/A"}");
            Console.WriteLine($"├─ Telefone: {pedido.Cliente?.Telefone ?? "N/A"}");
            Console.WriteLine($"├─ Status: {pedido.Status}");
            
            Console.WriteLine($"├─ Itens:");
            foreach (var item in pedido.Itens)
            {
                Console.WriteLine($"│  • {item.Quantidade}x {item.NomeProduto} = {item.Subtotal:C}");
                totalItens += item.Quantidade;
            }

            Console.WriteLine($"├─ Subtotal: {pedido.Subtotal:C}");
            
            if (pedido.PercentualDesconto > 0)
                Console.WriteLine($"├─ Desconto ({pedido.CupomDesconto} -{pedido.PercentualDesconto}%): -{pedido.ValorDesconto:C}");
            
            Console.WriteLine($"└─ Total: {pedido.Total:C}");
            
            // Apenas soma no faturamento se o pedido foi entregue
            if (pedido.Status == "Entregue")
                totalDia += pedido.Total;
        }

        Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
        Console.WriteLine($"  Total de Pedidos: {pedidosHoje.Count}");
        Console.WriteLine($"  Pedidos Entregues: {pedidosHoje.Count(p => p.Status == "Entregue")}");
        Console.WriteLine($"  Total de Itens: {totalItens}");
        Console.WriteLine($"  FATURAMENTO DO DIA (apenas entregues): {totalDia:C}");
        Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
    }

    public void AtualizarStatusPedido(int pedidoId)
    {
        if (_funcionarioLogado == null)
        {
            Console.WriteLine("✘ Você precisa estar logado para acessar essa funcionalidade.");
            return;
        }

        using var db = new AppDbContext();
        var pedido = db.Pedidos.FirstOrDefault(p => p.Id == pedidoId);

        if (pedido == null)
        {
            Console.WriteLine("✘ Pedido não encontrado.");
            return;
        }

        Console.WriteLine($"\nStatus atual: {pedido.Status}");
        Console.WriteLine("Novo status:");
        Console.WriteLine("1. Pendente");
        Console.WriteLine("2. Preparando");
        Console.WriteLine("3. Pronto");
        Console.WriteLine("4. Entregue");
        Console.Write("\nEscolha: ");

        var opcao = Console.ReadLine();
        var novoStatus = opcao switch
        {
            "1" => "Pendente",
            "2" => "Preparando",
            "3" => "Pronto",
            "4" => "Entregue",
            _ => null
        };

        if (novoStatus == null)
        {
            Console.WriteLine("✘ Opção inválida.");
            return;
        }

        pedido.Status = novoStatus;
        db.SaveChanges();
        Console.WriteLine($"✔ Status atualizado para '{novoStatus}'!");
    }

    public void ExibirPedidosPorCliente(string telefone)
    {
        if (_funcionarioLogado == null)
        {
            Console.WriteLine("✘ Você precisa estar logado para acessar essa funcionalidade.");
            return;
        }

        using var db = new AppDbContext();
        var cliente = db.Clientes.FirstOrDefault(c => c.Telefone == telefone);

        if (cliente == null)
        {
            Console.WriteLine("✘ Cliente não encontrado.");
            return;
        }

        var pedidos = db.Pedidos
            .Include(p => p.Itens)
            .Where(p => p.ClienteId == cliente.Id)
            .OrderByDescending(p => p.DataHora)
            .ToList();

        if (pedidos.Count == 0)
        {
            Console.WriteLine($"\n✘ Nenhum pedido encontrado para {cliente.Nome}.");
            return;
        }

        Console.WriteLine($"\n═══════════════════════════════════════════════════════════════");
        Console.WriteLine($"              PEDIDOS DE {cliente.Nome.ToUpper()}");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine($"Telefone: {cliente.Telefone}\nEndereço: {cliente.Endereco}");

        foreach (var pedido in pedidos)
        {
            Console.WriteLine($"\n┌─ Pedido #{pedido.Id} - {pedido.DataHora:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"├─ Status: {pedido.Status}");
            
            foreach (var item in pedido.Itens)
                Console.WriteLine($"├─ {item.Quantidade}x {item.NomeProduto}");
            
            Console.WriteLine($"└─ Total: {pedido.Total:C}");
        }

        Console.WriteLine("\n═══════════════════════════════════════════════════════════════\n");
    }

    public void CriarFuncionarioPadrao()
    {
        using var db = new AppDbContext();
        
        if (db.Funcionarios.Any())
            return;

        // Cria usuário padrão
        var gerente = new Funcionario("admin", "123456", "Gerente");
        var cozinheiro = new Funcionario("cozinha", "123456", "Cozinheiro");
        
        db.Funcionarios.Add(gerente);
        db.Funcionarios.Add(cozinheiro);
        db.SaveChanges();

        Console.WriteLine("✔ Funcionários padrão criados!");
        Console.WriteLine("  - Usuário: admin | Senha: 123456 | Cargo: Gerente");
        Console.WriteLine("  - Usuário: cozinha | Senha: 123456 | Cargo: Cozinheiro");
    }
}
