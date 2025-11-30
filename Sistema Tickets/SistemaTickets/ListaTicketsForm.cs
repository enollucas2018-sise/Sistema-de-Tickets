using SistemaTickets.Business.Services;
using SistemaTickets.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaTickets.Presentation
{
    public partial class ListaTicketsForm : Form
    {
        private TicketService _ticketService;
        private Usuario _usuarioLogueado;
        private List<Ticket> _tickets;

       
        private DataGridView dgvTickets;
        private TextBox txtBuscar;
        private ComboBox cmbFiltroEstado;
        private Button btnVerDetalle;
        private Button btnEditar;
        private Button btnDerivar;
        private Button btnSolucionar;
        private Button btnActualizar;

        public ListaTicketsForm(Usuario usuario)
        {
            _usuarioLogueado = usuario;
            _ticketService = new TicketService();
            InitializeCustomComponent();
            CargarTickets(); 
        }

        private void InitializeCustomComponent()
        {
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestión de Tickets - Todos los Tickets";
            this.BackColor = Color.White;

            
            var panelPrincipal = new Panel();
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.Padding = new Padding(20);
            panelPrincipal.BackColor = Color.White;

            
            var lblTitulo = new Label();
            lblTitulo.Text = "GESTIÓN DE TICKETS";
            lblTitulo.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0, 102, 204);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);

            
            var panelBusqueda = new Panel();
            panelBusqueda.Location = new Point(20, 60);
            panelBusqueda.Size = new Size(940, 40);
            panelBusqueda.BackColor = Color.FromArgb(240, 240, 240);
            panelBusqueda.Padding = new Padding(10);

            var lblBuscar = new Label();
            lblBuscar.Text = "Buscar:";
            lblBuscar.Location = new Point(10, 10);
            lblBuscar.AutoSize = true;

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(60, 7);
            txtBuscar.Size = new Size(200, 25);
            txtBuscar.Font = new Font("Arial", 10);
            txtBuscar.TextChanged += TxtBuscar_TextChanged;

            var lblFiltro = new Label();
            lblFiltro.Text = "Estado:";
            lblFiltro.Location = new Point(280, 10);
            lblFiltro.AutoSize = true;

            cmbFiltroEstado = new ComboBox();
            cmbFiltroEstado.Location = new Point(330, 7);
            cmbFiltroEstado.Size = new Size(150, 25);
            cmbFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroEstado.Font = new Font("Arial", 10);
            cmbFiltroEstado.Items.AddRange(new string[] { "Todos", "Abierto", "EnProceso", "Derivado", "Solucionado", "Cerrado" });
            cmbFiltroEstado.SelectedIndex = 0;
            cmbFiltroEstado.SelectedIndexChanged += CmbFiltroEstado_SelectedIndexChanged;

            btnActualizar = new Button();
            btnActualizar.Text = "Actualizar";
            btnActualizar.Location = new Point(500, 7);
            btnActualizar.Size = new Size(100, 25);
            btnActualizar.BackColor = Color.FromArgb(0, 102, 204);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Arial", 9);
            btnActualizar.Click += BtnActualizar_Click;

            panelBusqueda.Controls.AddRange(new Control[] { lblBuscar, txtBuscar, lblFiltro, cmbFiltroEstado, btnActualizar });

            
            dgvTickets = new DataGridView();
            dgvTickets.Location = new Point(20, 110);
            dgvTickets.Size = new Size(940, 350);
            dgvTickets.BackgroundColor = Color.White;
            dgvTickets.BorderStyle = BorderStyle.Fixed3D;
            dgvTickets.Font = new Font("Arial", 9);
            dgvTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTickets.MultiSelect = false;
            dgvTickets.ReadOnly = true;
            dgvTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTickets.RowHeadersVisible = false;

           
            dgvTickets.Columns.Add("NumeroTicket", "N° Ticket");
            dgvTickets.Columns.Add("Titulo", "Título");
            dgvTickets.Columns.Add("ClienteNombre", "Cliente");
            dgvTickets.Columns.Add("SistemaNombre", "Sistema");
            dgvTickets.Columns.Add("TipoTicket", "Tipo");
            dgvTickets.Columns.Add("Estado", "Estado");
            dgvTickets.Columns.Add("NivelUrgencia", "Urgencia");
            dgvTickets.Columns.Add("FechaCreacion", "Fecha Creación");
            dgvTickets.Columns.Add("UsuarioAsignadoNombre", "Asignado a");

           
            dgvTickets.Columns["NumeroTicket"].Width = 100;
            dgvTickets.Columns["Estado"].Width = 80;
            dgvTickets.Columns["TipoTicket"].Width = 120;
            dgvTickets.Columns["NivelUrgencia"].Width = 80;
            dgvTickets.Columns["FechaCreacion"].Width = 120;

            
            var panelBotones = new Panel();
            panelBotones.Location = new Point(20, 470);
            panelBotones.Size = new Size(940, 50);

            btnVerDetalle = new Button();
            btnVerDetalle.Text = "Ver Detalle";
            btnVerDetalle.Location = new Point(0, 10);
            btnVerDetalle.Size = new Size(100, 30);
            btnVerDetalle.BackColor = Color.FromArgb(0, 102, 204);
            btnVerDetalle.ForeColor = Color.White;
            btnVerDetalle.FlatStyle = FlatStyle.Flat;
            btnVerDetalle.Font = new Font("Arial", 9, FontStyle.Bold);
            btnVerDetalle.Click += BtnVerDetalle_Click;

            btnEditar = new Button();
            btnEditar.Text = "Editar";
            btnEditar.Location = new Point(110, 10);
            btnEditar.Size = new Size(100, 30);
            btnEditar.BackColor = Color.FromArgb(255, 153, 0);
            btnEditar.ForeColor = Color.White;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Arial", 9);
            btnEditar.Click += BtnEditar_Click;

            btnDerivar = new Button();
            btnDerivar.Text = "Derivar";
            btnDerivar.Location = new Point(220, 10);
            btnDerivar.Size = new Size(100, 30);
            btnDerivar.BackColor = Color.FromArgb(153, 0, 204);
            btnDerivar.ForeColor = Color.White;
            btnDerivar.FlatStyle = FlatStyle.Flat;
            btnDerivar.Font = new Font("Arial", 9);
            btnDerivar.Click += BtnDerivar_Click;

            btnSolucionar = new Button();
            btnSolucionar.Text = "Solucionar";
            btnSolucionar.Location = new Point(330, 10);
            btnSolucionar.Size = new Size(100, 30);
            btnSolucionar.BackColor = Color.FromArgb(0, 153, 0);
            btnSolucionar.ForeColor = Color.White;
            btnSolucionar.FlatStyle = FlatStyle.Flat;
            btnSolucionar.Font = new Font("Arial", 9);
            btnSolucionar.Click += BtnSolucionar_Click;

            var btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Location = new Point(840, 10);
            btnCerrar.Size = new Size(100, 30);
            btnCerrar.BackColor = Color.Gray;
            btnCerrar.ForeColor = Color.White;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Arial", 9);
            btnCerrar.Click += (s, e) => this.Close();

            panelBotones.Controls.AddRange(new Control[] {
                btnVerDetalle, btnEditar, btnDerivar, btnSolucionar, btnCerrar
            });

            // Agregar controles al panel principal
            panelPrincipal.Controls.Add(lblTitulo);
            panelPrincipal.Controls.Add(panelBusqueda);
            panelPrincipal.Controls.Add(dgvTickets);
            panelPrincipal.Controls.Add(panelBotones);

            this.Controls.Add(panelPrincipal);

            // Evento doble click en grid
            dgvTickets.CellDoubleClick += (s, e) => BtnVerDetalle_Click(s, e);
        }

       

        private void CargarTickets()
        {
            try
            {
                _tickets = _ticketService.ObtenerTodosTickets();
                ActualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tickets: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarGrid()
        {
            dgvTickets.Rows.Clear();

            var ticketsFiltrados = _tickets;

            
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                var busqueda = txtBuscar.Text.ToLower();
                ticketsFiltrados = ticketsFiltrados.Where(t =>
                    t.NumeroTicket.ToLower().Contains(busqueda) ||
                    t.Titulo.ToLower().Contains(busqueda) ||
                    t.ClienteNombre.ToLower().Contains(busqueda) ||
                    t.SistemaNombre.ToLower().Contains(busqueda)
                ).ToList();
            }

            // Aplicar filtro de estado
            if (cmbFiltroEstado.SelectedItem?.ToString() != "Todos")
            {
                ticketsFiltrados = ticketsFiltrados.Where(t =>
                    t.Estado == cmbFiltroEstado.SelectedItem.ToString()
                ).ToList();
            }

            foreach (var ticket in ticketsFiltrados)
            {
                dgvTickets.Rows.Add(
                    ticket.NumeroTicket,
                    ticket.Titulo,
                    ticket.ClienteNombre,
                    ticket.SistemaNombre,
                    ticket.TipoTicket,
                    ticket.Estado,
                    ticket.NivelUrgencia,
                    ticket.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
                    ticket.UsuarioAsignadoNombre
                );
            }

            // Colorear filas según estado
            foreach (DataGridViewRow row in dgvTickets.Rows)
            {
                var estado = row.Cells["Estado"].Value?.ToString();
                switch (estado)
                {
                    case "Abierto":
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                        break;
                    case "EnProceso":
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        break;
                    case "Derivado":
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                        break;
                    case "Solucionado":
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        break;
                    case "Cerrado":
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        break;
                }
            }
        }

        private Ticket ObtenerTicketSeleccionado()
        {
            if (dgvTickets.SelectedRows.Count == 0)
                return null;

            var numeroTicket = dgvTickets.SelectedRows[0].Cells["NumeroTicket"].Value.ToString();
            return _tickets.FirstOrDefault(t => t.NumeroTicket == numeroTicket);
        }

      

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            ActualizarGrid();
        }

        private void CmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarGrid();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            CargarTickets();
            MessageBox.Show("Lista de tickets actualizada", "Actualizado",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnVerDetalle_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();
            if (ticket == null)
            {
                MessageBox.Show("Por favor seleccione un ticket", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var detalleForm = new DetalleTicketForm(ticket, _usuarioLogueado);
            detalleForm.ShowDialog();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();
            if (ticket == null)
            {
                MessageBox.Show("Por favor seleccione un ticket", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Funcionalidad: Editar Ticket\n(Formulario en desarrollo)",
                          "Editar Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDerivar_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();
            if (ticket == null)
            {
                MessageBox.Show("Por favor seleccione un ticket", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Funcionalidad: Derivar Ticket\n(Formulario en desarrollo)",
                          "Derivar Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSolucionar_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();
            if (ticket == null)
            {
                MessageBox.Show("Por favor seleccione un ticket", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Funcionalidad: Solucionar Ticket\n(Formulario en desarrollo)",
                          "Solucionar Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
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