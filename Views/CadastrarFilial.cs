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
    public partial class CadastrarFilial : Form
    {
        private readonly FilialController _filialController;
        public CadastrarFilial()
        {
            InitializeComponent();

            // Criando a conexão com o banco
            string connectionString = "Server=localhost;Port=3306;Database=empresa;Uid=root;Pwd=13593204;";
            FilialDAO filialDAO = new FilialDAO(connectionString);
            _filialController = new FilialController(filialDAO);
        }
        private void CadastrarFilial_Load(object sender, EventArgs e)
        {

        }

        private void Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Criar objeto FilialDTO com os valores dos TextBoxes
                FilialDTO novaFilial = new FilialDTO
                {
                    Nome = txtNome.Text,
                    Endereco = txtEndereco.Text,
                    Telefone = txtTelefone.Text,
                    Email = txtEmail.Text,
                    Senha = txtSenha.Text,
                    Descricao = txtDescricao.Text,
                    Site = txtSite.Text,
                    NomeFilial = txtNomeFilial.Text,
                    Cnpj = txtCnpj.Text
                };

                // Validação: impedir campos vazios
                if (string.IsNullOrWhiteSpace(novaFilial.Nome) ||
                    string.IsNullOrWhiteSpace(novaFilial.Endereco) ||
                    string.IsNullOrWhiteSpace(novaFilial.Telefone) ||
                    string.IsNullOrWhiteSpace(novaFilial.Email) ||
                    string.IsNullOrWhiteSpace(novaFilial.Senha) ||
                    string.IsNullOrWhiteSpace(novaFilial.NomeFilial)||
                    string.IsNullOrWhiteSpace(novaFilial.Cnpj))
                {
                    MessageBox.Show("Preencha todos os campos obrigatórios!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Limpa os campos
                txtNome.Clear();
                txtEndereco.Clear();
                txtTelefone.Clear();
                txtEmail.Clear();
                txtSenha.Clear();
                txtDescricao.Clear();
                txtSite.Clear();
                txtNomeFilial.Clear();
                txtCnpj.Clear();

                _filialController.CadastrarFilial(novaFilial);
                MessageBox.Show("Filial cadastrada com sucesso!", "Sucesso, faça o login na tela inicial", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar filial: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            this.Hide();
        }
    }
 }

