using System;
using SistemaTickets.Entities;
using SistemaTickets.Data.Repositories;

namespace SistemaTickets.Business.Services
{
    public class LoginService
    {
        private readonly LoginDAO _loginDAO;

        public LoginService()
        {
            _loginDAO = new LoginDAO();
        }

        public Usuario ValidarCredenciales(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Email y password son requeridos");
            }

            return _loginDAO.ValidarUsuario(email, password);
        }
    }
}