using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTickets.Entities
{
    public class ReporteTickets
    {
        public int TicketId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string TipoTicket { get; set; }
        public string NivelUrgencia { get; set; }
        public string UsuarioAsignadoId { get; set; }
        public string Solucion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string FechaSolucion { get; set; } 
        public string DerivadoA { get; set; }
    }

}
