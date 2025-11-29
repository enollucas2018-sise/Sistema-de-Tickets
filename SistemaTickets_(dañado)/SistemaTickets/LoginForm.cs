using SistemaTickets.Business.Services;
using SistemaTickets.Entities;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTickets.Presentation
{
    public partial class LoginForm : Form
    {
        private LoginService _loginService;

        // Controles
        private PictureBox picLogo;
        private Label lblTitulo;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkRecordar;
        private Button btnLogin;
        private Button btnCancelar;
        private LinkLabel lnkOlvidePassword;
        private Panel panelPrincipal;
        private Label lblSubtitulo;
        private Label lblVersion;

        public LoginForm()
        {
            InitializeComponent();
            _loginService = new LoginService();
            CargarConfiguracion();
            ConfigurarEventos();
        }

        private void InitializeComponent()
        {
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkRecordar = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lnkOlvidePassword = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.panelPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.BackColor = System.Drawing.Color.White;
            this.panelPrincipal.Controls.Add(this.picLogo);
            this.panelPrincipal.Controls.Add(this.lblTitulo);
            this.panelPrincipal.Controls.Add(this.lblSubtitulo);
            this.panelPrincipal.Controls.Add(this.lblEmail);
            this.panelPrincipal.Controls.Add(this.txtEmail);
            this.panelPrincipal.Controls.Add(this.lblPassword);
            this.panelPrincipal.Controls.Add(this.txtPassword);
            this.panelPrincipal.Controls.Add(this.chkRecordar);
            this.panelPrincipal.Controls.Add(this.btnLogin);
            this.panelPrincipal.Controls.Add(this.btnCancelar);
            this.panelPrincipal.Controls.Add(this.lnkOlvidePassword);
            this.panelPrincipal.Controls.Add(this.lblVersion);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(20, 20);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(406, 422);
            this.panelPrincipal.TabIndex = 0;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLogo.Location = new System.Drawing.Point(149, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(80, 80);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitulo.Location = new System.Drawing.Point(77, 108);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(218, 22);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "SISTEMA DE TICKETS";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Arial", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitulo.Location = new System.Drawing.Point(128, 130);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(111, 16);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Soporte Técnico";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Location = new System.Drawing.Point(50, 180);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(41, 15);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Arial", 10F);
            this.txtEmail.Location = new System.Drawing.Point(50, 200);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 23);
            this.txtEmail.TabIndex = 1;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.Location = new System.Drawing.Point(50, 240);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(76, 15);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Arial", 10F);
            this.txtPassword.Location = new System.Drawing.Point(50, 260);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(300, 23);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // chkRecordar
            // 
            this.chkRecordar.AutoSize = true;
            this.chkRecordar.Font = new System.Drawing.Font("Arial", 9F);
            this.chkRecordar.Location = new System.Drawing.Point(50, 295);
            this.chkRecordar.Name = "chkRecordar";
            this.chkRecordar.Size = new System.Drawing.Size(139, 19);
            this.chkRecordar.TabIndex = 5;
            this.chkRecordar.Text = "Recordar mi usuario";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(50, 325);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(140, 35);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Iniciar Sesión";
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gray;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(210, 325);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(140, 35);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // lnkOlvidePassword
            // 
            this.lnkOlvidePassword.AutoSize = true;
            this.lnkOlvidePassword.Font = new System.Drawing.Font("Arial", 8F);
            this.lnkOlvidePassword.Location = new System.Drawing.Point(200, 295);
            this.lnkOlvidePassword.Name = "lnkOlvidePassword";
            this.lnkOlvidePassword.Size = new System.Drawing.Size(134, 14);
            this.lnkOlvidePassword.TabIndex = 6;
            this.lnkOlvidePassword.TabStop = true;
            this.lnkOlvidePassword.Text = "¿Olvidaste tu contraseña?";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 8F);
            this.lblVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblVersion.Location = new System.Drawing.Point(350, 370);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(37, 14);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.Text = "v1.0.0";
            // 
            // LoginForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(446, 462);
            this.Controls.Add(this.panelPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Tickets - Login";
            this.panelPrincipal.ResumeLayout(false);
            this.panelPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        private void ConfigurarEventos()
        {
            // Eventos para txtEmail
            txtEmail.Enter += (s, e) => txtEmail.BackColor = Color.LightYellow;
            txtEmail.Leave += (s, e) => txtEmail.BackColor = Color.White;

            // Eventos para txtPassword
            txtPassword.Enter += (s, e) => txtPassword.BackColor = Color.LightYellow;
            txtPassword.Leave += (s, e) => txtPassword.BackColor = Color.White;
            txtPassword.KeyPress += TxtPassword_KeyPress;

            // Eventos para botones
            btnLogin.Click += BtnLogin_Click;
            btnCancelar.Click += BtnCancelar_Click;

            // Evento para link
            lnkOlvidePassword.Click += LnkOlvidePassword_Click;
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void LnkOlvidePassword_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Contacte al administrador del sistema para recuperar su contraseña.",
                          "Recuperar Contraseña",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarConfiguracion()
        {
            // Cargar email recordado si existe
            try
            {
                var rememberedEmail = ConfigurationManager.AppSettings["RememberedEmail"];
                if (!string.IsNullOrEmpty(rememberedEmail))
                {
                    txtEmail.Text = rememberedEmail;
                    chkRecordar.Checked = true;
                    txtPassword.Focus();
                }
            }
            catch
            {
                // Si hay error cargando la configuración, continuar sin datos recordados
            }
        }

        private void GuardarConfiguracion()
        {
            
            if (chkRecordar.Checked)
            {
                
            }
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            try
            {
               
                if (!ValidarCampos()) return;

                Cursor = Cursors.WaitCursor;
                btnLogin.Enabled = false;
                btnCancelar.Enabled = false;

                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text;

                Usuario usuario = _loginService.ValidarCredenciales(email, password);

                if (usuario != null)
                {
                    GuardarConfiguracion();

                    // Mostrar mensaje de bienvenida según rol
                    
                    string mensajeBienvenida;
                    switch (usuario.Rol)
                    {
                        case "HelpDesk":
                            mensajeBienvenida = "Bienvenido/a - Primer Nivel de Soporte";
                            break;
                        case "SegundaLinea":
                            mensajeBienvenida = "Bienvenido/a - Segundo Nivel de Soporte";
                            break;
                        case "InvestigacionDesarrollo":
                            mensajeBienvenida = "Bienvenido/a - Área de I+D";
                            break;
                        case "Administrador":
                            mensajeBienvenida = "Bienvenido/a - Administrador del Sistema";
                            break;
                        default:
                            mensajeBienvenida = "Bienvenido/a al Sistema";
                            break;
                    }
                    ;

                MessageBox.Show(mensajeBienvenida, "Login Exitoso",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MainForm mainForm = new MainForm(usuario);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email o contraseña incorrectos.\nPor favor verifique sus credenciales.",
                                  "Error de Autenticación",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.SelectAll();
                    txtPassword.Focus();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnLogin.Enabled = true;
                btnCancelar.Enabled = true;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Por favor ingrese su email", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Por favor ingrese su contraseña", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Validación básica de formato email
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Por favor ingrese un email válido", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancelar_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea salir del sistema?", "Confirmar Salida",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}