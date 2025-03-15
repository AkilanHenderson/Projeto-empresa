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

        private void button1_Click(object sender, EventArgs e)
        {
            // Cria uma instância do formulário de login
            Loginform login = new Loginform();

            // Abre o formulário de login
            login.Show();

            // Opcional: Fechar ou ocultar o formulário atual
            this.Hide(); // Oculta o formulário atual
                         // this.Close(); // Fecha o formulário atual (use com cuidado)
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Cria uma instância do formulário de login
            Cadastroform cadastro = new Cadastroform();

            // Abre o formulário de login
            cadastro.Show();

            // Opcional: Fechar ou ocultar o formulário atual
            this.Hide(); // Oculta o formulário atual
                         // this.Close(); // Fecha o formulário atual (use com cuidado)
        }
    }
}
