using SistemaTickets.Entities;
using SistemaTickets.Data.Connection;
using System.Data.SqlClient;

namespace SistemaTickets.Data.Repositories
{
    public class LoginDAO
    {
        public Usuario ValidarUsuario(string email, string password)
        {
            Usuario usuario = null;

            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = @"SELECT UsuarioId, Nombre_Usuario, Nombres, Apellido_Paterno, Apellido_Materno, 
                             Email, Password, Rol, Activo, FechaCreacion 
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
                                Nombres = reader.GetString(1),
                                ApellidoPaterno = reader.GetString(2),
                                ApellidoMaterno = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Email = reader.GetString(4),
                                Password = reader.GetString(5),
                                Rol = reader.GetString(6),
                                Activo = reader.GetBoolean(7),
                                FechaCreacion = reader.GetDateTime(8)
                            };
                        }
                    }
                }
            }

            return usuario;
        }
    }
}