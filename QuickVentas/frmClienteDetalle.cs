using System;
using System.Windows.Forms;
using QuickVentas.Entidades;

namespace QuickVentas
{
    public partial class frmClienteDetalle : Form
    {
        private Cliente cliente;
        private bool esNuevo;

        public frmClienteDetalle()
        {
            InitializeComponent();
            EstilosAplicacion.AplicarEstilosBasicos(this);
            cliente = new Cliente();
            esNuevo = true;
            this.Text = "Nuevo Cliente";
        }

        public frmClienteDetalle(Cliente clienteExistente)
        {
            InitializeComponent();
            cliente = clienteExistente;
            esNuevo = false;
            this.Text = "Editar Cliente";
            CargarDatos();
        }

        private void CargarDatos()
        {
            txtNombre.Text = cliente.Nombre;
            txtTelefono.Text = cliente.Telefono;
            txtEmail.Text = cliente.Email;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                cliente.Nombre = txtNombre.Text.Trim();
                cliente.Telefono = txtTelefono.Text.Trim();
                cliente.Email = txtEmail.Text.Trim();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del cliente es obligatorio", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            // Validación simple de email
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Ingrese un email válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public Cliente Cliente
        {
            get { return cliente; }
        }

        // Métodos para eventos automáticos (dejar vacíos)
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtTelefono_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
    }
}