using SistemaTickets.Data.Connection;
using SistemaTickets.Entities;
using System.Data.SqlClient;

namespace SistemaTickets.Data.Repositories
{
    public class LoginDAO
    {
        public Usuario ValidarUsuario(string email, string password)
        {
            Usuario usuario = null;

            // Ahora usamos DatabaseConnection.GetConnection() directamente sin instancia
            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = @"SELECT UsuarioId, Nombre, Email, Password, Rol, Activo, FechaCreacion 
                             FROM Usuarios 
                             WHERE Email = @Email AND Password = @Password AND Activo = 1";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                UsuarioId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Email = reader.GetString(2),
                                Password = reader.GetString(3),
                                Rol = reader.GetString(4),
                                Activo = reader.GetBoolean(5),
                                FechaCreacion = reader.GetDateTime(6)
                            };
                        }
                    }
                }
            }

            return usuario;
        }
    }
}