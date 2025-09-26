
using System;
using System.Windows.Forms;

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
