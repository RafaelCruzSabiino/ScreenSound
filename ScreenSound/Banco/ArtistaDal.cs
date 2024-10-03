using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    internal class ArtistaDal : Connection
    {
        public IEnumerable<Artista> Listar()
        {
            List<Artista> lista = new List<Artista>();
            using (SqlConnection connection = ObterConexao())
            {
                connection.Open();
                string sql = "SELECT * FROM Artistas";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Artista artista = new Artista(Convert.ToString(dataReader["Nome"]), Convert.ToString(dataReader["Bio"]))
                        {
                            Id = Convert.ToInt32(dataReader["Id"])
                        };

                        lista.Add(artista);
                    }
                }
            }

            return lista;
        }

        public void Adicionar(Artista artista)
        {
            using (SqlConnection connection = ObterConexao())
            {
                connection.Open();
                string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@nome", artista.Nome);
                command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
                command.Parameters.AddWithValue("@bio", artista.Bio);

                Console.WriteLine(command.ExecuteNonQuery());
            }
        }

        public void Atualizar(Artista artista)
        {
            using (SqlConnection connection = ObterConexao())
            {
                connection.Open();
                string sql = "UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@nome", artista.Nome);
                command.Parameters.AddWithValue("@bio", artista.Bio);
                command.Parameters.AddWithValue("@id", artista.Id);

                Console.WriteLine(command.ExecuteNonQuery());
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection connection = ObterConexao())
            {
                connection.Open();
                string sql = "DELETE FROM Artistas WHERE Id = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);

                Console.WriteLine(command.ExecuteNonQuery());
            }
        }
    }
}
