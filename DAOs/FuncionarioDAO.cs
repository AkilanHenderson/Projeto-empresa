using MySqlConnector;
using Projeto_empresa.DTOs;
using System.Collections.Generic;


namespace Projeto_empresa.DAOs
{
    public class FuncionarioDAO
    {
        private readonly string _connectionString;

        // Construtor que recebe a string de conexão
        public FuncionarioDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para inserir um funcionário no banco de dados
        public void Insert(FuncionarioDTO funcionario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO Funcionarios (Nome, Cpf, Telefone, Email, Idade, FilialId)
                    VALUES (@Nome, @Cpf, @Telefone, @Email, @Idade, @FilialId);
                ";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@Cpf", funcionario.Cpf);
                    command.Parameters.AddWithValue("@Telefone", funcionario.Telefone);
                    command.Parameters.AddWithValue("@Email", funcionario.Email);
                    command.Parameters.AddWithValue("@Idade", funcionario.Idade);
                    command.Parameters.AddWithValue("@FilialId", funcionario.FilialId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para recuperar todos os funcionários do banco de dados
        public List<FuncionarioDTO> GetAll()
        {
            var funcionarios = new List<FuncionarioDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Funcionarios";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        funcionarios.Add(new FuncionarioDTO
                        {
                            Id = reader.GetInt32("Id"),
                            Nome = reader.GetString("Nome"),
                            Cpf = reader.GetString("Cpf"),
                            Telefone = reader.GetString("Telefone"),
                            Email = reader.GetString("Email"),
                            Idade = reader.GetInt32("Idade"),
                            FilialId = reader.GetInt32("FilialId")
                        });
                    }
                }
            }

            return funcionarios;
        }
    }
}
