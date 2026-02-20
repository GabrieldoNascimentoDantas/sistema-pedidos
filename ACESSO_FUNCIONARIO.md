# 🔑 Guia Rápido - Acesso de Funcionário

## Credenciais Padrão

| Usuário | Senha | Cargo |
|---------|-------|-------|
| `admin` | `123456` | Gerente |
| `cozinha` | `123456` | Cozinheiro |

## 📍 Onde Encontrar a Tela de Pedidos

1. Execute o programa: `dotnet run`
2. Menu principal → Opção **2. Acesso de Funcionário**
3. Faça login com as credenciais acima
4. Menu de funcionário → Opção **1. Ver pedidos do dia**

## 🎯 O que a Tela Exibe

```
═══════════════════════════════════════════════════════════════
              PEDIDOS DO DIA - 20/02/2026
═══════════════════════════════════════════════════════════════

┌─ Pedido #1
├─ Hora: 14:30
├─ Cliente: João Silva
├─ Telefone: 11987654321
├─ Status: Pendente
├─ Itens:
│  • 2x Frango Grelhado = R$ 30,00
│  • 1x Refrigerante 2L = R$ 12,00
├─ Subtotal: R$ 42,00
└─ Total: R$ 42,00

═══════════════════════════════════════════════════════════════
  Total de Pedidos: 1
  Total de Itens: 3
  FATURAMENTO DO DIA: R$ 42,00
═══════════════════════════════════════════════════════════════
```

## ⚙️ Funcionalidades Disponíveis

### 1️⃣ Ver Pedidos do Dia
- Mostra todos os pedidos recebidos hoje
- Exibe horário, cliente, itens e valor
- Calcula o **faturamento total do dia**
- Mostra o status de cada pedido

### 2️⃣ Procurar Pedidos de Cliente
- Digite o telefone do cliente
- Mostra todo o histórico de pedidos dele
- Útil para acompanhamento

### 3️⃣ Atualizar Status de Pedido
Mude o status do pedido para:
- Pendente
- Preparando
- Pronto
- Entregue

### 4️⃣ Ver Cardápio
- Consulta os produtos disponíveis
- Preços e categorias

## 🔒 Segurança

✅ **Apenas funcionários podem:**
- Ver pedidos do dia
- Visualizar faturamento
- Atualizar status
- Acessar dados de clientes

✅ **Clientes podem:**
- Fazer pedidos
- Aplicar cupons
- Ver apenas seu próprio resumo

## 🆘 Dúvidas?

- **Perdeu a senha?** Modifique no banco de dados ou crie novo usuário
- **Quer adicionar funcionário?** Abra o banco com SQLite e insira em Funcionarios
- **Quer mudar as senhas padrão?** Atualize diretamente no banco de dados
