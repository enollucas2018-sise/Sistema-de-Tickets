using SistemaTickets.Entities;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTickets.Presentation
{
    public partial class MainForm : Form
    {
        private Usuario _usuarioLogueado;

        
        private MenuStrip menuPrincipal;
        private StatusStrip statusBar;
        private ToolStrip toolStrip;
        private Panel panelContenido;
        private Label lblTituloPrincipal;

        
        private ToolStripMenuItem menuTickets;
        private ToolStripMenuItem menuAreaConocimiento;
        private ToolStripMenuItem menuReportes;
        private ToolStripMenuItem menuConfiguracion;
        private ToolStripMenuItem menuAyuda;

        public MainForm(Usuario usuario)
        {
            InitializeCustomComponent();
            _usuarioLogueado = usuario;
            ConfigurarInterfazSegunRol();
            ActualizarBarraEstado();
            MostrarBienvenida();
        }

        private void InitializeCustomComponent()
        {
            
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Sistema de Tickets de Soporte Técnico";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;

            
            menuPrincipal = new MenuStrip();
            menuPrincipal.BackColor = Color.FromArgb(0, 102, 204);
            menuPrincipal.ForeColor = Color.White;
            menuPrincipal.Font = new Font("Arial", 10);
            this.MainMenuStrip = menuPrincipal;

            
            menuTickets = new ToolStripMenuItem("Tickets");
            menuTickets.ForeColor = Color.White;

            var subMenuNuevoTicket = new ToolStripMenuItem("Nuevo Ticket");
            var subMenuVerTickets = new ToolStripMenuItem("Ver Todos los Tickets");
            var subMenuMisTickets = new ToolStripMenuItem("Mis Tickets Asignados");
            var separator1 = new ToolStripSeparator();
            var subMenuBuscarTicket = new ToolStripMenuItem("Buscar Ticket");
            var separator2 = new ToolStripSeparator(); // Nuevo separador
            var subMenuSalir = new ToolStripMenuItem("Salir"); // Nueva opción Salir

            menuTickets.DropDownItems.AddRange(new ToolStripItem[] {
                subMenuNuevoTicket,
                subMenuVerTickets,
                subMenuMisTickets,
                separator1,
                subMenuBuscarTicket,
                separator2,
                subMenuSalir
            });

            
            menuAreaConocimiento = new ToolStripMenuItem("Área de Conocimiento");
            menuAreaConocimiento.ForeColor = Color.White;

            
            menuReportes = new ToolStripMenuItem("Reportes");
            menuReportes.ForeColor = Color.White;
            var subMenuReporteTickets = new ToolStripMenuItem("Reporte de Tickets");
            var subMenuMetricas = new ToolStripMenuItem("Métricas de Soporte");
            menuReportes.DropDownItems.AddRange(new ToolStripItem[] { subMenuReporteTickets, subMenuMetricas });

            
            menuConfiguracion = new ToolStripMenuItem("Configuración");
            menuConfiguracion.ForeColor = Color.White;
            var subMenuClientes = new ToolStripMenuItem("Gestión de Clientes");
            var subMenuSistemas = new ToolStripMenuItem("Gestión de Sistemas");
            var subMenuUsuarios = new ToolStripMenuItem("Gestión de Usuarios");
            menuConfiguracion.DropDownItems.AddRange(new ToolStripItem[] { subMenuClientes, subMenuSistemas, subMenuUsuarios });

            
            menuAyuda = new ToolStripMenuItem("Ayuda");
            menuAyuda.ForeColor = Color.White;
            var subMenuAcercaDe = new ToolStripMenuItem("Acerca de...");
            var subMenuManual = new ToolStripMenuItem("Manual de Usuario");
            menuAyuda.DropDownItems.AddRange(new ToolStripItem[] { subMenuAcercaDe, subMenuManual });

            
            menuPrincipal.Items.AddRange(new ToolStripItem[] {
                menuTickets,
                menuAreaConocimiento,
                menuReportes,
                menuConfiguracion,
                menuAyuda
            });

            
            toolStrip = new ToolStrip();
            toolStrip.BackColor = Color.FromArgb(240, 240, 240);
            toolStrip.Height = 50;
            toolStrip.Padding = new Padding(5);

            
            var btnNuevoTicket = new ToolStripButton();
            btnNuevoTicket.Text = "Nuevo Ticket";
            btnNuevoTicket.Font = new Font("Arial", 9, FontStyle.Bold);
            btnNuevoTicket.BackColor = Color.FromArgb(0, 102, 204);
            btnNuevoTicket.ForeColor = Color.White;
            btnNuevoTicket.Padding = new Padding(10, 5, 10, 5);

            var btnAreaConocimiento = new ToolStripButton();
            btnAreaConocimiento.Text = "Área Conocimiento";
            btnAreaConocimiento.Font = new Font("Arial", 9);
            btnAreaConocimiento.Padding = new Padding(10, 5, 10, 5);

            var btnReportes = new ToolStripButton();
            btnReportes.Text = "Reportes";
            btnReportes.Font = new Font("Arial", 9);
            btnReportes.Padding = new Padding(10, 5, 10, 5);

            
            var separator = new ToolStripSeparator();
            separator.Padding = new Padding(10, 0, 10, 0);

            toolStrip.Items.AddRange(new ToolStripItem[] {
                btnNuevoTicket,
                separator,
                btnAreaConocimiento,
                btnReportes
            });

            
            statusBar = new StatusStrip();
            statusBar.BackColor = Color.FromArgb(0, 102, 204);
            statusBar.ForeColor = Color.White;
            statusBar.Font = new Font("Arial", 9);

            var lblUsuario = new ToolStripStatusLabel();
            lblUsuario.ForeColor = Color.White;
            var lblRol = new ToolStripStatusLabel();
            lblRol.ForeColor = Color.White;
            var lblFecha = new ToolStripStatusLabel();
            lblFecha.ForeColor = Color.White;
            var lblHora = new ToolStripStatusLabel();
            lblHora.ForeColor = Color.White;

            
            statusBar.Items.AddRange(new ToolStripItem[] {
                lblUsuario,
                new ToolStripStatusLabel { Text = " | ", ForeColor = Color.White },
                lblRol,
                new ToolStripStatusLabel { Text = " | ", ForeColor = Color.White },
                lblFecha,
                new ToolStripStatusLabel { Text = " | ", ForeColor = Color.White },
                lblHora
            });

            
            panelContenido = new Panel();
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.BackColor = Color.White;
            panelContenido.Padding = new Padding(20);

            
            lblTituloPrincipal = new Label();
            lblTituloPrincipal.Text = "SISTEMA DE TICKETS - SOPORTE TÉCNICO";
            lblTituloPrincipal.Font = new Font("Arial", 24, FontStyle.Bold);
            lblTituloPrincipal.ForeColor = Color.FromArgb(0, 102, 204);
            lblTituloPrincipal.AutoSize = true;
            lblTituloPrincipal.Location = new Point(250, 200);

            
            this.Controls.AddRange(new Control[] { panelContenido, statusBar, toolStrip, menuPrincipal });
            panelContenido.Controls.Add(lblTituloPrincipal);

            
            ConfigurarEventosMenu(subMenuNuevoTicket, subMenuVerTickets, subMenuMisTickets,
                                subMenuBuscarTicket, subMenuSalir, subMenuReporteTickets, subMenuMetricas,
                                subMenuClientes, subMenuSistemas, subMenuUsuarios,
                                subMenuAcercaDe, subMenuManual,
                                btnNuevoTicket, btnAreaConocimiento, btnReportes);
        }

        private void ConfigurarEventosMenu(ToolStripMenuItem subMenuNuevoTicket,
                                         ToolStripMenuItem subMenuVerTickets,
                                         ToolStripMenuItem subMenuMisTickets,
                                         ToolStripMenuItem subMenuBuscarTicket,
                                         ToolStripMenuItem subMenuSalir, // Nuevo parámetro
                                         ToolStripMenuItem subMenuReporteTickets,
                                         ToolStripMenuItem subMenuMetricas,
                                         ToolStripMenuItem subMenuClientes,
                                         ToolStripMenuItem subMenuSistemas,
                                         ToolStripMenuItem subMenuUsuarios,
                                         ToolStripMenuItem subMenuAcercaDe,
                                         ToolStripMenuItem subMenuManual,
                                         ToolStripButton btnNuevoTicket,
                                         ToolStripButton btnAreaConocimiento,
                                         ToolStripButton btnReportes)
        {
            
            subMenuNuevoTicket.Click += (s, e) => AbrirCrearTicket();
            subMenuVerTickets.Click += (s, e) => AbrirListaTickets();
            subMenuMisTickets.Click += (s, e) => AbrirMisTickets();
            subMenuBuscarTicket.Click += (s, e) => BuscarTicket();
            subMenuSalir.Click += (s, e) => SalirSistema(); // Nuevo evento

            
            subMenuReporteTickets.Click += (s, e) => AbrirReportes();
            subMenuMetricas.Click += (s, e) => AbrirMetricas();

            
            subMenuClientes.Click += (s, e) => AbrirGestionClientes();
            subMenuSistemas.Click += (s, e) => AbrirGestionSistemas();
            subMenuUsuarios.Click += (s, e) => AbrirGestionUsuarios();

            
            subMenuAcercaDe.Click += (s, e) => MostrarAcercaDe();
            subMenuManual.Click += (s, e) => AbrirManual();

            
            btnNuevoTicket.Click += (s, e) => AbrirCrearTicket();
            btnAreaConocimiento.Click += (s, e) => AbrirAreaConocimiento();
            btnReportes.Click += (s, e) => AbrirReportes();

            
            menuAreaConocimiento.Click += (s, e) => AbrirAreaConocimiento();
        }

        private void ConfigurarInterfazSegunRol()
        {
            
            switch (_usuarioLogueado.Rol)
            {
                case "HelpDesk":
                    menuConfiguracion.Visible = false;
                    toolStrip.Visible = true;
                    break;

                case "SegundaLinea":
                    menuConfiguracion.Visible = false;
                    toolStrip.Visible = true;
                    break;

                case "InvestigacionDesarrollo":
                    menuReportes.Visible = false;
                    toolStrip.Visible = true;
                    break;

                case "Administrador":
                    
                    toolStrip.Visible = true;
                    break;

                default:
                    toolStrip.Visible = false;
                    break;
            }

            
            this.Text = $"Sistema de Tickets - {_usuarioLogueado.Nombre_Usuario} ({_usuarioLogueado.Rol})";
        }

        private void ActualizarBarraEstado()
        {
            if (statusBar.Items.Count >= 7)
            {
                statusBar.Items[0].Text = $"Usuario: {_usuarioLogueado.Nombre_Usuario}";
                statusBar.Items[2].Text = $"Rol: {_usuarioLogueado.Rol}";
                statusBar.Items[4].Text = $"Fecha: {System.DateTime.Now:dd/MM/yyyy}";
                statusBar.Items[6].Text = $"Hora: {System.DateTime.Now:HH:mm:ss}";
            }

            
            var timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) => {
                if (statusBar.Items.Count >= 7)
                    statusBar.Items[6].Text = $"Hora: {System.DateTime.Now:HH:mm:ss}";
            };
            timer.Start();
        }

        private void MostrarBienvenida()
        {
            lblTituloPrincipal.Visible = true;
            lblTituloPrincipal.Text = $"BIENVENIDO/A {_usuarioLogueado.Nombre_Usuario.ToUpper()}\nSISTEMA DE TICKETS - {_usuarioLogueado.Rol.ToUpper()}";
            lblTituloPrincipal.TextAlign = ContentAlignment.MiddleCenter;
            lblTituloPrincipal.AutoSize = false;
            lblTituloPrincipal.Dock = DockStyle.Fill;
            lblTituloPrincipal.TextAlign = ContentAlignment.MiddleCenter;
            lblTituloPrincipal.Font = new Font("Arial", 20, FontStyle.Bold);
        }

        

        private void AbrirCrearTicket()
        {
            CrearTicketForm crearTicketForm = new CrearTicketForm(_usuarioLogueado);
            var result = crearTicketForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                MessageBox.Show("El ticket se creó correctamente", "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AbrirAreaConocimiento()
        {
            MessageBox.Show("Funcionalidad: Área de Conocimiento\n(Formulario en desarrollo)",
                          "Área de Conocimiento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AbrirListaTickets()
        {
            ListaTicketsForm listaForm = new ListaTicketsForm(_usuarioLogueado);
            listaForm.ShowDialog();
        }

        private void AbrirMisTickets()
        {
            var misTicketsForm = new ListaTicketsForm(_usuarioLogueado);
            misTicketsForm.Text = "Mis Tickets Asignados";
            misTicketsForm.ShowDialog();
        }

        private void BuscarTicket()
        {
            MessageBox.Show("Funcionalidad: Buscar Ticket\n(Formulario en desarrollo)",
                          "Buscar Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        private void SalirSistema()
        {
            if (MessageBox.Show("¿Está seguro que desea salir del sistema?",
                              "Confirmar Salida",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void AbrirReportes()
        {
            MessageBox.Show("Funcionalidad: Reportes\n(Formulario en desarrollo)",
                          "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AbrirMetricas()
        {
            MessageBox.Show("Funcionalidad: Métricas\n(Formulario en desarrollo)",
                          "Métricas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AbrirGestionClientes()
        {
            MessageBox.Show("Funcionalidad: Gestión de Clientes\n(Formulario en desarrollo)",
                          "Gestión de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AbrirGestionSistemas()
        {
            MessageBox.Show("Funcionalidad: Gestión de Sistemas\n(Formulario en desarrollo)",
                          "Gestión de Sistemas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AbrirGestionUsuarios()
        {
            MessageBox.Show("Funcionalidad: Gestión de Usuarios\n(Formulario en desarrollo)",
                          "Gestión de Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarAcercaDe()
        {
            MessageBox.Show("Sistema de Tickets v1.0\n\n" +
                          "Desarrollado para Gestión de Soporte Técnico\n\n" +
                          "Módulos:\n" +
                          "- Gestión de Tickets\n" +
                          "- Área de Conocimiento\n" +
                          "- Reportes y Métricas\n" +
                          "- Configuración del Sistema\n\n" +
                          "© 2024",
                          "Acerca del Sistema",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AbrirManual()
        {
            MessageBox.Show("El manual de usuario está en desarrollo.\n\n" +
                          "Para asistencia técnica contacte al administrador del sistema.",
                          "Manual de Usuario",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarFormularioEnPanel(Form form)
        {
            // Limpiar panel actual
            panelContenido.Controls.Clear();
            lblTituloPrincipal.Visible = false;

            // Configurar el formulario hijo
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Agregar al panel
            panelContenido.Controls.Add(form);
            form.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("¿Está seguro que desea salir del sistema?", "Confirmar Salida",
                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}