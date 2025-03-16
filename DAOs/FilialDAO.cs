using MySqlConnector;
using Projeto_empresa.DTOs;
using System.Collections.Generic;


namespace Projeto_empresa.DAOs
{
    public class FilialDAO
    {
        private readonly string _connectionString;

        // Construtor que recebe a string de conexão
        public FilialDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para inserir uma filial no banco de dados
        public void Insert(FilialDTO filial)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO Filiais (Nome, Endereco, Telefone, Descricao, Site, NomeFilial, Cnpj, Senha, Email)
                    VALUES (@Nome, @Endereco, @Telefone, @Descricao, @Site, @NomeFilial, @Cnpj, @Senha, @Email);
                ";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", filial.Nome);
                    command.Parameters.AddWithValue("@Endereco", filial.Endereco);
                    command.Parameters.AddWithValue("@Telefone", filial.Telefone);
                    command.Parameters.AddWithValue("@Descricao", filial.Descricao);
                    command.Parameters.AddWithValue("@Site", filial.Site);
                    command.Parameters.AddWithValue("@NomeFilial", filial.NomeFilial);
                    command.Parameters.AddWithValue("@Cnpj", filial.Cnpj);
                    command.Parameters.AddWithValue("@Senha", filial.Senha);
                    command.Parameters.AddWithValue("@Email", filial.Email);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para recuperar todas as filiais do banco de dados
        public List<FilialDTO> GetAll()
        {
            var filiais = new List<FilialDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Filiais";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        filiais.Add(new FilialDTO
                        {
                            Id = reader.GetInt32("Id"),
                            Nome = reader.GetString("Nome"),
                            Endereco = reader.GetString("Endereco"),
                            Telefone = reader.GetString("Telefone"),
                            Descricao = reader.GetString("Descricao"),
                            Site = reader.GetString("Site"),
                            NomeFilial = reader.GetString("NomeFilial"),
                            Cnpj = reader.GetString("Cnpj"),
                            Senha = reader.GetString("Senha"),
                            Email = reader.GetString("Email")

                        });
                    }
                }
            }

            return filiais;
        }
    

    // Método para recuperar apenas Id e Nome das filiais (para o ComboBox)
        public List<FilialDTO> GetAllParaComboBox()
        {
            var filiais = new List<FilialDTO>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Id, NomeFilial FROM Filiais"; // Apenas Id e Nome

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        filiais.Add(new FilialDTO
                        {
                            Id = reader.GetInt32("Id"),
                            NomeFilial = reader.GetString("NomeFilial")
                        });
                    }
                }
            }

            return filiais;
        }

        //Metodo para validar o login
        public bool ValidarLogin(string email, string senha)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Id FROM Filiais WHERE Email = @Email AND Senha = @Senha";

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

        //metodo buscar filial por email
        public FilialDTO BuscarFilialPorEmail(string email)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Filiais WHERE Email = @Email";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FilialDTO
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Nome"),
                                Endereco = reader.GetString("Endereco"),
                                Telefone = reader.GetString("Telefone"),
                                Email = reader.GetString("Email"),
                                Senha = reader.GetString("Senha"),
                                NomeFilial = reader.GetString("NomeFilial"),
                                Descricao = reader.GetString("Descricao"),
                                Cnpj = reader.GetString("Cnpj")
                            };
                        }
                    }
                }
            }

            return null; 
        }

        //metodo para atulizar filial
        public void AtualizarFilial(FilialDTO filial)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
            UPDATE Filiais
            SET Nome = @Nome, Endereco = @Endereco, Telefone = @Telefone, Email = @Email, Senha = @Senha, NomeFilial = @NomeFilial, Descricao = @Descricao, Cnpj = @Cnpj
            WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", filial.Nome);
                    command.Parameters.AddWithValue("@Endereco", filial.Endereco);
                    command.Parameters.AddWithValue("@Telefone", filial.Telefone);
                    command.Parameters.AddWithValue("@Email", filial.Email);
                    command.Parameters.AddWithValue("@Senha", filial.Senha);
                    command.Parameters.AddWithValue("@NomeFilial", filial.NomeFilial);
                    command.Parameters.AddWithValue("@Descricao", filial.Descricao);
                    command.Parameters.AddWithValue("@Cnpj", filial.Cnpj);
                    command.Parameters.AddWithValue("@Id", filial.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        //metodo para deletar filial
        public void DeletarFilial(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Filiais WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
