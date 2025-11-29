using System;

namespace SistemaTickets.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}