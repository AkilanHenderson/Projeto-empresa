using Projeto_empresa.Controllers;
using Projeto_empresa.DAOs;
using Projeto_empresa.DTOs;
using Projeto_empresa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_empresa.Views
{
    public partial class Funcionarioforms : Form
    {
        private FuncionarioController _funcionarioController;
        private FuncionarioDTO _funcionario;
        public Funcionarioforms(FuncionarioDTO funcionario)
        {
            InitializeComponent();

            // Obtém a string de conexão do App.config
            string connectionString = ConfigurationManager.ConnectionStrings["Empresa"].ConnectionString;

            // Instancia o DAO com a string de conexão
            FuncionarioDAO funcionarioDAO = new FuncionarioDAO(connectionString);

            // Instancia o controlador com o DAO
            _funcionarioController = new FuncionarioController(funcionarioDAO);

            // Armazena o funcionário atual
            _funcionario = funcionario;

            // Exibe os dados do funcionário nos TextBoxes
            txtId.Text = funcionario.Id.ToString(); // Adiciona o ID ao campo oculto
            txtNome.Text = funcionario.Nome;
            txtCpf.Text = funcionario.Cpf;
            txtTelefone.Text = funcionario.Telefone;
            txtEmail.Text = funcionario.Email;
            txtSenha.Text = funcionario.Senha;
            txtIdade.Text = funcionario.Idade.ToString();
            txtFilial.Text = funcionario.NomeFilial;

            // Desabilita a edição dos TextBoxes
            txtId.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtCpf.ReadOnly = true;
            txtTelefone.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtSenha.ReadOnly = true;
            txtIdade.ReadOnly = true;
            txtFilial.ReadOnly = true;
        }
        
        private void Funcionarioforms_Load(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

            // Abre o formulário de atualização, passando os dados atuais
            FuncionarioAtualizar funcionarioatualizar = new FuncionarioAtualizar(_funcionario);
            
            DialogResult result = funcionarioatualizar.ShowDialog(); // Abre o formulário de atualização como uma janela modal
            // Atualiza os dados no formulário principal após a edição

            if (result == DialogResult.OK)
            {
                _funcionario = funcionarioatualizar.FuncionarioAtualizado;
                txtNome.Text = _funcionario.Nome;
                txtCpf.Text = _funcionario.Cpf;
                txtTelefone.Text = _funcionario.Telefone;
                txtEmail.Text = _funcionario.Email;
                txtSenha.Text = _funcionario.Senha;
                txtIdade.Text = _funcionario.Idade.ToString();
                txtFilial.Text = _funcionario.NomeFilial;
            }
            else if (result == DialogResult.Cancel)
            {
                // Se o usuário cancelar, não faz nada (ou pode exibir uma mensagem)
                MessageBox.Show("Atualização cancelada.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {

            try
            {
                // Confirma a exclusão
                DialogResult result = MessageBox.Show("Tem certeza que deseja deletar sua conta?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Deleta o funcionário
                    _funcionarioController.DeletarFuncionario(int.Parse(txtId.Text)); // Supondo que você tenha um campo oculto para o ID
                    MessageBox.Show("Conta deletada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Fecha a tela do funcionário
                    Form1 form1 = new Form1();
                    form1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao deletar conta: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
