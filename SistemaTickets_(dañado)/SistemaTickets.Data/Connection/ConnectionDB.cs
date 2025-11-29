using System.Data.SqlClient;

namespace SistemaTickets.Data.Connection
{
    public class ConnectionDB
    {
        private static readonly string _connectionString;

        // Constructor estático para inicializar la cadena de conexión
        static ConnectionDB()
        {
            _connectionString = "Server=DESKTOP-LCJVT2J;Database=SistemaTickets;User Id=sa;Password=sqladmin";
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}