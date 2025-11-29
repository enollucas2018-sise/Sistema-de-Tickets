using SistemaTickets.Business.Services;
using SistemaTickets.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaTickets.Presentation
{
    public partial class CrearTicketForm : Form
    {
        private TicketService _ticketService;
        private Usuario _usuarioLogueado;
        private List<Cliente> _clientes;
        private List<Sistema> _sistemas;

        // Controles
        private ComboBox cmbClientes;
        private ComboBox cmbSistemas;
        private ComboBox cmbTipoTicket;
        private ComboBox cmbUrgencia;
        private TextBox txtTitulo;
        private TextBox txtDescripcion;
        private NumericUpDown numTiempoAtencion;
        private Button btnCrear;
        private Button btnCancelar;
        private Label lblUrgenciaInfo;

        public CrearTicketForm(Usuario usuario)
        {
            _usuarioLogueado = usuario;
            _ticketService = new TicketService();
            InitializeCustomComponent();
            CargarDatos();
        }

        private void InitializeCustomComponent()
        {
            this.Size = new Size(650, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Crear Nuevo Ticket";
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Panel principal
            var panelPrincipal = new Panel();
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.Padding = new Padding(20);
            panelPrincipal.BackColor = Color.White;

            // Título
            var lblTituloForm = new Label();
            lblTituloForm.Text = "NUEVO TICKET DE SOPORTE";
            lblTituloForm.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTituloForm.ForeColor = Color.FromArgb(0, 102, 204);
            lblTituloForm.AutoSize = true;
            lblTituloForm.Location = new Point(20, 20);

            // Cliente
            var lblCliente = new Label();
            lblCliente.Text = "Cliente:*";
            lblCliente.Location = new Point(20, 70);
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbClientes = new ComboBox();
            cmbClientes.Location = new Point(20, 90);
            cmbClientes.Size = new Size(540, 25);
            cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClientes.Font = new Font("Arial", 10);

            // Sistema
            var lblSistema = new Label();
            lblSistema.Text = "Sistema:*";
            lblSistema.Location = new Point(20, 130);
            lblSistema.AutoSize = true;
            lblSistema.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbSistemas = new ComboBox();
            cmbSistemas.Location = new Point(20, 150);
            cmbSistemas.Size = new Size(540, 25);
            cmbSistemas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSistemas.Font = new Font("Arial", 10);

            // Tipo de Ticket
            var lblTipoTicket = new Label();
            lblTipoTicket.Text = "Tipo de Ticket:*";
            lblTipoTicket.Location = new Point(20, 190);
            lblTipoTicket.AutoSize = true;
            lblTipoTicket.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbTipoTicket = new ComboBox();
            cmbTipoTicket.Location = new Point(20, 210);
            cmbTipoTicket.Size = new Size(540, 25);
            cmbTipoTicket.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoTicket.Font = new Font("Arial", 10);
            cmbTipoTicket.Items.AddRange(new string[] { "Incidencia", "Solicitud de Cambio" });
            cmbTipoTicket.SelectedIndexChanged += CmbTipoTicket_SelectedIndexChanged;

            // Nivel de Urgencia
            var lblUrgencia = new Label();
            lblUrgencia.Text = "Nivel de Urgencia:*";
            lblUrgencia.Location = new Point(20, 250);
            lblUrgencia.AutoSize = true;
            lblUrgencia.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbUrgencia = new ComboBox();
            cmbUrgencia.Location = new Point(20, 270);
            cmbUrgencia.Size = new Size(540, 25);
            cmbUrgencia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUrgencia.Font = new Font("Arial", 10);

            // Info urgencia
            lblUrgenciaInfo = new Label();
            lblUrgenciaInfo.Text = "";
            lblUrgenciaInfo.Location = new Point(20, 300);
            lblUrgenciaInfo.AutoSize = true;
            lblUrgenciaInfo.Font = new Font("Arial", 8);
            lblUrgenciaInfo.ForeColor = Color.Blue;

            // Título del ticket
            var lblTituloTicket = new Label();
            lblTituloTicket.Text = "Título:*";
            lblTituloTicket.Location = new Point(20, 330);
            lblTituloTicket.AutoSize = true;
            lblTituloTicket.Font = new Font("Arial", 9, FontStyle.Bold);

            txtTitulo = new TextBox();
            txtTitulo.Location = new Point(20, 350);
            txtTitulo.Size = new Size(540, 25);
            txtTitulo.Font = new Font("Arial", 10);
            

            // Descripción
            var lblDescripcion = new Label();
            lblDescripcion.Text = "Descripción Detallada:*";
            lblDescripcion.Location = new Point(20, 390);
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Arial", 9, FontStyle.Bold);

            txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(20, 410);
            txtDescripcion.Size = new Size(540, 80);
            txtDescripcion.Multiline = true;
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Font = new Font("Arial", 10);
            

            // Tiempo de atención
            var lblTiempo = new Label();
            lblTiempo.Text = "Tiempo Estimado de Atención (minutos):";
            lblTiempo.Location = new Point(20, 500);
            lblTiempo.AutoSize = true;
            lblTiempo.Font = new Font("Arial", 9, FontStyle.Bold);

            numTiempoAtencion = new NumericUpDown();
            numTiempoAtencion.Location = new Point(250, 500);
            numTiempoAtencion.Size = new Size(100, 25);
            numTiempoAtencion.Minimum = 0;
            numTiempoAtencion.Maximum = 480; // 8 horas
            numTiempoAtencion.Value = 60;

            // Botones
            btnCrear = new Button();
            btnCrear.Text = "Crear Ticket";
            btnCrear.Location = new Point(350, 500);
            btnCrear.Size = new Size(100, 30);
            btnCrear.BackColor = Color.FromArgb(0, 102, 204);
            btnCrear.ForeColor = Color.White;
            btnCrear.FlatStyle = FlatStyle.Flat;
            btnCrear.Font = new Font("Arial", 10, FontStyle.Bold);
            btnCrear.Click += BtnCrear_Click;

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(460, 500);
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Arial", 10);
            btnCancelar.Click += BtnCancelar_Click;

            // Agregar controles al panel
            panelPrincipal.Controls.Add(lblTituloForm);
            panelPrincipal.Controls.Add(lblCliente);
            panelPrincipal.Controls.Add(cmbClientes);
            panelPrincipal.Controls.Add(lblSistema);
            panelPrincipal.Controls.Add(cmbSistemas);
            panelPrincipal.Controls.Add(lblTipoTicket);
            panelPrincipal.Controls.Add(cmbTipoTicket);
            panelPrincipal.Controls.Add(lblUrgencia);
            panelPrincipal.Controls.Add(cmbUrgencia);
            panelPrincipal.Controls.Add(lblUrgenciaInfo);
            panelPrincipal.Controls.Add(lblTituloTicket);
            panelPrincipal.Controls.Add(txtTitulo);
            panelPrincipal.Controls.Add(lblDescripcion);
            panelPrincipal.Controls.Add(txtDescripcion);
            panelPrincipal.Controls.Add(lblTiempo);
            panelPrincipal.Controls.Add(numTiempoAtencion);
            panelPrincipal.Controls.Add(btnCrear);
            panelPrincipal.Controls.Add(btnCancelar);

            this.Controls.Add(panelPrincipal);

            // Configurar valores iniciales
            cmbTipoTicket.SelectedIndex = 0;
        }

        private void CargarDatos()
        {
            try
            {
                _clientes = _ticketService.ObtenerClientes();
                _sistemas = _ticketService.ObtenerSistemas();

                cmbClientes.DisplayMember = "Nombre";
                cmbClientes.ValueMember = "ClienteId";
                cmbClientes.DataSource = _clientes;

                cmbSistemas.DisplayMember = "Nombre";
                cmbSistemas.ValueMember = "SistemaId";
                cmbSistemas.DataSource = _sistemas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbTipoTicket_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUrgencia.Items.Clear();

            if (cmbTipoTicket.SelectedItem?.ToString() == "Incidencia")
            {
                cmbUrgencia.Items.AddRange(new string[] { "Bajo", "Medio", "Alto", "Stopper" });
                lblUrgenciaInfo.Text = "Para Incidencias: Bajo, Medio, Alto, Stopper";
            }
            else if (cmbTipoTicket.SelectedItem?.ToString() == "Solicitud de Cambio")
            {
                cmbUrgencia.Items.AddRange(new string[] { "Bajo", "Urgente" });
                lblUrgenciaInfo.Text = "Para Solicitudes de Cambio: Bajo, Urgente";
            }

            if (cmbUrgencia.Items.Count > 0)
                cmbUrgencia.SelectedIndex = 0;
        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;

                var ticket = new Ticket
                {
                    ClienteId = (int)cmbClientes.SelectedValue,
                    SistemaId = (int)cmbSistemas.SelectedValue,
                    UsuarioAsignadoId = _usuarioLogueado.UsuarioId,
                    TipoTicket = cmbTipoTicket.SelectedItem.ToString(),
                    NivelUrgencia = cmbUrgencia.SelectedItem.ToString(),
                    Titulo = txtTitulo.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    TiempoAtencion = (int)numTiempoAtencion.Value,
                    Estado = "Abierto"
                };

                int ticketId = _ticketService.CrearTicket(ticket);

                MessageBox.Show($"✅ Ticket creado exitosamente!\nNúmero de Ticket: TICKET-{ticketId:D6}",
                              "Ticket Creado",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear ticket: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (cmbClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbClientes.Focus();
                return false;
            }

            if (cmbSistemas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un sistema", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSistemas.Focus();
                return false;
            }

            if (cmbTipoTicket.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el tipo de ticket", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoTicket.Focus();
                return false;
            }

            if (cmbUrgencia.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el nivel de urgencia", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUrgencia.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("El título es requerido", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción es requerida", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}