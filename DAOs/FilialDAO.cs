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
                    INSERT INTO Filiais (Nome, Endereco, Telefone)
                    VALUES (@Nome, @Endereco, @Telefone);
                ";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", filial.Nome);
                    command.Parameters.AddWithValue("@Endereco", filial.Endereco);
                    command.Parameters.AddWithValue("@Telefone", filial.Telefone);
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
                            Telefone = reader.GetString("Telefone")
                        });
                    }
                }
            }

            return filiais;
        }
    }
}
