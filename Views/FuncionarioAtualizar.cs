using Projeto_empresa.Controllers;
using Projeto_empresa.DAOs;
using Projeto_empresa.DTOs;
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
    public partial class FuncionarioAtualizar : Form
    {
        private FuncionarioDTO _funcionario;
        private FuncionarioController _funcionarioController;
        private FilialController _filialController;

        public FuncionarioDTO FuncionarioAtualizado { get; private set; }

        public FuncionarioAtualizar(FuncionarioDTO funcionario)
        {
            InitializeComponent();

            // Obtém a string de conexão do App.config
            string connectionString = ConfigurationManager.ConnectionStrings["Empresa"].ConnectionString;

            // Instancia os DAOs com a string de conexão
            FuncionarioDAO funcionarioDAO = new FuncionarioDAO(connectionString);
            FilialDAO filialDAO = new FilialDAO(connectionString);

            // Instancia os controladores com os DAOs
            _funcionarioController = new FuncionarioController(funcionarioDAO);
            _filialController = new FilialController(filialDAO);

            // Armazena o funcionário atual
            _funcionario = funcionario;

            // Preenche os campos com os dados atuais do funcionário
            txtNome.Text = _funcionario.Nome;
            txtCpf.Text = _funcionario.Cpf;
            txtTelefone.Text = _funcionario.Telefone;
            txtEmail.Text = _funcionario.Email;
            txtIdade.Text = _funcionario.Idade.ToString();
            txtSenha.Text = _funcionario.Senha;

            CarregarFiliais();

            // Seleciona a filial atual do funcionário no ComboBox
            cmbFilial.SelectedValue = _funcionario.FilialId;
        }

        private void CarregarFiliais()
        {
            try
            {
                // Obtém a lista de filiais (apenas Id e Nome) do banco de dados
                var filiais = _filialController.ListarFiliaisParaComboBox();

                // Configura o ComboBox
                cmbFilial.DataSource = filiais; // Define a lista de filiais como fonte de dados
                cmbFilial.DisplayMember = "NomeFilial"; // Exibe o nome da filial no ComboBox
                cmbFilial.ValueMember = "Id"; // Define o valor selecionado como o ID da filial
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar filiais: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FuncionarioAtualizar_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirma_Click(object sender, EventArgs e)
        {
            try
            {
                // Atualiza o objeto FuncionarioDTO com os dados editados
                _funcionario.Nome = txtNome.Text;
                _funcionario.Cpf = txtCpf.Text;
                _funcionario.Telefone = txtTelefone.Text;
                _funcionario.Email = txtEmail.Text;
                _funcionario.Idade = int.Parse(txtIdade.Text);
                _funcionario.Senha = txtSenha.Text;
                _funcionario.FilialId = (int)cmbFilial.SelectedValue; // Obtém o ID da filial selecionada

                // Chama o controlador para atualizar os dados no banco
                _funcionarioController.AtualizarFuncionario(_funcionario);

                // Define o resultado do formulário como OK
                this.DialogResult = DialogResult.OK;
                this.FuncionarioAtualizado = _funcionario;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar Funcionario: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Fecha o formulário de atualização sem salvar as alterações
            this.DialogResult = DialogResult.Cancel;
            this.Close();


        }
    }
 }

