using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuickVentas
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            // Configurar ventana principal
            this.Text = "QuickVentas - Sistema de Gestión";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(900, 700); 
            this.BackColor = Color.FromArgb(240, 240, 245);
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // Limpiar controles existentes del diseñador
            this.Controls.Clear();

            // Crear controles
            CrearControlesPrincipales();
        }

        private void CrearControlesPrincipales()
        {
            // Panel de encabezado
            Panel panelHeader = new Panel();
            panelHeader.BackColor = Color.FromArgb(30, 136, 229); 
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 100; 
            this.Controls.Add(panelHeader);

            // Título principal 
            Label lblTitulo = new Label();
            lblTitulo.Text = "QUICKVENTAS";
            lblTitulo.Font = new Font("Segoe UI", 26, FontStyle.Bold); 
            lblTitulo.ForeColor = Color.White;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(30, 25);
            panelHeader.Controls.Add(lblTitulo);

            // Subtítulo 
            Label lblSubtitulo = new Label();
            lblSubtitulo.Text = "Sistema de Gestión para Pequeños Negocios";
            lblSubtitulo.Font = new Font("Segoe UI", 11, FontStyle.Italic); 
            lblSubtitulo.ForeColor = Color.White;
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(32, 70);
            panelHeader.Controls.Add(lblSubtitulo);

            // Panel de botones 
            Panel panelBotones = new Panel();
            panelBotones.BackColor = Color.White;
            panelBotones.Size = new Size(800, 400);
            panelBotones.Location = new Point(50, 140); 
            this.Controls.Add(panelBotones);

            // botones con iconos 
            CrearBotonMenu(panelBotones, 50, 30, "📦", "PRODUCTOS", "Gestionar inventario", Color.FromArgb(76, 175, 80), btnProductos_Click);
            CrearBotonMenu(panelBotones, 300, 30, "👥", "CLIENTES", "Administrar clientes", Color.FromArgb(156, 39, 176), btnClientes_Click);
            CrearBotonMenu(panelBotones, 550, 30, "💰", "VENTAS", "Registrar ventas", Color.FromArgb(255, 152, 0), btnVentas_Click);
            CrearBotonMenu(panelBotones, 50, 180, "📊", "REPORTES", "Ver estadísticas", Color.FromArgb(33, 150, 243), btnReportes_Click);
            CrearBotonMenu(panelBotones, 300, 180, "ℹ️", "ACERCA DE", "Información del sistema", Color.FromArgb(100, 100, 100), btnAcercaDe_Click);
            CrearBotonMenu(panelBotones, 550, 180, "❌", "SALIR", "Cerrar aplicación", Color.FromArgb(244, 67, 54), btnSalir_Click);

            // Footer 
            Label lblFooter = new Label();
            lblFooter.Text = "© 2025 QuickVentas - Proyecto Final Desarrollo de Software IV";
            lblFooter.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblFooter.ForeColor = Color.Gray;
            lblFooter.AutoSize = true;
            lblFooter.Location = new Point(250, 580); 
            this.Controls.Add(lblFooter);
        }

        private void CrearBotonMenu(Panel panel, int x, int y, string icono, string texto, string subtitulo, Color color, EventHandler clickHandler)
        {
            // Panel del botón 
            Panel panelBoton = new Panel();
            panelBoton.Size = new Size(200, 130); 
            panelBoton.Location = new Point(x, y);
            panelBoton.BackColor = Color.White;
            panelBoton.BorderStyle = BorderStyle.FixedSingle;
            panelBoton.Cursor = Cursors.Hand;
            panel.Controls.Add(panelBoton);

            // Efecto hover 
            panelBoton.MouseEnter += (sender, e) =>
            {
                panelBoton.BackColor = Color.FromArgb(245, 245, 245);
                
                panelBoton.BorderStyle = BorderStyle.Fixed3D;
            };
            panelBoton.MouseLeave += (sender, e) =>
            {
                panelBoton.BackColor = Color.White;
                panelBoton.BorderStyle = BorderStyle.FixedSingle;
            };

            // Icono 
            Label lblIcono = new Label();
            lblIcono.Text = icono;
            lblIcono.Font = new Font("Segoe UI", 28, FontStyle.Bold); 
            lblIcono.ForeColor = color;
            lblIcono.AutoSize = true;
            lblIcono.Location = new Point(80, 20); 
            lblIcono.TextAlign = ContentAlignment.MiddleCenter;
            panelBoton.Controls.Add(lblIcono);

            // Texto principal 
            Label lblTexto = new Label();
            lblTexto.Text = texto;
            lblTexto.Font = new Font("Segoe UI", 11, FontStyle.Bold); 
            lblTexto.ForeColor = Color.FromArgb(33, 33, 33);
            lblTexto.AutoSize = false; 
            lblTexto.Size = new Size(180, 25); 
            lblTexto.TextAlign = ContentAlignment.MiddleCenter;
            lblTexto.Location = new Point(10, 70); 
            panelBoton.Controls.Add(lblTexto);

            // Subtítulo 
            Label lblSubtitulo = new Label();
            lblSubtitulo.Text = subtitulo;
            lblSubtitulo.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            lblSubtitulo.ForeColor = Color.Gray;
            lblSubtitulo.AutoSize = false; 
            lblSubtitulo.Size = new Size(180, 30); 
            lblSubtitulo.TextAlign = ContentAlignment.TopCenter;
            lblSubtitulo.Location = new Point(10, 95); 
            panelBoton.Controls.Add(lblSubtitulo);

            // Click handler para todos los elementos
            panelBoton.Click += clickHandler;
            lblIcono.Click += clickHandler;
            lblTexto.Click += clickHandler;
            lblSubtitulo.Click += clickHandler;
        }

        // Métodos de los botones
        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmProductos form = new frmProductos();
            form.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes form = new frmClientes();
            form.Show();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            frmVentas form = new frmVentas();
            form.Show();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            frmReportes form = new frmReportes();
            form.Show();
        }

        private void btnAcercaDe_Click(object sender, EventArgs e)
        {
            MostrarAcercaDe();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Está seguro que desea salir de QuickVentas?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MostrarAcercaDe()
        {
            Form aboutForm = new Form();
            aboutForm.Text = "Acerca de QuickVentas";
            aboutForm.Size = new Size(450, 350); 
            aboutForm.StartPosition = FormStartPosition.CenterParent;
            aboutForm.BackColor = Color.White;
            aboutForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            aboutForm.MaximizeBox = false;
            aboutForm.MinimizeBox = false;
            aboutForm.Padding = new Padding(20); 

            // Panel superior con color
            Panel panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 70;
            panelHeader.BackColor = Color.FromArgb(30, 136, 229);
            aboutForm.Controls.Add(panelHeader);

            // Título en header
            Label lblTitulo = new Label();
            lblTitulo.Text = "QuickVentas";
            lblTitulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);
            panelHeader.Controls.Add(lblTitulo);

            // Contenido 
            Label lblVersion = new Label();
            lblVersion.Text = "Versión 1.0";
            lblVersion.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblVersion.ForeColor = Color.FromArgb(30, 136, 229);
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(180, 90);
            aboutForm.Controls.Add(lblVersion);

            // Descripción 
            Label lblDescripcion = new Label();
            lblDescripcion.Text = "Sistema de gestión para pequeños negocios\n\n" +
                                 "Desarrollado como Proyecto Final\n" +
                                 "Desarrollo de Software IV\n" +
                                 "Universidad Tecnológica de Panamá\n\n" +
                                 "© 2025 Todos los derechos reservados";
            lblDescripcion.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblDescripcion.ForeColor = Color.FromArgb(66, 66, 66);
            lblDescripcion.TextAlign = ContentAlignment.MiddleCenter;
            lblDescripcion.AutoSize = false;
            lblDescripcion.Size = new Size(390, 140); 
            lblDescripcion.Location = new Point(30, 120); 
            aboutForm.Controls.Add(lblDescripcion);

            // Botón Cerrar
            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Size = new Size(120, 35); 
            btnCerrar.Location = new Point(165, 265); 
            btnCerrar.BackColor = Color.FromArgb(30, 136, 229);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCerrar.Cursor = Cursors.Hand;

            // Efecto hover en botón
            btnCerrar.MouseEnter += (s, e) => btnCerrar.BackColor = Color.FromArgb(66, 165, 245);
            btnCerrar.MouseLeave += (s, e) => btnCerrar.BackColor = Color.FromArgb(30, 136, 229);

            btnCerrar.Click += (s, ev) => aboutForm.Close();
            aboutForm.Controls.Add(btnCerrar);

            aboutForm.ShowDialog();
        }
    }
}