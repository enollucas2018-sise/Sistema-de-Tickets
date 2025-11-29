using System;

namespace SistemaTickets.Entities
{
    public class ArchivoAdjunto
    {
        public int ArchivoId { get; set; }
        public int TicketId { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoArchivo { get; set; }
        public byte[] ArchivoData { get; set; }
        public DateTime FechaSubida { get; set; }
    }
}