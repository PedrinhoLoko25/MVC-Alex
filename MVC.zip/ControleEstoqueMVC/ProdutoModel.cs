
using System.Collections.Generic;
using System.Linq;

namespace ControleEstoqueMVC
{
    public class ProdutoModel
    {
        private List<Produto> produtos = new List<Produto>();
        private int nextId = 1;

        public List<Produto> ListarProdutos() => produtos;

        public void AdicionarProduto(string nome, decimal preco, int quantidade)
        {
            produtos.Add(new Produto { Id = nextId++, Nome = nome, Preco = preco, Quantidade = quantidade });
        }

        public void IncrementarQuantidade(int id, int quantidade)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null) produto.Quantidade += quantidade;
        }

        public void DecrementarQuantidade(int id, int quantidade)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null && produto.Quantidade >= quantidade) produto.Quantidade -= quantidade;
        }
    }
}
