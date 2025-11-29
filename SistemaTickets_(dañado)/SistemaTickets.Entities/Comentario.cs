using System;

namespace SistemaTickets.Entities
{
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public int TicketId { get; set; }
        public int UsuarioId { get; set; }
        public string ComentarioTexto { get; set; }
        public DateTime FechaComentario { get; set; }
        public string UsuarioNombre { get; set; }
    }
}