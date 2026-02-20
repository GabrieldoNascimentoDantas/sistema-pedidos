using SistemaPedidos.Models;

namespace SistemaPedidos.Services;

public class PedidoService
{
    private List<Produto> _cardapio = new()
    {
        new Produto(1, "Frango Grelhado",    15.00m, "Marmita"),
        new Produto(2, "Figado Acebolado",     15.00m, "Marmita"),
        new Produto(3, "Macarrão C/ Carne Moída",  15.00m, "Marmita"),
        new Produto(4, "Refrigerante 2L",      12.00m, "Bebida"),
        new Produto(5, "Guaravita",         2.00m, "Bebida"),
        new Produto(6, "Pavê",        10.00m, "Sobremesa"),
    };

    private Pedido _pedidoAtual = new() { Id = 1 };

    public void ExibirCardapio()
    {
        Console.WriteLine("\n========= CARDÁPIO =========");
        foreach (var grupo in _cardapio.GroupBy(p => p.Categoria))
        {
            Console.WriteLine($"\n  {grupo.Key.ToUpper()}");
            foreach (var produto in grupo)
                Console.WriteLine($"    {produto}");
        }
        Console.WriteLine("============================\n");
    }

    public void AdicionarItem(int produtoId, int quantidade)
    {
        var produto = _cardapio.FirstOrDefault(p => p.Id == produtoId);
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        var itemExistente = _pedidoAtual.Itens.FirstOrDefault(i => i.Produto.Id == produtoId);
        if (itemExistente != null)
            itemExistente.Quantidade += quantidade;
        else
            _pedidoAtual.Itens.Add(new ItemPedido(produto, quantidade));

        Console.WriteLine($"✔ {quantidade}x {produto.Nome} adicionado(s)!");
    }

    public void AplicarDesconto(string cupom)
    {
        if (_pedidoAtual.AplicarCupom(cupom))
            Console.WriteLine($"✔ Cupom '{cupom.ToUpper()}' aplicado com sucesso!");
        else
            Console.WriteLine("✘ Cupom inválido.");
    }

    public void ColetarDadosCliente()
    {
        Console.WriteLine("\n===== DADOS DO CLIENTE =====");

        string nome = ColetarNome();
        string telefone = ColetarTelefone();
        string endereco = ColetarEndereco();

        _pedidoAtual.Cliente = new Cliente(nome, endereco, telefone);
        Console.WriteLine("✔ Dados salvos!");
}

    private string ColetarNome()
    {
        while (true)
        {
            Console.Write("Nome (mínimo 5 caracteres): ");
            string nome = Console.ReadLine()!.Trim();
            if (nome.Length >= 5)
                return nome;
            Console.WriteLine("✘ Nome muito curto, tente novamente.");
        }
    }

    private string ColetarTelefone()
    {
        while (true)
        {
            Console.Write("Telefone (DDD + 9 dígitos, ex: 11987654321): ");
            string telefone = Console.ReadLine()!.Trim();
            if (telefone.Length == 11 && telefone.All(char.IsDigit))
                return telefone;
            Console.WriteLine("✘ Telefone inválido, tente novamente.");
        }
}

    private string ColetarEndereco()
    {
        while (true)
        {
            Console.WriteLine("Endereço (Rua, Número, Bairro, Apartamento):");
            Console.Write("> ");
            string endereco = Console.ReadLine()!.Trim();
            if (endereco.Length >= 10)
                return endereco;
            Console.WriteLine("✘ Endereço muito curto, tente novamente.");
        }
}

            public bool FinalizarPedido()
    {
        if (_pedidoAtual.Itens.Count == 0)
        {
            Console.WriteLine("✘ Adicione pelo menos um item antes de finalizar!");
            return false;
        }

        if (_pedidoAtual.Cliente == null)
        {
            Console.WriteLine("⚠ Informe os dados do cliente antes de finalizar!\n");
            ColetarDadosCliente();
        }

        _pedidoAtual.ExibirResumo();
        return true;
    }
}