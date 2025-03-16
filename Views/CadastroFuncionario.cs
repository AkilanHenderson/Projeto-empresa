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
    public partial class CadastroFuncionario : Form
    {
        private readonly FuncionarioController _funcionarioController;
        private readonly FilialController _filialController;
        public CadastroFuncionario()
        {
            InitializeComponent();

            // Configurar a conexão com o banco
            string connectionString = "Server=localhost;Port=3306;Database=empresa;Uid=root;Pwd=13593204;";

            // Instanciar os DAOs
            FuncionarioDAO funcionarioDAO = new FuncionarioDAO(connectionString);
            FilialDAO filialDAO = new FilialDAO(connectionString);

            // Instanciar os Controllers
            _funcionarioController = new FuncionarioController(funcionarioDAO);
            _filialController = new FilialController(filialDAO);

            // Carrega as filiais no ComboBox ao abrir o formulário
            CarregarFiliais();
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

        private void CadastroFuncionario_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FuncionarioDTO funcionario = new FuncionarioDTO
                {
                    Nome = txtNome.Text,
                    Cpf = txtCpf.Text,
                    Telefone = txtTelefone.Text,
                    Email = txtEmail.Text,
                    Idade = int.Parse(txtIdade.Text),
                    FilialId = int.Parse(cmbFilial.SelectedValue.ToString()) // ComboBox de Filiais
                };

                _funcionarioController.CadastrarFuncionario(funcionario);

                MessageBox.Show("Funcionário cadastrado com sucesso!, faça o login na tela inicial ");

                LimparCampos();

                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }
            private void LimparCampos()
        {
            txtNome.Clear();
            txtCpf.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            txtIdade.Clear();
            cmbFilial.SelectedIndex = 0; // Volta para a primeira opção do ComboBox
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cadastroform cadastro = new Cadastroform();
            cadastro.Show();
            this.Hide();
        }
    }
   }

