using SistemaTickets.Entities;
using SistemaTickets.Data.Repositories;
using System.Collections.Generic;

namespace SistemaTickets.Business.Services
{
    public class TicketService
    {
        private readonly TicketDAO _ticketDAO;

        public TicketService()
        {
            _ticketDAO = new TicketDAO();
        }

        public int CrearTicket(Ticket ticket)
        {
            if (string.IsNullOrEmpty(ticket.Titulo))
                throw new System.ArgumentException("El título es requerido");

            if (string.IsNullOrEmpty(ticket.Descripcion))
                throw new System.ArgumentException("La descripción es requerida");

            if (ticket.ClienteId <= 0)
                throw new System.ArgumentException("Debe seleccionar un cliente");

            if (ticket.SistemaId <= 0)
                throw new System.ArgumentException("Debe seleccionar un sistema");

            return _ticketDAO.CrearTicket(ticket);
        }

        public List<Ticket> ObtenerTodosTickets()
        {
            return _ticketDAO.ObtenerTodosTickets();
        }

        public List<Ticket> ObtenerTicketsPorUsuario(int usuarioId)
        {
            return _ticketDAO.ObtenerTicketsPorUsuario(usuarioId);
        }

        public Ticket ObtenerTicketPorId(int ticketId)
        {
            return _ticketDAO.ObtenerTicketPorId(ticketId);
        }

        public void ActualizarTicket(Ticket ticket)
        {
            _ticketDAO.ActualizarTicket(ticket);
        }

        public List<Cliente> ObtenerClientes()
        {
            return _ticketDAO.ObtenerClientes();
        }

        public List<Sistema> ObtenerSistemas()
        {
            return _ticketDAO.ObtenerSistemas();
        }

        public List<Usuario> ObtenerUsuariosPorRol(string rol)
        {
            return _ticketDAO.ObtenerUsuariosPorRol(rol);
        }
    }
}