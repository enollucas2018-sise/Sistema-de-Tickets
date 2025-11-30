using SistemaTickets.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTickets.Presentation
{
    public partial class DetalleTicketForm : Form
    {
        private Ticket _ticket;
        private Usuario _usuarioLogueado;

        public DetalleTicketForm(Ticket ticket, Usuario usuario)
        {
            _ticket = ticket;
            _usuarioLogueado = usuario;
            InitializeCustomComponent();
            CargarDatosTicket();
        }

        private void InitializeCustomComponent()
        {
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = $"Detalle del Ticket - {_ticket.NumeroTicket}";
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            
            var panelPrincipal = new Panel();
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.Padding = new Padding(20);
            panelPrincipal.BackColor = Color.White;
            panelPrincipal.AutoScroll = true;

           
            var lblTitulo = new Label();
            lblTitulo.Text = $"DETALLE DEL TICKET - {_ticket.NumeroTicket}";
            lblTitulo.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0, 102, 204);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            
            var controles = CrearControlesInformacion();

            
            var btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Location = new Point(550, 520);
            btnCerrar.Size = new Size(100, 30);
            btnCerrar.BackColor = Color.Gray;
            btnCerrar.ForeColor = Color.White;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Arial", 10);
            btnCerrar.Click += (s, e) => this.Close();

            
            panelPrincipal.Controls.Add(lblTitulo);
            foreach (var control in controles)
            {
                panelPrincipal.Controls.Add(control);
            }
            panelPrincipal.Controls.Add(btnCerrar);

            this.Controls.Add(panelPrincipal);
        }

        private Control[] CrearControlesInformacion()
        {
            var controles = new System.Collections.Generic.List<Control>();
            int y = 70;

            
            void AgregarFila(string etiqueta, string valor, bool esMultilinea = false)
            {
                var lblEtiqueta = new Label();
                lblEtiqueta.Text = etiqueta + ":";
                lblEtiqueta.Location = new Point(20, y);
                lblEtiqueta.AutoSize = true;
                lblEtiqueta.Font = new Font("Arial", 9, FontStyle.Bold);
                controles.Add(lblEtiqueta);

                if (esMultilinea)
                {
                    var txtValor = new TextBox();
                    txtValor.Location = new Point(150, y);
                    txtValor.Size = new Size(500, 80);
                    txtValor.Multiline = true;
                    txtValor.ScrollBars = ScrollBars.Vertical;
                    txtValor.Text = valor ?? "";
                    txtValor.ReadOnly = true;
                    txtValor.BackColor = Color.White;
                    txtValor.BorderStyle = BorderStyle.FixedSingle;
                    txtValor.Font = new Font("Arial", 9);
                    controles.Add(txtValor);
                    y += 90;
                }
                else
                {
                    var lblValor = new Label();
                    lblValor.Text = valor ?? "";
                    lblValor.Location = new Point(150, y);
                    lblValor.AutoSize = true;
                    lblValor.Font = new Font("Arial", 9);
                    controles.Add(lblValor);
                    y += 30;
                }
            }

           
            AgregarFila("Título", _ticket.Titulo);
            AgregarFila("Cliente", _ticket.ClienteNombre);
            AgregarFila("Sistema", _ticket.SistemaNombre);
            AgregarFila("Tipo", _ticket.TipoTicket);
            AgregarFila("Urgencia", _ticket.NivelUrgencia);
            AgregarFila("Estado", _ticket.Estado);
            AgregarFila("Asignado a", _ticket.UsuarioAsignadoNombre);
            AgregarFila("Fecha Creación", _ticket.FechaCreacion.ToString("dd/MM/yyyy HH:mm"));
            AgregarFila("Tiempo Estimado", $"{_ticket.TiempoAtencion} minutos");

            if (!string.IsNullOrEmpty(_ticket.DerivadoA))
            {
                AgregarFila("Derivado a", _ticket.DerivadoA);
                AgregarFila("Fecha Derivación", _ticket.FechaDerivacion?.ToString("dd/MM/yyyy HH:mm") ?? "N/A");
            }

            if (!string.IsNullOrEmpty(_ticket.Solucion))
            {
                AgregarFila("Solución", _ticket.Solucion, true);
                AgregarFila("Fecha Solución", _ticket.FechaSolucion?.ToString("dd/MM/yyyy HH:mm") ?? "N/A");
            }

            AgregarFila("Descripción", _ticket.Descripcion, true);

            return controles.ToArray();
        }

        private void CargarDatosTicket()
        {
            
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }
    }
}