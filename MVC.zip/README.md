# Sistema de Controle de Estoque MVC

## Descrição
Projeto de controle de estoque desenvolvido no padrão MVC (Model-View-Controller) em C# com Windows Forms.  
Permite cadastrar produtos, incrementar e decrementar a quantidade em estoque.

## Funcionalidades
- Adicionar novos produtos (nome, preço e quantidade)
- Incrementar quantidade de um produto
- Decrementar quantidade de um produto
- Visualizar lista de produtos com preço e quantidade

## Arquitetura
- **Model:** `ProdutoModel` e `Produto` (gerenciamento de dados)
- **Controller:** `ProdutoController` (processa ações e interage com o Model)
- **View:** `MainForm` (interface com o usuário)

## Banco de Dados
O sistema utiliza uma tabela `produto` no MySQL ou SQL Server. Script para criar:

```sql
CREATE TABLE produto (
    id_produto INT NOT NULL AUTO_INCREMENT,
    nome VARCHAR(45) NOT NULL,
    preco DECIMAL(10,2) NOT NULL,
    quantidade INT DEFAULT 0,
    PRIMARY KEY (id_produto)
);
```

## Como Rodar
1. Abra a solução `ControleEstoqueMVC.sln` no Visual Studio 2023.
2. Compile o projeto.
3. Execute. A interface permitirá gerenciar o estoque.

## PDF do Trabalho
O PDF com respostas teóricas e diagramas UML está incluído: `Trabalho_MVC.pdf`.
