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
    public partial class Filialforms : Form
    {
        private FilialController _filialController;
        private FuncionarioController _funcionarioController;
        private FilialDTO _filial;

        public Filialforms(FilialDTO filial)
        {
            InitializeComponent();

            // Obtém a string de conexão do App.config
            string connectionString = ConfigurationManager.ConnectionStrings["Empresa"].ConnectionString;

            // Instancia o DAO com a string de conexão
            FilialDAO filialDAO = new FilialDAO(connectionString);
            FuncionarioDAO funcionarioDAO = new FuncionarioDAO(connectionString);

            // Instancia o controlador com o DAO
            _filialController = new FilialController(filialDAO);
            _funcionarioController = new FuncionarioController(funcionarioDAO);

            // Armazena o funcionário atual
            _filial = filial;

            // Exibe os dados da filial nos TextBoxes
            txtId.Text = filial.Id.ToString(); // Adiciona o ID ao campo oculto
            txtNome.Text = filial.Nome;
            txtEndereco.Text = filial.Endereco;
            txtTelefone.Text = filial.Telefone;
            txtEmail.Text = filial.Email;
            txtSenha.Text = filial.Senha;
            txtCnpj.Text = filial.Cnpj;
            txtNomeFilial.Text = filial.NomeFilial;
            txtDescricao.Text = filial.Descricao;

            // Desabilita a edição dos TextBoxes
            txtNome.ReadOnly = true;
            txtEndereco.ReadOnly = true;
            txtTelefone.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtSenha.ReadOnly = true;
            txtCnpj.ReadOnly = true;
            txtNomeFilial.ReadOnly = true;
            txtDescricao.ReadOnly = true;

            // Carrega os funcionários da filial no ListBox
            CarregarFuncionarios();

        }

        private void CarregarFuncionarios()
        {
            try
            {
                // Obtém os funcionários da filial atual
                var funcionarios = _funcionarioController.ListarFuncionariosPorFilial(_filial.Id);

                // Limpa o ListBox
                listFuncionario.Items.Clear();

                // Adiciona os funcionários ao ListBox
                foreach (var funcionario in funcionarios)
                {
                    string funcionarioInfo = $"Nome: {funcionario.Nome}, Email: {funcionario.Email}, Idade: {funcionario.Idade}, Telefone: {funcionario.Telefone}";
                    listFuncionario.Items.Add(funcionarioInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar funcionários: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void Filialforms_Load(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            // Abre o formulário de atualização, passando os dados atuais
            FilialAtualizar filialaAtualizar = new FilialAtualizar(_filial);

            DialogResult result = filialaAtualizar.ShowDialog(); // Abre o formulário de atualização como uma janela modal
            // Atualiza os dados no formulário principal após a edição

            if (result == DialogResult.OK)
            {
                _filial = filialaAtualizar.FilialAtualizado; 
                txtNome.Text = _filial.Nome;
                txtEndereco.Text = _filial.Endereco;
                txtTelefone.Text = _filial.Telefone;
                txtEmail.Text = _filial.Email;
                txtSenha.Text = _filial.Senha;
                txtCnpj.Text = _filial.Cnpj;
                txtNomeFilial.Text = _filial.NomeFilial;
                txtDescricao.Text = _filial.Descricao;
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
                    // Deleta a filial
                    _filialController.DeletarFilial(int.Parse(txtId.Text)); // Supondo que você tenha um campo oculto para o ID
                    MessageBox.Show("Conta deletada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Fecha a tela da filial
                    Form1 form1 = new Form1();
                    form1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao deletar conta: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
           
            this.Hide();
        }
    }
}
