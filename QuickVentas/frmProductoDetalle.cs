using System;
using System.Windows.Forms;
using QuickVentas.LogicaNegocio;
using QuickVentas.Entidades;

namespace QuickVentas
{
    public partial class frmProductoDetalle : Form
    {
        private Producto producto;
        private bool esNuevo;

        public frmProductoDetalle()
        {
            InitializeComponent();
            EstilosAplicacion.AplicarEstilosBasicos(this);
            producto = new Producto();
            esNuevo = true;
            this.Text = "Nuevo Producto";
        }

        public frmProductoDetalle(Producto productoExistente)
        {
            InitializeComponent();
            producto = productoExistente;
            esNuevo = false;
            this.Text = "Editar Producto";
            CargarDatos();
        }

        private void CargarDatos()
        {
            txtNombre.Text = producto.Nombre;
            numPrecio.Value = producto.Precio;
            numStock.Value = producto.Stock;
            txtCategoria.Text = producto.Categoria;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                producto.Nombre = txtNombre.Text.Trim();
                producto.Precio = numPrecio.Value;
                producto.Stock = Convert.ToInt32(numStock.Value);
                producto.Categoria = txtCategoria.Text.Trim();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto");
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public Producto Producto
        {
            get { return producto; }
        }
    }
}