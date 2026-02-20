# 🍽️ Sistema de Pedidos - Tempero da Baixinha

## 📋 Implementações Realizadas

### 1. **Campo de Data e Hora nos Pedidos**
- Adicionado campo `DataHora` na classe `Pedido.cs` com timestamp automático
- Adicionado campo `Status` para rastrear o estado do pedido (Pendente, Preparando, Pronto, Entregue)
- A data/hora é exibida automaticamente no resumo do pedido

### 2. **Sistema de Autenticação de Funcionários**
- Criado modelo `Funcionario.cs` com campos: Nome, Senha, Cargo e Status Ativo
- Implementado `FuncionarioService.cs` com autenticação básica
- Na primeira execução, são criados dois usuários padrão:
  - **Usuário**: `admin` | **Senha**: `123456` | **Cargo**: Gerente
  - **Usuário**: `cozinha` | **Senha**: `123456` | **Cargo**: Cozinheiro

### 3. **Tela de Pedidos do Dia (Apenas Funcionários)**
- Visualizar todos os pedidos do dia com informações completas:
  - ID do pedido
  - Horário do pedido
  - Nome e contato do cliente
  - Status atual
  - Itens do pedido
  - Valor total
- **Exibe estatísticas do dia**:
  - Total de pedidos
  - Total de itens vendidos
  - **FATURAMENTO TOTAL DO DIA**

### 4. **Gestão de Pedidos**
- Procurar pedidos por cliente (usando telefone)
- Atualizar status dos pedidos
- Visualizar histórico de pedidos de cada cliente

## 🔐 Segurança

- Apenas funcionários autenticados podem acessar:
  - Visualização de pedidos do dia
  - Faturamento
  - Atualização de status
  - Dados de clientes

- Menu de cliente separado do menu de funcionário
- Sessão de funcionário com login/logout

## 🚀 Como Usar

### Iniciar o Sistema
```bash
cd /home/gabriel/Documents/SistemaPedidos
dotnet run
```

### Menu Principal
1. **Fazer Pedido (Cliente)** - Permite aos clientes fazer pedidos
2. **Acesso de Funcionário** - Requer autenticação
3. **Sair** - Encerrar o programa

### Como Funcionário

1. Selecione "Acesso de Funcionário" no menu principal
2. Faça login com as credenciais:
   - **admin/123456** (para gerente) ou
   - **cozinha/123456** (para cozinheiro)

#### Menu de Funcionário
- **Ver pedidos do dia** - Visualiza todos os pedidos com faturamento
- **Procurar pedidos de cliente** - Busca por telefone do cliente
- **Atualizar status de pedido** - Muda o status de um pedido
- **Ver cardápio** - Consulta os produtos disponíveis
- **Deslogar** - Finaliza a sessão

## 📊 Exemplos de Uso

### Visualizar Pedidos do Dia
```
1. Ver pedidos do dia
```
Mostrará:
- Todos os pedidos recebidos hoje
- Horário de cada pedido
- Cliente e contato
- Itens e valores
- **Faturamento total do dia**

### Atualizar Status do Pedido
```
3. Atualizar status de pedido
ID do pedido: 5
Status atual: Pendente
Novo status:
1. Pendente
2. Preparando
3. Pronto
4. Entregue
```

## 💾 Banco de Dados

O sistema usa SQLite (`marmitas.db`) com as seguintes tabelas:
- **Clientes** - Dados dos clientes
- **Produtos** - Catálogo de produtos
- **Pedidos** - Pedidos com DataHora e Status
- **ItensPedido** - Items de cada pedido
- **Funcionarios** - Usuários do sistema (novo)

As migrações foram aplicadas automaticamente.

## 🔄 Próximas Melhorias Sugeridas

- Criptografia de senhas dos funcionários
- Relatórios de vendas por período
- Sistema de permissões (roles mais granulares)
- Logs de ações dos funcionários
- Interface web ou desktop com WinForms/WPF
