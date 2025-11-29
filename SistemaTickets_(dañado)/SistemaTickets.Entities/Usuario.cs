using System;

namespace SistemaTickets.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre_Usuario { get; set; } // Mapea desde Nombre_Usuario (formato corto)
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedad de solo lectura para nombre completo
        public string NombreCompleto
        {
            get
            {
                if (!string.IsNullOrEmpty(Nombres) && !string.IsNullOrEmpty(ApellidoPaterno))
                {
                    string nombreCompleto = $"{Nombres} {ApellidoPaterno}";
                    if (!string.IsNullOrEmpty(ApellidoMaterno))
                    {
                        nombreCompleto += $" {ApellidoMaterno}";
                    }
                    return nombreCompleto;
                }
                else
                {
                    return Nombre_Usuario; // Fallback al formato corto
                }
            }
        }
    }
}