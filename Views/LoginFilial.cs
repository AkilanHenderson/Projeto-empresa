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
    public partial class LoginFilial : Form
    {
        private readonly FilialController _filialController;
        public LoginFilial()
        {
            InitializeComponent();

            // Configurar a conexão com o banco
            string connectionString = "Server=localhost;Port=3306;Database=empresa;Uid=root;Pwd=13593204;";

            // Inicializar o controller
            FilialDAO filialDAO = new FilialDAO(connectionString);
            _filialController = new FilialController(filialDAO);

        }

        private void LoginFilial_Load(object sender, EventArgs e)
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
            bool loginValido = _filialController.ValidarLogin(email, senha);

            if (loginValido)
            {
                MessageBox.Show("Login realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Busca os dados da filial
                FilialDTO filial = _filialController.BuscarFilialPorEmail(email);

                if (filial != null)
                {

                    
                    Filialforms filialforms = new Filialforms(filial);
                    filialforms.Show();
                    this.Hide(); // Oculta a tela de login
                
                }
                else
                {

                    MessageBox.Show("Empresa não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("Email ou senha incorretos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
          
            this.Hide();
        }
    }
}
