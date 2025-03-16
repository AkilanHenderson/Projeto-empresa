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
    public partial class FilialAtualizar : Form
    {

        private FilialDTO _filial;
        private FilialController _filialController;

        public FilialDTO FilialAtualizado { get; private set; }

        public FilialAtualizar( FilialDTO filial)
        {
            InitializeComponent();

            // Obtém a string de conexão do App.config
            string connectionString = ConfigurationManager.ConnectionStrings["Empresa"].ConnectionString;

            // Instancia os DAOs com a string de conexão
            FilialDAO filialDAO = new FilialDAO(connectionString);

            // Instancia os controladores com os DAOs
            _filialController = new FilialController(filialDAO);

            // Armazena o funcionário atual
            _filial = filial;

            // Preenche os campos com os dados atuais do funcionário
            txtNome.Text = filial.Nome;
            txtEndereco.Text = filial.Endereco;
            txtTelefone.Text = filial.Telefone;
            txtEmail.Text = filial.Email;
            txtSenha.Text = filial.Senha;
            txtCnpj.Text = filial.Cnpj;
            txtNomeFilial.Text = filial.NomeFilial;
            txtDescricao.Text = filial.Descricao;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                // Atualiza os dados da filial
                _filial.Nome = txtNome.Text;
                _filial.Endereco = txtEndereco.Text;
                _filial.Telefone = txtTelefone.Text;
                _filial.Email = txtEmail.Text;
                _filial.Senha = txtSenha.Text;
                _filial.Cnpj = txtCnpj.Text;
                _filial.NomeFilial = txtNomeFilial.Text;
                _filial.Descricao = txtDescricao.Text;

                // Atualiza a filial no banco de dados
                _filialController.AtualizarFilial(_filial);

                // Define o resultado do formulário como OK
                this.DialogResult = DialogResult.OK;
                this.FilialAtualizado = _filial;
                this.Close();
            }
            catch (Exception ex)
            {
                // Exibe mensagem de erro
                MessageBox.Show($"Erro ao atualizar Filial: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilialaAtualizar_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Fecha o formulário de atualização sem salvar as alterações
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
