using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuickVentas
{
    public static class EstilosAplicacion
    {
        // Colores de la aplicación
        public static Color ColorPrimario = Color.FromArgb(30, 136, 229);   // Azul
        public static Color ColorSecundario = Color.FromArgb(76, 175, 80);  // Verde
        public static Color ColorExito = Color.FromArgb(76, 175, 80);       // Verde
        public static Color ColorPeligro = Color.FromArgb(244, 67, 54);     // Rojo
        public static Color ColorAdvertencia = Color.FromArgb(255, 152, 0); // Naranja
        public static Color ColorInfo = Color.FromArgb(33, 150, 243);       // Azul claro

        // Aplicar estilo a TODO un formulario
        public static void AplicarEstiloFormulario(Form formulario, string titulo = "")
        {
            // Configurar ventana
            formulario.Text = string.IsNullOrEmpty(titulo) ?
                $"QuickVentas - {formulario.Text}" :
                $"QuickVentas - {titulo}";

            formulario.StartPosition = FormStartPosition.CenterScreen;
            formulario.BackColor = Color.White;
            formulario.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // Agregar encabezado
            AgregarEncabezado(formulario, titulo);

            // Aplicar estilos a todos los controles
            AplicarEstilosControles(formulario);
        }

        private static void AgregarEncabezado(Form formulario, string titulo)
        {
            // Panel superior (header)
            Panel panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 60;
            panelHeader.BackColor = ColorPrimario;
            panelHeader.Padding = new Padding(0, 0, 0, 0);

            // Título en header
            Label lblTitulo = new Label();
            lblTitulo.Text = string.IsNullOrEmpty(titulo) ? formulario.Text : titulo;
            lblTitulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 18);

            panelHeader.Controls.Add(lblTitulo);
            formulario.Controls.Add(panelHeader);

            // Mover panelHeader al frente
            panelHeader.BringToFront();

            // Ajustar posición de otros controles
            MoverControlesAbajo(formulario, panelHeader.Height);
        }

        private static void MoverControlesAbajo(Form formulario, int alturaHeader)
        {
            foreach (Control control in formulario.Controls)
            {
                // No mover el panel del header
                if (control is Panel && control.BackColor == ColorPrimario)
                    continue;

                // Mover controles hacia abajo
                control.Location = new Point(
                    control.Location.X,
                    control.Location.Y + alturaHeader
                );
            }
        }

        private static void AplicarEstilosControles(Control controlPadre)
        {
            foreach (Control control in controlPadre.Controls)
            {
                // DataGridView
                if (control is DataGridView dgv)
                {
                    ConfigurarDataGridView(dgv);
                }
                // Button
                else if (control is Button btn)
                {
                    ConfigurarBoton(btn);
                }
                // Label
                else if (control is Label lbl && lbl.Parent is Form)
                {
                    lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }
                // TextBox, ComboBox, NumericUpDown
                else if (control is TextBox || control is ComboBox || control is NumericUpDown)
                {
                    control.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    control.BackColor = Color.White;
                }

                // Recursivo para controles dentro de contenedores
                if (control.HasChildren)
                {
                    AplicarEstilosControles(control);
                }
            }
        }

        public static void ConfigurarDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.EnableHeadersVisualStyles = false;

            // Estilo encabezados
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorPrimario;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;

            // Filas alternadas
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 245);

            // Selección
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 181, 246);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Grid lines
            dgv.GridColor = Color.FromArgb(224, 224, 224);

            // Ajuste automático
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public static void ConfigurarBoton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            btn.Height = 35;

            // Determinar color según texto
            string texto = btn.Text.ToLower();

            if (texto.Contains("guardar") || texto.Contains("agregar") || texto.Contains("nuevo") || texto.Contains("aceptar"))
            {
                btn.BackColor = ColorExito;
                btn.ForeColor = Color.White;
            }
            else if (texto.Contains("eliminar") || texto.Contains("cancelar") || texto.Contains("salir"))
            {
                btn.BackColor = ColorPeligro;
                btn.ForeColor = Color.White;
            }
            else if (texto.Contains("editar") || texto.Contains("actualizar") || texto.Contains("modificar"))
            {
                btn.BackColor = ColorAdvertencia;
                btn.ForeColor = Color.White;
            }
            else if (texto.Contains("buscar"))
            {
                btn.BackColor = ColorInfo;
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.BackColor = Color.FromArgb(158, 158, 158);
                btn.ForeColor = Color.White;
            }

            // Efecto hover simple
            btn.MouseEnter += (sender, e) =>
            {
                btn.BackColor = ControlPaint.Light(btn.BackColor);
            };

            btn.MouseLeave += (sender, e) =>
            {
                // Restaurar color original
                if (texto.Contains("guardar") || texto.Contains("agregar") || texto.Contains("nuevo") || texto.Contains("aceptar"))
                    btn.BackColor = ColorExito;
                else if (texto.Contains("eliminar") || texto.Contains("cancelar") || texto.Contains("salir"))
                    btn.BackColor = ColorPeligro;
                else if (texto.Contains("editar") || texto.Contains("actualizar") || texto.Contains("modificar"))
                    btn.BackColor = ColorAdvertencia;
                else if (texto.Contains("buscar"))
                    btn.BackColor = ColorInfo;
                else
                    btn.BackColor = Color.FromArgb(158, 158, 158);
            };
        }

        // Método para aplicar estilo solo a controles específicos (sin encabezado)
        public static void AplicarEstilosBasicos(Form formulario)
        {
            formulario.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            formulario.BackColor = Color.White;
            AplicarEstilosControles(formulario);
        }
    }
}