using Projeto_empresa.Controllers;
using Projeto_empresa.DTOs;
using Projeto_empresa.Models;
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
    public partial class Funcionarioforms : Form
    {
        private FuncionarioController _funcionarioController;

        public Funcionarioforms(FuncionarioDTO funcionario)
        {
            InitializeComponent();

            // Exibe os dados do funcionário nos TextBoxes
            txtId.Text = funcionario.Id.ToString(); // Adiciona o ID ao campo oculto
            txtNome.Text = funcionario.Nome;
            txtCpf.Text = funcionario.Cpf;
            txtTelefone.Text = funcionario.Telefone;
            txtEmail.Text = funcionario.Email;
            txtSenha.Text = funcionario.Senha;
            txtIdade.Text = funcionario.Idade.ToString();
            txtFilial.Text = funcionario.NomeFilial; 
        }
        
        private void Funcionarioforms_Load(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

            try
            {
                // Cria um novo FuncionarioDTO com os dados atualizados
                FuncionarioDTO funcionario = new FuncionarioDTO
                {
                    Id = int.Parse(txtId.Text), // Supondo que você tenha um campo oculto para o ID
                    Nome = txtNome.Text,
                    Cpf = txtCpf.Text,
                    Telefone = txtTelefone.Text,
                    Email = txtEmail.Text,
                    Idade = int.Parse(txtIdade.Text),
                    Senha = txtSenha.Text,
                    FilialId = int.Parse(txtFilial.Text)
                };

                // Atualiza os dados do funcionário
                _funcionarioController.AtualizarFuncionario(funcionario);
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
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}
