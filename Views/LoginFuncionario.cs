using Projeto_empresa.Controllers;
using Projeto_empresa.DAOs;
using Projeto_empresa.DTOs;
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
    public partial class LoginFuncionario : Form
    {
        private readonly FuncionarioController _funcionarioController;
        public LoginFuncionario()
        {
            InitializeComponent();

            // Configurar a conexão com o banco
            string connectionString = "Server=localhost;Port=3306;Database=empresa;Uid=root;Pwd=13593204;";
           
            FuncionarioDAO funcionarioDAO = new FuncionarioDAO(connectionString);
            _funcionarioController = new FuncionarioController(funcionarioDAO);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

            //impedir que o campo seja nulo
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha todos os campos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Valida o login
            bool loginValido = _funcionarioController.ValidarLogin(email, senha);


            if (loginValido)
            {
                // Busca os dados do funcionário
                FuncionarioDTO funcionario = _funcionarioController.BuscarFuncionarioPorEmail(email);

                if (funcionario != null)
                {

                    MessageBox.Show("Login realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Abre a tela de login do funcionario 
                    Funcionarioforms funcionarioforms = new Funcionarioforms(funcionario);
                    funcionarioforms.Show();
                    this.Hide(); // Oculta a tela de login

                }

                else
                {
                    MessageBox.Show("Funcionário não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("Email ou senha incorretos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginFuncionario_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Loginform login = new Loginform();
            login.Show();
            this.Close();
        }
    }
}
