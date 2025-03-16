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
                    INSERT INTO Funcionarios (Nome, Cpf, Telefone, Email, Idade, Senha, FilialId)
                    VALUES (@Nome, @Cpf, @Telefone, @Email, @Idade, @Senha, @FilialId);
                ";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@Cpf", funcionario.Cpf);
                    command.Parameters.AddWithValue("@Telefone", funcionario.Telefone);
                    command.Parameters.AddWithValue("@Email", funcionario.Email);
                    command.Parameters.AddWithValue("@Idade", funcionario.Idade);
                    command.Parameters.AddWithValue("@Senha", funcionario.Senha);
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
                            Senha = reader.GetString("Senha"),
                            Idade = reader.GetInt32("Idade"),
                            FilialId = reader.GetInt32("FilialId")
                        });
                    }
                }
            }

            return funcionarios;
        }

        //Metodo para validar o login
        public bool ValidarLogin(string email, string senha)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Id FROM Funcionarios WHERE Email = @Email AND Senha = @Senha";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Senha", senha);

                    using (var reader = command.ExecuteReader())
                    {
                        // Se houver um registro com o email e senha fornecidos, o login é válido
                        return reader.HasRows;
                    }
                }
            }
        }
        //buscar o funcionario com base no email
        public FuncionarioDTO BuscarFuncionarioPorEmail(string email)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                            SELECT f.*, fi.Nome AS NomeFilial
                            FROM Funcionarios f
                            INNER JOIN Filiais fi ON f.FilialId = fi.Id
                            WHERE f.Email = @Email";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FuncionarioDTO
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Nome"),
                                Cpf = reader.GetString("Cpf"),
                                Telefone = reader.GetString("Telefone"),
                                Email = reader.GetString("Email"),
                                Idade = reader.GetInt32("Idade"),
                                Senha = reader.GetString("Senha"),
                                FilialId = reader.GetInt32("FilialId"),
                                NomeFilial = reader.GetString("NomeFilial")
                                
                            };
                        }
                    }
                }
            }

            return null; // Retorna null se o funcionário não for encontrado
        }

        //metodo para atualizar o campo funcionario
        public void AtualizarFuncionario(FuncionarioDTO funcionario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
            UPDATE Funcionarios
            SET Nome = @Nome, Cpf = @Cpf, Telefone = @Telefone, Email = @Email, Idade = @Idade, FilialId = @FilialId
            WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@Cpf", funcionario.Cpf);
                    command.Parameters.AddWithValue("@Telefone", funcionario.Telefone);
                    command.Parameters.AddWithValue("@Email", funcionario.Email);
                    command.Parameters.AddWithValue("@Idade", funcionario.Idade);
                    command.Parameters.AddWithValue("@FilialId", funcionario.FilialId);
                    command.Parameters.AddWithValue("@Id", funcionario.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para recuperar todos os funcionários de uma filial específica
        public List<FuncionarioDTO> GetFuncionariosPorFilial(int filialId)
        {
            var funcionarios = new List<FuncionarioDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
            SELECT f.*, fi.NomeFilial
            FROM Funcionarios f
            INNER JOIN Filiais fi ON f.FilialId = fi.Id
            WHERE f.FilialId = @FilialId"; // Filtra por FilialId

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FilialId", filialId); // Passa o ID da filial como parâmetro

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
                                Senha = reader.GetString("Senha"),
                                FilialId = reader.GetInt32("FilialId"),
                                NomeFilial = reader.GetString("NomeFilial") // Nome da filial (opcional)
                            });
                        }
                    }
                }
            }

            return funcionarios;
        }

        //metodo para deletar o funcionario
        public void DeletarFuncionario(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Funcionarios WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
