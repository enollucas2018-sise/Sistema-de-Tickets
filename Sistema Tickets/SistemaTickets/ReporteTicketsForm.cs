using Microsoft.Reporting.WinForms;
using SistemaTickets.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTickets
{
    public partial class ReporteTicketsForm : Form
    {
        private List<Ticket> _tickets;
        public ReporteTicketsForm()
        {
            InitializeComponent();
        }

        private void ReporteTicketsForm_Load(object sender, EventArgs e)
        {
            _tickets = new List<Ticket>
            {
                new Ticket {TicketId = 1, Titulo="prueba",Estado="Abierto", NivelUrgencia ="Bajo"},
            };

            ReportDataSource rds = new ReportDataSource("DataSet1", _tickets);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportPath = "ReporteTickets.rdlc";


            this.reportViewer1.RefreshReport();
       }
    }
}





