using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_empresa.Views
{
    public partial class Cadastroform : Form
    {
        public Cadastroform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cria uma instância do formulário de login
            CadastroFuncionario funcioanario = new CadastroFuncionario();

            // Abre o formulário de login
            funcioanario.Show();

            // Opcional: Fechar ou ocultar o formulário atual
            this.Hide(); // Oculta o formulário atual
                         // this.Close(); // Fecha o formulário atual (use com cuidado)
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Cria uma instância do formulário de login
            CadastrarFilial filial = new CadastrarFilial();

            // Abre o formulário de login
            filial.Show();

            // Opcional: Fechar ou ocultar o formulário atual
            this.Hide(); // Oculta o formulário atual
                         // this.Close(); // Fecha o formulário atual (use com cuidado)
        }

        private void Cadastroform_Load(object sender, EventArgs e)
        {

        }
    }
}
