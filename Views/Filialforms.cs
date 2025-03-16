using Projeto_empresa.Controllers;
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
    public partial class Filialforms : Form
    {
        private FilialController _filialController;

        public Filialforms(FilialDTO filial)
        {
            InitializeComponent();

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
        }

        private void Filialforms_Load(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Cria um novo FilialDTO com os dados atualizados
                FilialDTO filial = new FilialDTO
                {
                    Id = int.Parse(txtId.Text), // Supondo que você tenha um campo oculto para o ID
                    Nome = txtNome.Text,
                    Endereco = txtEndereco.Text,
                    Telefone = txtTelefone.Text,
                    Email = txtEmail.Text,
                    Senha = txtSenha.Text,
                    NomeFilial = txtNomeFilial.Text,
                    Descricao = txtDescricao.Text,
                    Cnpj = txtCnpj.Text
                };

                // Atualiza os dados da filial
                _filialController.AtualizarFilial(filial);
                MessageBox.Show("Dados atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
