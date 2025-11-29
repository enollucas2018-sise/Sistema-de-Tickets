using SistemaTickets.Entities;
using SistemaTickets.Data.Connection;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace SistemaTickets.Data.Repositories
{
    public class TicketDAO
    {
        public int CrearTicket(Ticket ticket)
        {
            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = @"INSERT INTO Tickets 
                            (ClienteId, SistemaId, UsuarioAsignadoId, TipoTicket, NivelUrgencia, 
                             Titulo, Descripcion, Estado, TiempoAtencion, FechaCreacion)
                            OUTPUT INSERTED.TicketId
                            VALUES (@ClienteId, @SistemaId, @UsuarioAsignadoId, @TipoTicket, @NivelUrgencia,
                                    @Titulo, @Descripcion, @Estado, @TiempoAtencion, @FechaCreacion)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClienteId", ticket.ClienteId);
                    command.Parameters.AddWithValue("@SistemaId", ticket.SistemaId);
                    command.Parameters.AddWithValue("@UsuarioAsignadoId", (object)ticket.UsuarioAsignadoId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TipoTicket", ticket.TipoTicket);
                    command.Parameters.AddWithValue("@NivelUrgencia", ticket.NivelUrgencia);
                    command.Parameters.AddWithValue("@Titulo", ticket.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", ticket.Descripcion);
                    command.Parameters.AddWithValue("@Estado", ticket.Estado);
                    command.Parameters.AddWithValue("@TiempoAtencion", ticket.TiempoAtencion);
                    command.Parameters.AddWithValue("@FechaCreacion", ticket.FechaCreacion);

                    return (int)command.ExecuteScalar();
                }
            }
        }

        public List<Ticket> ObtenerTodosTickets()
        {
            var tickets = new List<Ticket>();

            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = @"SELECT t.TicketId, t.NumeroTicket, t.ClienteId, t.SistemaId, t.UsuarioAsignadoId,
                             t.TipoTicket, t.NivelUrgencia, t.Titulo, t.Descripcion, t.Estado,
                             t.TiempoAtencion, t.FechaCreacion, t.FechaSolucion, t.Solucion,
                             t.DerivadoA, t.FechaDerivacion,
                             c.Nombre as ClienteNombre, s.Nombre as SistemaNombre,
                             u.Nombre_Usuario as UsuarioAsignadoNombre
                             FROM Tickets t
                             INNER JOIN Clientes c ON t.ClienteId = c.ClienteId
                             INNER JOIN Sistemas s ON t.SistemaId = s.SistemaId
                             LEFT JOIN Usuarios u ON t.UsuarioAsignadoId = u.UsuarioId
                             ORDER BY t.FechaCreacion DESC";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ticket = new Ticket
                        {
                            TicketId = reader.GetInt32(0),
                            NumeroTicket = reader.GetString(1),
                            ClienteId = reader.GetInt32(2),
                            SistemaId = reader.GetInt32(3),
                            UsuarioAsignadoId = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                            TipoTicket = reader.GetString(5),
                            NivelUrgencia = reader.GetString(6),
                            Titulo = reader.GetString(7),
                            Descripcion = reader.GetString(8),
                            Estado = reader.GetString(9),
                            TiempoAtencion = reader.GetInt32(10),
                            FechaCreacion = reader.GetDateTime(11),
                            ClienteNombre = reader.GetString(16),
                            SistemaNombre = reader.GetString(17),
                            UsuarioAsignadoNombre = reader.IsDBNull(18) ? "No asignado" : reader.GetString(18)
                        };

                        if (!reader.IsDBNull(12))
                            ticket.FechaSolucion = reader.GetDateTime(12);

                        if (!reader.IsDBNull(13))
                            ticket.Solucion = reader.GetString(13);

                        if (!reader.IsDBNull(14))
                            ticket.DerivadoA = reader.GetString(14);

                        if (!reader.IsDBNull(15))
                            ticket.FechaDerivacion = reader.GetDateTime(15);

                        tickets.Add(ticket);
                    }
                }
            }

            return tickets;
        }

        public List<Ticket> ObtenerTicketsPorUsuario(int usuarioId)
        {
            var tickets = new List<Ticket>();

            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = @"SELECT t.TicketId, t.NumeroTicket, t.ClienteId, t.SistemaId, t.UsuarioAsignadoId,
                             t.TipoTicket, t.NivelUrgencia, t.Titulo, t.Descripcion, t.Estado,
                             t.TiempoAtencion, t.FechaCreacion, t.FechaSolucion, t.Solucion,
                             t.DerivadoA, t.FechaDerivacion,
                             c.Nombre as ClienteNombre, s.Nombre as SistemaNombre,
                             u.Nombre_Usuario as UsuarioAsignadoNombre
                             FROM Tickets t
                             INNER JOIN Clientes c ON t.ClienteId = c.ClienteId
                             INNER JOIN Sistemas s ON t.SistemaId = s.SistemaId
                             LEFT JOIN Usuarios u ON t.UsuarioAsignadoId = u.UsuarioId
                             WHERE t.UsuarioAsignadoId = @UsuarioId
                             ORDER BY t.FechaCreacion DESC";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UsuarioId", usuarioId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ticket = new Ticket
                            {
                                TicketId = reader.GetInt32(0),
                                NumeroTicket = reader.GetString(1),
                                ClienteId = reader.GetInt32(2),
                                SistemaId = reader.GetInt32(3),
                                UsuarioAsignadoId = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                                TipoTicket = reader.GetString(5),
                                NivelUrgencia = reader.GetString(6),
                                Titulo = reader.GetString(7),
                                Descripcion = reader.GetString(8),
                                Estado = reader.GetString(9),
                                TiempoAtencion = reader.GetInt32(10),
                                FechaCreacion = reader.GetDateTime(11),
                                ClienteNombre = reader.GetString(16),
                                SistemaNombre = reader.GetString(17),
                                UsuarioAsignadoNombre = reader.IsDBNull(18) ? "No asignado" : reader.GetString(18)
                            };

                            if (!reader.IsDBNull(12))
                                ticket.FechaSolucion = reader.GetDateTime(12);

                            if (!reader.IsDBNull(13))
                                ticket.Solucion = reader.GetString(13);

                            if (!reader.IsDBNull(14))
                                ticket.DerivadoA = reader.GetString(14);

                            if (!reader.IsDBNull(15))
                                ticket.FechaDerivacion = reader.GetDateTime(15);

                            tickets.Add(ticket);
                        }
                    }
                }
            }

            return tickets;
        }

        public Ticket ObtenerTicketPorId(int ticketId)
        {
            Ticket ticket = null;

            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = @"SELECT t.TicketId, t.NumeroTicket, t.ClienteId, t.SistemaId, t.UsuarioAsignadoId,
                             t.TipoTicket, t.NivelUrgencia, t.Titulo, t.Descripcion, t.Estado,
                             t.TiempoAtencion, t.FechaCreacion, t.FechaSolucion, t.Solucion,
                             t.DerivadoA, t.FechaDerivacion,
                             c.Nombre as ClienteNombre, s.Nombre as SistemaNombre,
                             u.Nombre_Usuario as UsuarioAsignadoNombre
                             FROM Tickets t
                             INNER JOIN Clientes c ON t.ClienteId = c.ClienteId
                             INNER JOIN Sistemas s ON t.SistemaId = s.SistemaId
                             LEFT JOIN Usuarios u ON t.UsuarioAsignadoId = u.UsuarioId
                             WHERE t.TicketId = @TicketId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TicketId", ticketId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ticket = new Ticket
                            {
                                TicketId = reader.GetInt32(0),
                                NumeroTicket = reader.GetString(1),
                                ClienteId = reader.GetInt32(2),
                                SistemaId = reader.GetInt32(3),
                                UsuarioAsignadoId = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                                TipoTicket = reader.GetString(5),
                                NivelUrgencia = reader.GetString(6),
                                Titulo = reader.GetString(7),
                                Descripcion = reader.GetString(8),
                                Estado = reader.GetString(9),
                                TiempoAtencion = reader.GetInt32(10),
                                FechaCreacion = reader.GetDateTime(11),
                                ClienteNombre = reader.GetString(16),
                                SistemaNombre = reader.GetString(17),
                                UsuarioAsignadoNombre = reader.IsDBNull(18) ? "No asignado" : reader.GetString(18)
                            };

                            if (!reader.IsDBNull(12))
                                ticket.FechaSolucion = reader.GetDateTime(12);

                            if (!reader.IsDBNull(13))
                                ticket.Solucion = reader.GetString(13);

                            if (!reader.IsDBNull(14))
                                ticket.DerivadoA = reader.GetString(14);

                            if (!reader.IsDBNull(15))
                                ticket.FechaDerivacion = reader.GetDateTime(15);
                        }
                    }
                }
            }

            return ticket;
        }

        public void ActualizarTicket(Ticket ticket)
        {
            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = @"UPDATE Tickets SET 
                            Estado = @Estado,
                            Solucion = @Solucion,
                            FechaSolucion = @FechaSolucion,
                            DerivadoA = @DerivadoA,
                            FechaDerivacion = @FechaDerivacion,
                            TiempoAtencion = @TiempoAtencion
                            WHERE TicketId = @TicketId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Estado", ticket.Estado);
                    command.Parameters.AddWithValue("@Solucion", (object)ticket.Solucion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FechaSolucion", (object)ticket.FechaSolucion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DerivadoA", (object)ticket.DerivadoA ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FechaDerivacion", (object)ticket.FechaDerivacion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TiempoAtencion", ticket.TiempoAtencion);
                    command.Parameters.AddWithValue("@TicketId", ticket.TicketId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> ObtenerClientes()
        {
            var clientes = new List<Cliente>();

            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = "SELECT ClienteId, Nombre, Email, Telefono FROM Clientes WHERE Activo = 1 ORDER BY Nombre";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente
                        {
                            ClienteId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Email = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Telefono = reader.IsDBNull(3) ? "" : reader.GetString(3)
                        });
                    }
                }
            }

            return clientes;
        }

        public List<Sistema> ObtenerSistemas()
        {
            var sistemas = new List<Sistema>();

            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = "SELECT SistemaId, Nombre,  Descripcion FROM Sistemas WHERE Activo = 1 ORDER BY Nombre";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sistemas.Add(new Sistema
                        {
                            SistemaId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2)
                        });
                    }
                }
            }

            return sistemas;
        }

        public List<Usuario> ObtenerUsuariosPorRol(string rol)
        {
            var usuarios = new List<Usuario>();

            using (var connection = ConnectionDB.GetConnection())
            {
                connection.Open();
                var query = "SELECT UsuarioId, Nombre_Usuario, Email, Rol FROM Usuarios WHERE Rol = @Rol AND Activo = 1 ORDER BY Nombre_Usuario";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Rol", rol);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario
                            {
                                UsuarioId = reader.GetInt32(0),
                                Nombre_Usuario = reader.GetString(1),
                                Email = reader.GetString(2),
                                Rol = reader.GetString(3)
                            });
                        }
                    }
                }
            }

            return usuarios;
        }
    }
}