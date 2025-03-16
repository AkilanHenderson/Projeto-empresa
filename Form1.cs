using Projeto_empresa.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using System.Configuration;

namespace Projeto_empresa
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString;
        public Form1()
        {
            InitializeComponent();

            // Lê a string de conexão do App.config
            _connectionString = ConfigurationManager.ConnectionStrings["Empresa"].ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Conexão com o banco de dados realizada com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}");
                }
            }
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            // Cria uma isnstância do formulário de ca
            Cadastroform cadastro = new Cadastroform();

            // Abre o formulário de login
            cadastro.Show();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
         
        private void escolhaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Verifica se o formulário de login já está aberto
            if (Application.OpenForms.OfType<Loginform>().Count() == 0)
            {
                // Cria uma nova instância do formulário de login
                Loginform login = new Loginform();
                login.MdiParent = this; // Define o formulário principal como MDI Parent
                login.Show(); // Exibe o formulário de login
                login.BringToFront(); // Traz o formulário para a frente
                login.Activate(); // Ativa o formulário para garantir que ele receba foco
            }
            else
            {
                // Se o formulário já estiver aberto, traz ele para a frente
                var loginForm = Application.OpenForms.OfType<Loginform>().First();
                loginForm.WindowState = FormWindowState.Normal; // Restaura o estado da janela (se estiver minimizada)
                loginForm.BringToFront(); // Traz o formulário para a frente
                loginForm.Activate(); // Ativa o formulário
            }
        }

        private void cadastroToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Cadastroform>().Count() == 0)
            {
                // Cria uma instância do formulário de cadastro
                Cadastroform cadastro = new Cadastroform();
                cadastro.MdiParent = this;
                // Abre o formulário de login
                cadastro.Show();
                cadastro.BringToFront(); 
                cadastro.Activate();
            }
            else
            {
                var cadastroform = Application.OpenForms.OfType<Cadastroform>().First();
                cadastroform.WindowState = FormWindowState.Normal; // Restaura o estado da janela (se estiver minimizada)
                cadastroform.BringToFront(); // Traz o formulário para a frente
                cadastroform.Activate(); // Ativa o formulário
            }
        }
    }
}
