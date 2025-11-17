using System;

namespace SistemaTickets.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string NumeroTicket { get; set; }
        public int ClienteId { get; set; }
        public int SistemaId { get; set; }
        public int? UsuarioAsignadoId { get; set; }
        public string TipoTicket { get; set; }
        public string NivelUrgencia { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int TiempoAtencion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaSolucion { get; set; }
        public string Solucion { get; set; }
        public string DerivadoA { get; set; }
        public DateTime? FechaDerivacion { get; set; }

        // Propiedades de navegación
        public string ClienteNombre { get; set; }
        public string SistemaNombre { get; set; }
        public string UsuarioAsignadoNombre { get; set; }
    }
}