
using System.Collections.Generic;

namespace ControleEstoqueMVC
{
    public class ProdutoController
    {
        private ProdutoModel model;
        public ProdutoController(ProdutoModel model) => this.model = model;

        public List<Produto> ObterProdutos() => model.ListarProdutos();
        public void CriarProduto(string nome, decimal preco, int quantidade) => model.AdicionarProduto(nome, preco, quantidade);
        public void AdicionarEstoque(int id, int quantidade) => model.IncrementarQuantidade(id, quantidade);
        public void RetirarEstoque(int id, int quantidade) => model.DecrementarQuantidade(id, quantidade);
    }
}
