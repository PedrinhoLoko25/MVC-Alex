using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

// ================== MODELS ==================
namespace ControleEstoqueMVC
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }

    public class ProdutoModel
    {
        private List<Produto> produtos = new List<Produto>();
        private int nextId = 1;

        public List<Produto> ListarProdutos() => produtos;

        public void AdicionarProduto(string nome, decimal preco, int quantidade)
        {
            produtos.Add(new Produto
            {
                Id = nextId++,
                Nome = nome,
                Preco = preco,
                Quantidade = quantidade
            });
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

// ================== CONTROLLERS ==================
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

// ================== VIEWS ==================
namespace ControleEstoqueMVC
{
    public class MainForm : Form
    {
        private ProdutoController controller;
        private ListBox listBoxProdutos;
        private TextBox txtNome, txtPreco, txtQuantidade;
        private Button btnAdicionar, btnIncrementar, btnDecrementar;

        public MainForm(ProdutoController controller)
        {
            this.controller = controller;
            this.Text = "Controle de Estoque MVC";
            this.Width = 500; this.Height = 400;

            listBoxProdutos = new ListBox() { Top = 10, Left = 10, Width = 460, Height = 200 };
            txtNome = new TextBox() { Top = 220, Left = 10, Width = 220, PlaceholderText = "Nome" };
            txtPreco = new TextBox() { Top = 250, Left = 10, Width = 100, PlaceholderText = "Preço" };
            txtQuantidade = new TextBox() { Top = 250, Left = 120, Width = 100, PlaceholderText = "Qtd" };

            btnAdicionar = new Button() { Top = 280, Left = 10, Width = 100, Text = "Adicionar" };
            btnIncrementar = new Button() { Top = 280, Left = 120, Width = 100, Text = "Adicionar Estoque" };
            btnDecrementar = new Button() { Top = 280, Left = 230, Width = 120, Text = "Retirar Estoque" };

            btnAdicionar.Click += BtnAdicionar_Click;
            btnIncrementar.Click += BtnIncrementar_Click;
            btnDecrementar.Click += BtnDecrementar_Click;

            this.Controls.Add(listBoxProdutos);
            this.Controls.Add(txtNome);
            this.Controls.Add(txtPreco);
            this.Controls.Add(txtQuantidade);
            this.Controls.Add(btnAdicionar);
            this.Controls.Add(btnIncrementar);
            this.Controls.Add(btnDecrementar);

            AtualizarLista();
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNome.Text) &&
                decimal.TryParse(txtPreco.Text, out decimal preco) &&
                int.TryParse(txtQuantidade.Text, out int qtd))
            {
                controller.CriarProduto(txtNome.Text, preco, qtd);
                txtNome.Clear(); txtPreco.Clear(); txtQuantidade.Clear();
                AtualizarLista();
            }
            else MessageBox.Show("Preencha todos os campos corretamente!");
        }

        private void BtnIncrementar_Click(object sender, EventArgs e)
        {
            if (listBoxProdutos.SelectedItem != null &&
                int.TryParse(txtQuantidade.Text, out int qtd))
            {
                string selected = listBoxProdutos.SelectedItem.ToString();
                int id = int.Parse(selected.Split(':')[0]);
                controller.AdicionarEstoque(id, qtd);
                txtQuantidade.Clear();
                AtualizarLista();
            }
            else MessageBox.Show("Selecione um produto e informe a quantidade!");
        }

        private void BtnDecrementar_Click(object sender, EventArgs e)
        {
            if (listBoxProdutos.SelectedItem != null &&
                int.TryParse(txtQuantidade.Text, out int qtd))
            {
                string selected = listBoxProdutos.SelectedItem.ToString();
                int id = int.Parse(selected.Split(':')[0]);
                controller.RetirarEstoque(id, qtd);
                txtQuantidade.Clear();
                AtualizarLista();
            }
            else MessageBox.Show("Selecione um produto e informe a quantidade!");
        }

        private void AtualizarLista()
        {
            listBoxProdutos.Items.Clear();
            foreach (var p in controller.ObterProdutos())
            {
                listBoxProdutos.Items.Add($"{p.Id}: {p.Nome} - R${p.Preco} - Qtd: {p.Quantidade}");
            }
        }
    }
}

// ================== PROGRAMA PRINCIPAL ==================
namespace ControleEstoqueMVC
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ProdutoModel model = new ProdutoModel();
            ProdutoController controller = new ProdutoController(model);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(controller));
        }
    }
}
